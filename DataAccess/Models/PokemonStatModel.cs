using System;

namespace DataAccess.Models
{
    public class PokemonStatModel
    {

        public int ID { get; set; }

        public int Generation { get; set; } = 1;
        public string Format { get; set; } = "";
        public int PokemonId { get; set; } = 0;
        public int RawCount { get; set; } = 0;
        public string Abilities { get; set; } = "";
        public string Items { get; set; } = "";
        public string Spreads { get; set; } = "";
        public string Moves { get; set; } = "";
        public string Teammates { get; set; } = "";
        public string ChecksAndCounters { get; set; } = "";

    }
}
