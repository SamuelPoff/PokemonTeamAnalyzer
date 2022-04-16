using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using DataAccess.Data;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;
using PokemonTeamAnalyzer.Helpers;

namespace PokemonTeamAnalyzerRazorUI.ViewComponents
{
    [ViewComponent]
    public class TypeAnalysisPokemonSelectionViewComponent : ViewComponent
    {

        private readonly IPokemonData _data;
        public TypeAnalysisPokemonSelectionViewComponent(IPokemonData data)
        {
            _data = data;
        }

        public async Task<IViewComponentResult> InvokeAsync(string pokemonName)
        {

            pokemonName = DataAccess.Helpers.TextFormater.FormatString(pokemonName);
            PokemonModel? model = await _data.GetPokemonByName(pokemonName);

            return View(model);

        }

    }
}
