using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Models;

namespace PokemonTeamAnalyzerRazorUI.Models
{
    public class PokemonViewerSearchResultsModel
    {

        public List<PokemonModel> PokemonList { get; set; }
        public int NumImagesToLoad { get; set; } = 8;

    }
}
