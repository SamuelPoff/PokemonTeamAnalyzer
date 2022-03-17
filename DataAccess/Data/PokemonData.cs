using DataAccess.SqlAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

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

        public Task InsertPokemon(PokemonModel pokemon) {

            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Insert", new
            {
                pokemon.Name,
                pokemon.Type1,
                pokemon.Type2,
                Hp = pokemon.HP,
                Att = pokemon.Att,
                Def = pokemon.Def,
                SpAtt = pokemon.SpAtt,
                SpDef = pokemon.SpDef,
                spd = pokemon.Spd
            });

        }
            

        public Task UpdatePokemon(PokemonModel pokemon) =>

            _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Update", new
            {
                Id = pokemon.ID,
                pokemon.Name,
                pokemon.Type1,
                pokemon.Type2,
                Hp = pokemon.HP,
                Att = pokemon.Att,
                Def = pokemon.Def,
                SpAtt = pokemon.SpAtt,
                SpDef = pokemon.SpDef,
                spd = pokemon.Spd
            });

        public Task DeletePokemon(int id)
        {
            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemon_Delete", new { Id = id });
        }

    }
}
