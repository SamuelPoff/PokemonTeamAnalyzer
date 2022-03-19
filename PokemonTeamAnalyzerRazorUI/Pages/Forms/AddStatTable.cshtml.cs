using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PokemonTeamAnalyzerRazorUI.Models;
using UsageStatUpdater;

namespace PokemonTeamAnalyzerRazorUI.Pages.Forms
{

    public class AddStatTableModel : PageModel
    {

        private readonly IConfiguration _config;

        [BindProperty]
        public TableCreationDataModel TableCreationData { get; set; }

        public AddStatTableModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //Create and populate table
            string url = $"https://www.smogon.com/stats/{TableCreationData.Year}-{TableCreationData.Month}/moveset/gen{TableCreationData.Generation}{TableCreationData.Format}-0.txt";
            string tableName = $"UsageStatsGen{TableCreationData.Generation}{TableCreationData.Format}{TableCreationData.Year}_{TableCreationData.Month}";

            //Validate that url exists and that there is not already a table by this name
            //In the future if there is a table by this name already it will simply update it
            bool valid = false;
            try
            {

                Console.WriteLine("Checking URL: " + url);

                using HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Head, url);

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) {
                    valid = true;
                    Console.WriteLine("Valid URL!");
                }
                else
                {
                    Console.WriteLine("Invalid URL. Canceling operation...");
                }
                    

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            if (valid)
            {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                UsageStatsDataGatherer.UpdateStatTable(url, tableName, _config.GetConnectionString("Default"));

                stopwatch.Stop();
                Console.WriteLine($"Table Creation and population time: {stopwatch.ElapsedMilliseconds}ms");

            }
            
            return RedirectToPage("/Index");

        }

    }
}
