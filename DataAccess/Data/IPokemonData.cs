using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccess.Filters;

namespace DataAccess.Data
{
    public interface IPokemonData
    {
        Task DeletePokemon(int id);
        Task<IEnumerable<PokemonModel>> GetAllPokemon();
        Task<PokemonModel> GetPokemon(int id);
        Task<PokemonModel?> GetPokemonByName(string name);
        Task<IEnumerable<PokemonModel>> GetPokemonByNameSearch(string searchString, int limit = 0);
        Task<IEnumerable<PokemonModel>> GetPokemonByFilter(PokemonFilterData filterData);
        Task InsertPokemon(PokemonModel pokemon);
        Task UpdatePokemon(PokemonModel pokemon);
        bool IsEmpty();
        void BeginOperations();
        void EndOperations();
    }
}