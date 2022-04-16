using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Diagnostics;

using DataAccess.Models;
using DataAccess.Data;

namespace PokemonTeamAnalyzer
{

    public static class Collector
    {

        private static IPokemonUsageData _data;

        private static List<string> SupportedFormats = new List<string>();

        public static void CollectorConfigure(IPokemonUsageData data)
        {
            _data = data;
            GetSupportedFormats();
        }

        public static async Task UpdateUsageStats()
        {

            Console.WriteLine("Updating Usage Stats...");
            List<PokemonFormat> formats = GetFormats();

            List<Task<List<PokemonStatModel>>> parsingTasks = new List<Task<List<PokemonStatModel>>>();
            List<Task> insertAndUpdateTasks = new List<Task>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _data.BeginOperations();

            foreach (PokemonFormat format in formats)
            {

                string url = GetUrl(format);

                parsingTasks.Add( Parser.ParseStats(url, format.Generation, format.FormatType) );
                

            }

            await Task.WhenAll(parsingTasks);

            foreach (var parsingTask in parsingTasks)
            {
                foreach(PokemonStatModel stat in parsingTask.Result)
                {

                    //Check if stat already exists (which 99% of the time it should)
                    //Insert new stat if it doesnt, otherwise: Update
                    bool exists = await _data.RecordExists(stat.PokemonId, stat.Generation, stat.Format);
                    if (exists)
                    {
                        insertAndUpdateTasks.Add(_data.UpdatePokemonStat(stat));
                    }
                    else
                    {
                        insertAndUpdateTasks.Add(_data.InsertPokemonStat(stat));
                    }

                }
                
            }

            await Task.WhenAll(insertAndUpdateTasks);

            _data.EndOperations();

            stopwatch.Stop();

            Console.WriteLine($"Total elapsed time: {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("Completed collecting usage stats!");

        }

        /// <summary>
        /// Call to seed empty PokemonUsage table with data
        /// (Doesnt check for updates, to re-parse the data while checking for updates to be inserted/updated, use UpdateUsageStats())
        /// </summary>
        /// <returns></returns>
        public static async Task SeedUsageStats()
        {

            Console.WriteLine("Collecting Usage Stats");
            List<PokemonFormat> formats = GetFormats();

            List<Task<List<PokemonStatModel>>> parsingTasks = new List<Task<List<PokemonStatModel>>>();
            List<Task> insertTasks = new List<Task>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _data.BeginOperations();

            foreach (PokemonFormat format in formats)
            {

                string url = GetUrl(format);

                parsingTasks.Add(Parser.ParseStats(url, format.Generation, format.FormatType));


            }

            await Task.WhenAll(parsingTasks);

            foreach (var parsingTask in parsingTasks)
            {
                foreach (PokemonStatModel stat in parsingTask.Result)
                {



                    insertTasks.Add(_data.InsertPokemonStat(stat));

                }

            }

            await Task.WhenAll(insertTasks);

            _data.EndOperations();

            stopwatch.Stop();

            Console.WriteLine($"Total elapsed time: {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("Completed collecting usage stats!");

        }


        private static string GetUrl(PokemonFormat format)
        {

            return $"https://www.smogon.com/stats/2022-02/moveset/gen{format.Generation}{format.FormatType}-0.txt";

        }

        private static List<PokemonFormat> GetFormats()
        {

            List<PokemonFormat> formats = new List<PokemonFormat>();

            foreach(string strFormat in SupportedFormats)
            {

                string[] stringComponents = strFormat.Split("-");
                PokemonFormat format = new PokemonFormat();
                format.Generation = Int32.Parse(stringComponents[0]);
                format.FormatType = stringComponents[1];
                formats.Add(format);

            }

            return formats;

        }

        

        private static void GetSupportedFormats() {

            SupportedFormats.Add("8-ubers");
            SupportedFormats.Add("8-ou");
            SupportedFormats.Add("8-uu");
            //SupportedFormats.Add("8-ru");
            //SupportedFormats.Add("8-nu");
            //SupportedFormats.Add("8-pu");
            //SupportedFormats.Add("8-lc");
            //SupportedFormats.Add("8-monotype");
            //SupportedFormats.Add("8-nationaldex");

            //SupportedFormats.Add("7-ou");
            //SupportedFormats.Add("6-ou");

        }

        struct PokemonFormat
        {

            public int Generation;
            public string FormatType;

        }

    }
}
