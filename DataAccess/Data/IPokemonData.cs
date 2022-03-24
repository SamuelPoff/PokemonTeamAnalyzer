using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IPokemonData
    {
        Task DeletePokemon(int id);
        Task<IEnumerable<PokemonModel>> GetAllPokemon();
        Task<PokemonModel> GetPokemon(int id);

        Task<PokemonModel?> GetPokemonByName(string name);
        Task InsertPokemon(PokemonModel pokemon);
        Task UpdatePokemon(PokemonModel pokemon);

        void BeginOperations();
        void EndOperations();
    }
}