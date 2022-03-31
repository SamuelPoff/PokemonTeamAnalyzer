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
        IDbConnection connection = null;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadDataSP<T, U>(string storedProc, U parameters, string connectionId = "Default")
        {

            if(connection == null)
            {
                using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));

                return await conn.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {

                return await connection.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);

            }
            

        }

        public async Task SaveDataSP<T>(string storedProc, T parameters, string connectionId = "Default")
        {

            if(connection == null)
            {

                using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));

                try
                {
                    await conn.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {

                try
                {
                    await connection.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string queryString, U parameters, string connectionId = "Default")
        {

            if(connection == null)
            {

                using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));

                return await conn.QueryAsync<T>(queryString, parameters, commandType: CommandType.Text);

            }
            else
            {

                return await connection.QueryAsync<T>(queryString, parameters, commandType: CommandType.Text);

            }

        }

        public async Task SaveData<T>(string queryString, T parameters, string connectionId = "Default")
        {

            if(connection == null)
            {

                using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));

                await conn.ExecuteAsync(queryString, parameters, commandType: CommandType.Text);

            }
            {

                await connection.ExecuteAsync(queryString, parameters, commandType: CommandType.Text);

            }

        }

        public async Task SaveDataCmd(SqlCommand cmd, string connectionId = "Default")
        {

            if(connection == null)
            {

                try
                {

                    using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));
                    
                    cmd.Connection = (SqlConnection)conn;

                    await cmd.ExecuteNonQueryAsync();

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {

                try
                {
                    cmd.Connection = (SqlConnection)connection;
                    await cmd.ExecuteNonQueryAsync();
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            

        }

        public void BeginOperations(string connectionId = "Default")
        {

            connection = new SqlConnection(_config.GetConnectionString(connectionId));
            Console.WriteLine("Openning up connection for continued operations...");
            connection.Open();

        }

        public void EndOperations()
        {

            if(connection == null)
            {
                Exception e = new Exception("Ended SQL data access operations before actually beginning any (calling BeginOperations(connectionId?))");
                throw e;
            }

            Console.WriteLine("Closing connection for continuous operations...");

            connection.Close();
            connection.Dispose();
            connection = null;

        }

    }

}



