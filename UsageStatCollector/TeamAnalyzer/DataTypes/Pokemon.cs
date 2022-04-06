using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageStatCollector.TeamAnalyzer.DataTypes
{
    /// <summary>
    /// Represents a Pokemon build (I.E Item, Ability, Move set, Ev Spread)
    /// </summary>
    public class Pokemon
    {

        public string Name { get; set; }
        public PokemonType.TypeName Type1 { get; set; }
        public PokemonType.TypeName Type2 { get; set; }
        public string AbilityName { get; set; }
        public string ItemName { get; set; }
        public PokemonMoveset Moveset { get; set; }
        public PokemonSpread Spread { get; set; }
        public PokemonStats Stats { get; set; }

    }
}
