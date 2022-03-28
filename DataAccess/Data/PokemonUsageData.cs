using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.SqlAccess;
using DataAccess.Models;

using System.Data.SqlClient;

namespace DataAccess.Data
{
    public class PokemonUsageData : IPokemonUsageData
    {

        private readonly ISqlDataAccess _db;

        public PokemonUsageData(ISqlDataAccess db)
        {

            _db = db;

        }

        /// <summary>
        /// Returns all Pokemon Usage Stat data currently in dbo.PokemonUsage
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats()
        {

            return _db.LoadDataSP<PokemonStatModel, dynamic>("PkmnDatabase.dbo.spPokemonUsage_GetAll", new { });

        }

        /// <summary>
        /// Returns the Pokmon Usage Stat that matches the given ID, or returns null if there is no row matching the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PokemonStatModel?> GetPokemonStat(int id)
        {

            var result = await _db.LoadDataSP<PokemonStatModel, dynamic>("PkmnDatabse.dbo.spPokemonUsage_Get", new { Id = id });
            return result.FirstOrDefault();

        }

        public async Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats(int generation, string format)
        {

            return await _db.LoadDataSP<PokemonStatModel, dynamic>("PkmnDatabase.dbo.spPokemonUsage_GetAllGenAndFormat", new { Generation=generation, Format=format });

        }


        public Task InsertPokemonStat(PokemonStatModel stat)
        {

            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemonUsage_Insert", new
            {

                Generation=stat.Generation,
                Format=stat.Format,

                PokemonId=stat.PokemonId,
                RawCount=stat.RawCount,
                Abilities=stat.Abilities,
                Items=stat.Items,
                Spreads=stat.Spreads,
                Moves=stat.Moves,
                Teammates=stat.Teammates,
                ChecksAndCounters=stat.ChecksAndCounters

            });

        }

        public Task UpdatePokemonStat(PokemonStatModel stat)
        {

            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemonUsage_Update", new 
            { 
                
                Id=stat.ID,
                Generation=stat.Generation,
                Format=stat.Format,

                PokemonId=stat.PokemonId,
                Rawcount=stat.RawCount,
                Abilities=stat.Abilities,
                Items=stat.Items,
                Spreads=stat.Spreads,
                Moves=stat.Moves,
                Teammates=stat.Teammates,
                ChecksAndCounters=stat.ChecksAndCounters
            
            });

        }

        public Task DeletePokemonStat(int id)
        {

            return _db.SaveDataSP("PkmnDatabase.dbo.spPokemonUsage_Delete", new { Id = id });

        }

        public Task<IEnumerable<PokemonStatModel>> GetAllPokemonStatsByPkmnId(int pkmnId)
        {

            return _db.LoadDataSP<PokemonStatModel, dynamic>("PkmnDatabase.dbo.spPokemonUsage_GetAllByPkmnID", new { PokemonId = pkmnId });

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
