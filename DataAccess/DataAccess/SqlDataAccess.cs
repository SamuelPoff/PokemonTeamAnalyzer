using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.SqlAccess
{

    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadDataSP<T, U>(string storedProc, U parameters, string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);

        }

        public async Task SaveDataSP<T>(string storedProc, T parameters, string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            try {
                await connection.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string queryString, U parameters, string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(queryString, parameters, commandType: CommandType.Text);

        }

        public async Task SaveData<T>(string queryString, T parameters, string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(queryString, parameters, commandType: CommandType.Text);

        }

        public async Task SaveDataCmd(SqlCommand cmd, string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            cmd.Connection = (SqlConnection)connection;

            await cmd.ExecuteNonQueryAsync();

        }

    }

}



