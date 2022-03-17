using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PokemonTeamAnalyzer.Models;
using DataAccess.Data;
using DataAccess.Models;

namespace PokemonTeamAnalyzer.Pages.Forms
{
    public class AddPokemonModel : PageModel
    {

        private readonly IPokemonData _data;
        public AddPokemonModel(IPokemonData data)
        {
            _data = data;
        }

        [BindProperty]
        public DataAccess.Models.PokemonModel Pokemon { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if(ModelState.IsValid == false)
            {
                return Page();
            }

            _data.InsertPokemon(Pokemon);

            return RedirectToPage("/Index");

        }

    }
}
