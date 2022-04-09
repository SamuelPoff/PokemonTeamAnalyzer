using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IPokemonUsageData
    {
        Task DeletePokemonStat(int id);
        Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats();
        Task<IEnumerable<PokemonStatModel>> GetAllPokemonStats(int generation, string format);
        Task<PokemonStatModel> GetPokemonStat(int id);
        Task InsertPokemonStat(PokemonStatModel stat);
        Task UpdatePokemonStat(PokemonStatModel stat);
        Task<IEnumerable<PokemonStatModel>> GetAllPokemonStatsByPkmnId(int pkmnId);
        Task<bool> RecordExists(int pokemonId, int gen, string format);
        bool IsEmpty();
        void BeginOperations();

        void EndOperations();

    }
}