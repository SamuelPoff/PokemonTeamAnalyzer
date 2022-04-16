using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DataAccess.Data;
using DataAccess.Models;

namespace PokemonTeamAnalyzerRazorUI.Pages
{
    public class ViewPokemonModel : PageModel
    {

        IPokemonData _data;
        [BindProperty]
        public List<PokemonModel> PokemonList { get; set; }

        [BindProperty(SupportsGet=true)]
        public string SearchString { get; set; }

        public ViewPokemonModel(IPokemonData data)
        {
            _data = data;
        }

        public async Task OnGet()
        {

            
            
        }

        public IActionResult OnGetCallPokemonViewerSearchResultsViewComponent(string nameSearchString, string typeFilter, string typeFilter2, string abilityNameSearchString)
        {

            return ViewComponent("PokemonViewerSearchResults", new { nameSearchString=nameSearchString,
                                                                     typeFilter=typeFilter,
                                                                     typeFilter2=typeFilter2,
                                                                     abilityNameSearchString=abilityNameSearchString
                                                                    });

        }

    }
}
