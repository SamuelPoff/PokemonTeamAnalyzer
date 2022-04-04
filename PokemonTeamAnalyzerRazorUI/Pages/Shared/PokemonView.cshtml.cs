using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DataAccess.Models;
using DataAccess.Data;
using DataAccess.Filters;

namespace PokemonTeamAnalyzerRazorUI.Pages.Shared
{
    public class PokemonViewModel : PageModel
    {

        private readonly IPokemonData _data;
        public int NumImagesToLoad { get; set; } = 8;
        public List<PokemonModel> PokemonList { get; set; }

        public PokemonViewModel(IPokemonData data)
        {
            _data = data;
        }

        public async Task OnGet(string searchString, string typeFilter, string typeFilter2, string abilityNameFilter)
        {

            PokemonFilterData filterData = new PokemonFilterData();
            filterData.NameSearchString = searchString;
            filterData.AbilityNameSearchString = abilityNameFilter;
            filterData.TypeFilter = typeFilter;
            filterData.TypeFilter2 = typeFilter2;

            PokemonList = (List<PokemonModel>)await _data.GetPokemonByFilter(filterData);
            
        }
    }
}
