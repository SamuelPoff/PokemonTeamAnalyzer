using System.Collections.Generic;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace DataAccess.SqlAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadDataSP<T, U>(string storedProc, U parameters, string connectionId = "Default");
        Task SaveDataSP<T>(string storedProc, T parameters, string connectionId = "Default");

        Task<IEnumerable<T>> LoadData<T, U>(string queryString, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProc, T parameters, string connectionId = "Default");
        Task<T> ExecuteScalarAsync<T, U>(string queryString, U parameters, string connectionId = "default");
        Task SaveDataCmd(SqlCommand cmd, string connectionId = "Default");

        void BeginOperations(string connectionId = "Default");
        void EndOperations();
    }
}