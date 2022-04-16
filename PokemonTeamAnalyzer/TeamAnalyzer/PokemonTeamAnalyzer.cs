using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzer.TeamAnalyzer
{
    public static class TeamAnalyzer
    {

        /// <summary>
        /// Given a team of pokemon, return how many weaknesses the team has against every type
        /// </summary>
        /// <returns>An array containing how many weaknesses the team has against every type</returns>
        public static int[] GetTeamTypeWeaknesses(List<Pokemon> Pokemon)
        {

            int[] weaknesses = new int[PokemonType.NumberOfTypes];

            foreach(Pokemon pokemon in Pokemon)
            {

                //Get pokemon's type weaknesses and increment the position in the array for that given type if it is present
                List<TypeEffectiveness> typeWeaknesses;
                typeWeaknesses = PokemonType.GetAllWeaknesses(pokemon.Type1, pokemon.Type2);

                foreach(var weakness in typeWeaknesses)
                {

                    weaknesses[((int)weakness.AttackingType)] += 1;

                }

            }

            return weaknesses;

        }

        /// <summary>
        /// Given a team of pokemon, returns how many resistances that team has total against every type
        /// </summary>
        /// <param name="Pokemon"></param>
        /// <returns>An array containing how many resistances the list of pokemon has against every type</returns>
        public static int[] GetTeamTypeResistances(List<Pokemon> Pokemon)
        {

            int[] resistances = new int[PokemonType.NumberOfTypes];

            foreach(Pokemon pokemon in Pokemon)
            {

                List<TypeEffectiveness> typeResistances = PokemonType.GetAllResistances(pokemon.Type1, pokemon.Type2);

                foreach(var resistance in typeResistances)
                {

                    resistances[((int)resistance.AttackingType)] += 1;

                }

            }

            return resistances;

        }

    }
}
