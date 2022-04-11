using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace PokemonTeamAnalyzerRazorUI.ViewComponents
{
    [ViewComponent]
    public class PokemonSearchBoxViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
