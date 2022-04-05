using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public static class TextFormater
    {

        public static string FormatString(string text)
        {

            if (!String.IsNullOrEmpty(text))
            {
                //All trailing whitespace must be removed
                text = text.Trim();

                //All words with spaces have the spaces replaced with "-"
                text = text.Replace(" ", "-");
            }

            return text;

        }

    }
}
