using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PokemonTeamAnalyzerRazorUI.Pages
{
    public class TypeAnalyzerModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetCallPokemonSearchViewComponent(string searchString)
        {

            return ViewComponent("PokemonSearch", new { pokemonNameSearchString = searchString });

        }

        public IActionResult OnGetCallPokemonSearchBoxViewComponent()
        {

            return ViewComponent("PokemonSearchBox");

        }

    }
}
