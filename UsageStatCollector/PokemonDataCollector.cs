using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokeNetApi;
using PokeNetApi.Objects;

using DataAccess.Data;
using DataAccess.Models;

namespace UsageStatCollector
{
    public static class PokemonDataCollector
    {

        private readonly static int PokemonNum = 898;
        private static IPokemonData _data;

        public static void PokemonDataCollectorConfigure(IPokemonData data)
        {
            _data = data;
        }

        public static async Task CollectPokemonData()
        {

            DataFetcher dataFetcher = new DataFetcher();
            //List<Task<Pokemon>> fetchingTasks = new List<Task<Pokemon>>();

            List<Task> insertTasks = new List<Task>();
            List<Pokemon> pokemonList = new List<Pokemon>();
            

            for (int i = 1; i <= PokemonNum; ++i)
            {

                string path = $"pokemon/{i}";
                pokemonList.Add(await dataFetcher.GetPokemonAsync(path));

            }

            _data.BeginOperations();

            foreach (Pokemon pokemon in pokemonList)
            {

                PokemonModel pokemonModel = PokemonToPokemonModel(pokemon);
                insertTasks.Add( _data.InsertPokemon(pokemonModel) );

            }

            await Task.WhenAll(insertTasks);

            _data.EndOperations();

        }

        private static PokemonModel PokemonToPokemonModel(Pokemon pokemon)
        {

            PokemonModel pokemonModel = new PokemonModel();
            pokemonModel.Name = pokemon.name;
            
            //Get types
            for(int i = 0; i < pokemon.types.Length; i++)
            {

                Type type = pokemon.types[i];
                if(type.slot == 1)
                {
                    pokemonModel.Type1 = type.type.name.ToUpper();
                }else if(type.slot == 2)
                {
                    pokemonModel.Type2 = type.type.name.ToUpper();
                }

            }

            //Get abilities
            for(int i = 0; i < pokemon.abilities.Length; i++)
            {
                Ability ability = pokemon.abilities[i];
                if (ability.is_hidden)
                {
                    pokemonModel.HiddenAbility = ability.ability.name;
                }
                else if(ability.slot == 1)
                {
                    pokemonModel.Ability1 = ability.ability.name;
                }else if(ability.slot == 2)
                {
                    pokemonModel.Ability2 = ability.ability.name;
                }
            }

            //Get base stats
            pokemonModel.HP = pokemon.stats[0].base_stat;
            pokemonModel.Att = pokemon.stats[1].base_stat;
            pokemonModel.Def = pokemon.stats[2].base_stat;
            pokemonModel.SpAtt = pokemon.stats[3].base_stat;
            pokemonModel.SpDef = pokemon.stats[4].base_stat;
            pokemonModel.Spd = pokemon.stats[5].base_stat;

            //Get Sprite
            pokemonModel.SpriteUrl = pokemon.sprites.front_default;

            return pokemonModel;

        }

    }
}
