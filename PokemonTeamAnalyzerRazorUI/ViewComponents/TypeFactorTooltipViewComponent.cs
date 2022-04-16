using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzerRazorUI.ViewComponents
{
    [ViewComponent]
    public class TypeFactorTooltipViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(List<Pokemon> pokemon, int typeid, float typeEffectiveness)
        {

            return View(pokemon);

        }

    }
}
