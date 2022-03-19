using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IPokemonUsageData
    {
        Task DeletePokemonStat(int id, string tableName);
        Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats();
        Task<PokemonStatModel> GetPokemonStat(int id);
        Task InsertPokemonStat(PokemonStatModel stat, string tableName);
        Task UpdatePokemonStat(PokemonStatModel stat, string tableName);

        void BeginOperations();

        void EndOperations();

    }
}