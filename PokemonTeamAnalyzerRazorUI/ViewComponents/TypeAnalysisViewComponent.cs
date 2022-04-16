using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DataAccess.Data;
using DataAccess.Models;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;
using PokemonTeamAnalyzerRazorUI.Models;

namespace PokemonTeamAnalyzerRazorUI.ViewComponents
{

    [ViewComponent]
    public class TypeAnalysisViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(TypeAnalysisData data)
        {

            return View(data);

        }


    }
}
