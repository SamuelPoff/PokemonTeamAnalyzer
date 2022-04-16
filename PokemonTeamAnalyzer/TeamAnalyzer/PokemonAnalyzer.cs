using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzer.TeamAnalyzer
{
    /// <summary>
    /// Static helper class for analyzing different aspects of a single pokemon, from type matchups, to matchups against other pokemon
    /// </summary>
    public static class PokemonAnalyzer
    {

        /// <summary>
        /// Analyzes the given pokemon's team weaknesses
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns>A list containing all types this pokemon is weak to</returns>
        public static List<TypeEffectiveness> GetTypeWeakness(Pokemon pokemon)
        {

            return new List<TypeEffectiveness>();

        }

    }
}
