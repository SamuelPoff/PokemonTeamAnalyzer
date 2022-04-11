using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Models;
using DataAccess.Data;

using Microsoft.AspNetCore.Mvc;


namespace PokemonTeamAnalyzerRazorUI.Pages.ViewComponents
{
    [ViewComponent]
    public class PokemonSearchViewComponent : ViewComponent
    {

        private const int ResultsLimit = 10;

        private readonly IPokemonData _data;
        public PokemonSearchViewComponent(IPokemonData data)
        {
            _data = data;
        }

        public async Task<IViewComponentResult> InvokeAsync(string pokemonNameSearchString)
        {

            List<PokemonModel> pokemon = (List<PokemonModel>)await _data.GetPokemonByNameSearch(pokemonNameSearchString, ResultsLimit);
            return View(pokemon);

        }

    }
}
