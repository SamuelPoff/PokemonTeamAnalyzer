using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DataAccess.Data;
using DataAccess.Models;

using PokemonTeamAnalyzer;

namespace PokemonTeamAnalyzerRazorUI.Pages
{
    public class ViewPokemonStatModel : PageModel
    {

        [BindProperty(SupportsGet=true)]
        public string PokemonName { get; set; }

        public List<PokemonStatModel> PokemonStats { get; set; }

        private readonly IPokemonUsageData usageData;
        private readonly IPokemonData pokemonData;

        public ViewPokemonStatModel(IPokemonUsageData usageData, IPokemonData pokemonData)
        {

            this.usageData = usageData;
            this.pokemonData = pokemonData;

        }

        public async Task OnGet()
        {

            PokemonModel pkmnModel = await pokemonData.GetPokemonByName(PokemonName);
            if (pkmnModel != null)
            {

                List<PokemonStatModel> pkmnStatModels = (List<PokemonStatModel>)await usageData.GetAllPokemonStatsByPkmnId(pkmnModel.ID);
                if (pkmnStatModels.Count >= 0)
                {
                    PokemonStats = pkmnStatModels;
                }
                else
                {
                    Console.WriteLine("Nothing Found");
                }

            }
            else
            {
                Console.WriteLine("Pokemon isnt in database");
            }
        }

        public string GetFormatedStatBlockText(string statBlockCsvText)
        {

            string formattedText = "";

            var statList = Parser.ParseUsageSection(statBlockCsvText);
            foreach(Tuple<string, float> stat in statList)
            {

                formattedText += $"{stat.Item1}: {stat.Item2}%\n";

            }

            return formattedText;

        }

    }
}
