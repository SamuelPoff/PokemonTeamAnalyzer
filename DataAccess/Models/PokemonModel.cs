using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PokemonModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int HP { get; set; }
        public int Att { get; set; }
        public int Def { get; set; }
        public int SpAtt { get; set; }
        public int SpDef { get; set; }
        public int Spd { get; set; }

    }
}
