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


        public Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats()
        {

            string queryString = "";

            return _db.LoadData<PokemonStatModel, dynamic>(queryString, new { });

        }


        public async Task<PokemonStatModel?> GetPokemonStat(int id)
        {

            string queryString = "";

            var result = await _db.LoadData<PokemonStatModel, dynamic>(queryString, new { Id = id });
            return result.FirstOrDefault();

        }

        public Task InsertPokemonStat(PokemonStatModel stat, string tableName)
        {

            string commandText = $"INSERT INTO PkmnDatabase.dbo.{tableName}(PkmnID, RawCount, Abilities, Items, Spreads, Moves, Teammates, ChecksAndCounters)";
            commandText += "VALUES(@PkmnID, @RawCount, @Abilities, @Items, @Spreads, @Moves, @Teammates, @ChecksAndCounters)";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.Parameters.AddWithValue("@PkmnID", stat.PkmnID);
            cmd.Parameters.AddWithValue("@RawCount", stat.RawCount);
            cmd.Parameters.AddWithValue("@Abilities", stat.Abilities);
            cmd.Parameters.AddWithValue("@Items", stat.Items);
            cmd.Parameters.AddWithValue("@Spreads", stat.Spreads);
            cmd.Parameters.AddWithValue("@Moves", stat.Moves);
            cmd.Parameters.AddWithValue("@Teammates", stat.Teammates);
            cmd.Parameters.AddWithValue("@ChecksAndCounters", stat.ChecksAndCounters);

            return _db.SaveDataCmd(cmd);

        }

        public Task UpdatePokemonStat(PokemonStatModel stat, string tableName)
        {

            string commandText = $"UPDATE PkmnBase.dbo.{tableName}";
            commandText += "SET PkmnId=@PkmnId, RawCount=@RawCount, Abilities=@Abilities, Items=@Items, Spreads=@Spreads, Moves=@Moves, Teammates=@Teammates, ChecksAndCounters=@ChecksAndCounters";
            commandText += "WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;

            cmd.Parameters.AddWithValue("@PkmnID", stat.PkmnID);
            cmd.Parameters.AddWithValue("@RawCount", stat.RawCount);
            cmd.Parameters.AddWithValue("@Abilities", stat.Abilities);
            cmd.Parameters.AddWithValue("@Items", stat.Items);
            cmd.Parameters.AddWithValue("@Spreads", stat.Spreads);
            cmd.Parameters.AddWithValue("@Moves", stat.Moves);
            cmd.Parameters.AddWithValue("@Teammates", stat.Teammates);
            cmd.Parameters.AddWithValue("@ChecksAndCounters", stat.ChecksAndCounters);

            return _db.SaveDataCmd(cmd);

        }

        public Task DeletePokemonStat(int id, string tableName)
        {

            string commandText = $"DELETE FROM PkmnDatabase.dbo.{tableName} WHERE Id=@Id";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;

            cmd.Parameters.AddWithValue("@Id", id);

            return _db.SaveDataCmd(cmd);

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
