using System;

namespace DataAccess.Models
{
    public class PokemonStatModel
    {

        public int ID { get; set; }
        public int PkmnID { get; set; }
        public int RawCount { get; set; }
        public string Abilities { get; set; }
        public string Items { get; set; }
        public string Spreads { get; set; }
        public string Moves { get; set; }
        public string Teammates { get; set; }
        public string ChecksAndCounters { get; set; }

    }
}
