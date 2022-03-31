using DataAccess.SqlAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;
using DataAccess.Filters;

namespace DataAccess.Data
{
    public class PokemonData : IPokemonData
    {

        private readonly ISqlDataAccess _db;

        public PokemonData(ISqlDataAccess db)
        {

            _db = db;

        }

        public Task<IEnumerable<PokemonModel>> GetAllPokemon() =>
            _db.LoadDataSP<PokemonModel, dynamic>("PkmnDatabase.dbo.spPokemon_GetAll", new { });

        public async Task<PokemonModel?> GetPokemon(int id)
        {

            var results = await _db.LoadDataSP<PokemonModel, dynamic>("PkmnDatabase.dbo.spPokemon_Get", new { Id = id });
            return results.FirstOrDefault();

        }

        public async Task<PokemonModel?> GetPokemonByName(string name)
        {

            var results = await _db.LoadDataSP<PokemonModel, dynamic>("PkmnDatabase.dbo.spPokemon_GetByName", new { Name = name });
            return results.FirstOrDefault();

        }

        public async Task<IEnumerable<PokemonModel>> GetPokemonByNameSearch(string searchString)
        {
            var results = await _db.LoadDataSP<PokemonModel, dynamic>("PkmnDatabase.dbo.spPokemon_GetByNameStringSearch", new { SearchString = searchString});
            return results;
        }

        public async Task<IEnumerable<PokemonModel>> GetPokemonByFilter(PokemonFilterData filterData)
        {

            //Check for just a name filter since there is an optimized stored proc for that
            if (filterData.NameSearchString != "" && filterData.TypeFilter == "no-selection" && filterData.TypeFilter2 == "no-selection" && filterData.AbilityNameSearchString == "")
            {
                return await _db.LoadDataSP<PokemonModel, dynamic>("PokemonDatabase.dbo.spPokemon_GetByNameStringSearch", new { SearchString = filterData.NameSearchString});
            }

            //Check for empty filter
            if (filterData.IsEmpty())
            {
                return await _db.LoadDataSP<PokemonModel, dynamic>("PokemonDatabase.dbo.spPokemon_GetAll", new { });
            }

            //Otherwise build out the query based on what info we have to filter by
            string queryString = "" +
                "SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]  " +
                "FROM PkmnDatabase.dbo.Pokemon " +
                "WHERE ";

            bool firstClauseAdded = false;

            if(filterData.NameSearchString != "" && filterData.NameSearchString != null)
            {
                if (firstClauseAdded)
                    queryString += "AND ";

                queryString += "[Name] LIKE '%'+@NameSearchString+'%' ";
                firstClauseAdded = true;
            }

            if (filterData.TypeFilter != "no-selection")
            {

                //If Type 1 has something but type 2 is left default, then search both types for that typing
                if (filterData.TypeFilter2 == "no-selection")
                {

                    if (firstClauseAdded)
                        queryString += "AND ";

                    queryString += "([Type1] LIKE '%'+@TypeFilter+'%' OR [Type2] LIKE '%'+@TypeFilter+'%') ";
                    firstClauseAdded = true;
                }
                else
                {

                    if (firstClauseAdded)
                        queryString += "AND ";

                    queryString += "[Type1] LIKE '%'+@TypeFilter+'%' ";
                    firstClauseAdded = true;

                }

            }

            if(filterData.TypeFilter2 != "no-selection")
            {
                if(filterData.TypeFilter2 == "NONE")
                {
                    if (firstClauseAdded)
                        queryString += "AND ";

                    queryString += "[Type2] IS NULL ";
                }
                else
                {
                    if (firstClauseAdded)
                        queryString += "AND ";

                    queryString += "[Type2] LIKE '%'+@TypeFilter2+'%' ";
                }

                firstClauseAdded = true;
            }

            if(filterData.AbilityNameSearchString != "" && filterData.AbilityNameSearchString != null)
            {
                if (firstClauseAdded)
                    queryString += "AND ";

                queryString += "([Ability1] LIKE '%'+@AbilityNameSearchString+'%' OR [Ability2] LIKE '%'+@AbilityNameSearchString+'%' OR [HiddenAbility] LIKE '%'+@AbilityNameSearchString+'%') ";
                firstClauseAdded = true;
            }

            Console.WriteLine(queryString);

            return await _db.LoadData<PokemonModel, dynamic>(queryString, new {
                NameSearchString=filterData.NameSearchString ,
                TypeFilter=filterData.TypeFilter,
                TypeFilter2=filterData.TypeFilter2,
                AbilityNameSearchString=filterData.AbilityNameSearchString
            });

        }

        public Task InsertPokemon(PokemonModel pokemon) {

            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Insert", new
            {
                pokemon.Name,
                pokemon.Type1,
                pokemon.Type2,
                pokemon.Ability1,
                pokemon.Ability2,
                pokemon.HiddenAbility,
                Hp = pokemon.HP,
                Att = pokemon.Att,
                Def = pokemon.Def,
                SpAtt = pokemon.SpAtt,
                SpDef = pokemon.SpDef,
                Spd = pokemon.Spd,
                SpriteUrl = pokemon.SpriteUrl
            }); 

        }


        public Task UpdatePokemon(PokemonModel pokemon) =>

            _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Update", new
            {
                Id = pokemon.ID,
                pokemon.Name,
                pokemon.Type1,
                pokemon.Type2,
                pokemon.Ability1,
                pokemon.Ability2,
                pokemon.HiddenAbility,
                Hp = pokemon.HP,
                Att = pokemon.Att,
                Def = pokemon.Def,
                SpAtt = pokemon.SpAtt,
                SpDef = pokemon.SpDef,
                Spd = pokemon.Spd,
                SpriteUrl = pokemon.SpriteUrl
            });

        public Task DeletePokemon(int id)
        {
            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Delete", new { Id = id });
        }

        public void BeginOperations()
        {

            _db.BeginOperations();

        }

        public void EndOperations()
        {

            _db.EndOperations();

        }

    }
}
