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
        public List<PokemonModel> PokemonList { get; set; }

        public ViewPokemonModel(IPokemonData data)
        {
            _data = data;

        }

        public async Task OnGet()
        {

            PokemonList = (List<PokemonModel>)await _data.GetAllPokemon();
            
        }
    }
}
