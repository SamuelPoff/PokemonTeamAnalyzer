using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Filters
{
    public class PokemonFilterData
    {

        public string NameSearchString { get; set; }
        public string AbilityNameSearchString { get; set; }
        public string TypeFilter { get; set; }
        public string TypeFilter2 { get; set; }

        /// <summary>
        /// Returns if all filter fields were left empty or default
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {

            bool empty = true;

            if(NameSearchString != "" && NameSearchString != null)
            {
                empty = false;
            }
            if(AbilityNameSearchString != "" && AbilityNameSearchString != null)
            {
                empty = false;
            }
            if(TypeFilter != "no-selection" || TypeFilter2 != "no-selection")
            {
                empty = false;
            }

            return empty;

        }

    }
}
