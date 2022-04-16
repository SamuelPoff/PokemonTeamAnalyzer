using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PokemonTeamAnalyzerRazorUI.Models;

using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Filters;
using DataAccess.Helpers;

namespace PokemonTeamAnalyzerRazorUI.ViewComponents
{
    [ViewComponent]
    public class PokemonViewerSearchResultsViewComponent : ViewComponent
    {
        private readonly IPokemonData _data;
        public PokemonViewerSearchResultsViewComponent(IPokemonData data)
        {

            _data = data;

        }

        public async Task<IViewComponentResult> InvokeAsync(string nameSearchString, string typeFilter, string typeFilter2, string abilityNameSearchString)
        {

            PokemonViewerSearchResultsModel model = new PokemonViewerSearchResultsModel();

            PokemonFilterData filterData = new PokemonFilterData();
            filterData.NameSearchString = TextFormater.FormatString(nameSearchString);
            filterData.AbilityNameSearchString = TextFormater.FormatString(abilityNameSearchString);
            filterData.TypeFilter = typeFilter;
            filterData.TypeFilter2 = typeFilter2;

            model.PokemonList = (List<PokemonModel>)await _data.GetPokemonByFilter(filterData);
            return View(model);

        }

    }
}
