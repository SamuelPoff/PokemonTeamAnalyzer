using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.IO;

using DataAccess.Models;
using DataAccess.Data;


namespace UsageStatCollector
{
    public static class Parser
    {

        private static IPokemonData _data;
        public static void ParserConfigure(IPokemonData data)
        {

            _data = data;

        }

        private static async Task<string> GetUsageStatText(string url)
        {

            //Validate URL
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);

            var result = await client.SendAsync(request);
            bool isValid = false;
            if (result.IsSuccessStatusCode && url.Contains("www.smogon.com/stats/"))
                isValid = true;

            if (!isValid)
            {
                Console.WriteLine($"URL: {url} was invalid");
                throw new Exception("Tried to access invalid Usage Stat URL");
            }

            return await client.GetStringAsync(url);

        }

        public static async Task<List<PokemonStatModel>> ParseStats(string url, int generation, string format)
        {

            string text = await GetUsageStatText(url);
            //List<Task> insertTasks = new List<Task>();

            List<PokemonStatModel> stats = new List<PokemonStatModel>();

            using (StringReader reader = new StringReader(text))
            {

                int sectionIndex = 0;
                char[] removechars = new char[] { '|' };

                string name = "";
                
                int rawCount = 0;
                string abilities = "";
                string items = "";
                string moves = "";
                string spreads = "";
                string teammates = "";
                string counters = "";

                string line = reader.ReadLine();
                while (line != null)
                {

                    //Check for new section line
                    if (line.Contains("+-"))
                    {
                        sectionIndex++;
                        line = reader.ReadLine();

                        //Next Section, submit current one
                        if (sectionIndex >= 9)
                        {

                            sectionIndex = 0;

                            //Get Pokemon ID corresponding to the name
                            PokemonModel pkmn = await _data.GetPokemonByName(name.ToLower());

                            //Fill out stat model object and add it to list
                            PokemonStatModel stat = new PokemonStatModel();
                            stat.Generation = generation;
                            stat.Format = format;
                            if(pkmn != null)
                            {
                                Console.WriteLine("Pokemon name found in DB!: " + name);
                                stat.PokemonId = pkmn.ID;
                            }
                            else
                            {
                                //Console.WriteLine("!!! Pokemon name wasnt found in DB");
                                stat.PokemonId = 1;
                            }
                            stat.RawCount = rawCount;
                            stat.Abilities = abilities.Trim(',');
                            stat.Items = items.Trim(',');
                            stat.Moves = moves.Trim(',');
                            stat.Spreads = spreads.Trim(',');
                            stat.Teammates = teammates.Trim(',');
                            stat.ChecksAndCounters = counters.Trim(',');
                            stats.Add(stat);

                            //Reset strings
                            abilities = "";
                            items = "";
                            moves = "";
                            spreads = "";
                            teammates = "";
                            counters = "";

                        }

                        continue;
                    }



                    switch (sectionIndex)
                    {
                        //Parse NAME
                        case 1:
                            name = TrimLine(line, removechars);
                            break;
                        //Parse meta data
                        case 2:
                            if (line.Contains("Raw count:"))
                            {
                                string[] splitString = line.Split(':');
                                rawCount = Int32.Parse(splitString[1].Replace("|", String.Empty));
                            }
                            break;
                        //Parse ABILITIES
                        case 3:
                            if (line.Contains("Abilities"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            abilities += FormatStatLine(line, removechars) + ",";

                            break;
                        //Parse ITEMS
                        case 4:

                            if (line.Contains("Items"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            items += FormatStatLine(line, removechars) + ",";

                            break;
                        //Parse SPREADS
                        case 5:

                            if (line.Contains("Spreads"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            spreads += FormatStatLine(line, removechars) + ",";

                            break;
                        //Parse MOVES
                        case 6:
                            if (line.Contains("Moves"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            moves += FormatStatLine(line, removechars) + ",";

                            break;
                        //Parse TEAMMATES
                        case 7:

                            if (line.Contains("Teammates"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            teammates += FormatStatLine(line, removechars) + ",";

                            break;
                        //PARSE CHECKS AND COUNTERS
                        case 8:

                            if (line.Contains("Checks and Counters") || line.Contains("/"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            int rmvIndex = line.IndexOf('(');
                            if (rmvIndex != -1)
                            {
                                line = line.Remove(rmvIndex);
                                counters += FormatStatLine(line, removechars) + ",";
                            }


                            break;

                    }

                    line = reader.ReadLine();

                }

            }

            return stats;

        }

        private static string TrimLine(string s, char[] chars)
        {

            foreach (char c in chars)
            {
                s = s.Replace(c.ToString(), String.Empty);
            }
            s = s.Trim();

            return s;

        }

        private static string FormatStatLine(string s, char[] chars)
        {

            foreach (char c in chars)
            {
                s = s.Replace(c.ToString(), String.Empty);
            }
            s = s.Trim();

            //Add formatting for tuples

            //Find where percent starts and add underscore, and remove random extra spaces
            //To cover the most possible(and most likely) senarios, split string at spaces, and assume the last one is the percent
            //currently this is the case 100% of the time, but could change as more items are added

            string[] sections = s.Split(" ");
            string formattedString = String.Empty;
            for(int i = 0; i < sections.Length; i++)
            {

                //Add "-" between item names that contained spaces before for easier parsing
                //Add a space between item name and percent
                if (i == sections.Length - 1)
                {
                    formattedString += " ";
                }
                else if (i != 0 && sections.Length > 2)
                {
                    formattedString += "-";
                }

                

                formattedString += sections[i];

                
            }

            //Because of course the source text randomly contains double-spaces: so it leaves a trailing "-"
            if(formattedString.Contains("- "))
            {
                formattedString = formattedString.Remove(formattedString.LastIndexOf("-"), 1);
            }

            return formattedString;

        }

        /// <summary>
        /// Returns a list of tuples for any of the usage stat sections that contain a move/item/spread/etc with a corresponding percent
        /// of how often it is used (Given that the text provided is the CSV text generated by the data collector)
        /// </summary>
        /// <returns></returns>
        public static List<Tuple<string, float>> ParseUsageSection(string sectionCsvText)
        {

            //Records are stored like this: "MoveName1 10%, MoveName2 15.5%, ETC..."

            List < Tuple<string, float> > tupleList = new List<Tuple<string, float>>();
            string[] records = sectionCsvText.Split(",");

            foreach(string record in records)
            {

                string[] tuple = record.Split(" ");

                //Construct and add tuple for this record index 0 will be the item name, index 1 will be the usage percent
                float percent = float.Parse(tuple[1].Replace("%", String.Empty));
                tupleList.Add(new Tuple<string, float>(tuple[0], percent));

            }

            return tupleList;

        }

    }
}
