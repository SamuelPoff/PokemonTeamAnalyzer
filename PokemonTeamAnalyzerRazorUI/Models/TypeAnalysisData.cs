using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UsageStatCollector.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzerRazorUI.Models
{
    public class TypeAnalysisData
    {

        public Dictionary<Pokemon, List<TypeEffectiveness>> PokemonWeaknesses { get; set; }
        public Dictionary<Pokemon, List<TypeEffectiveness>> PokemonResistances { get; set; }

        public int[] TotalX4 { get; set; }
        public int[] TotalX2 { get; set; }
        public int[] TotalHalf { get; set; }
        public int[] TotalFourth { get; set; }
        public int[] TotalZero { get; set; }
        public int[] TotalWeaknesses { get; set; }
        public int[] TotalResistances { get; set; }

        public int[] Offsets { get; set; }
        /// <summary>
        /// Contains a list of TypeName Id's that have a neutral difference between resistances and weaknesses
        /// </summary>
        public List<int> OffsetNeutral { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more weaknesses than resistances represented in the given list of pokemon
        /// </summary>
        public List<Tuple<int, int>> OffsetWeaknesses { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more resistances than weaknesses represented in the given list of pokemon
        /// </summary>
        public List<Tuple<int, int>> OffsetResistances { get; set; }

    }
}
