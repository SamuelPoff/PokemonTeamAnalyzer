using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DataAccess.Data;
using DataAccess.Models;

using UsageStatCollector.TeamAnalyzer.DataTypes;
using System.Text.Json;
using UsageStatCollector.Helpers;
using UsageStatCollector.TeamAnalyzer;
using PokemonTeamAnalyzerRazorUI.Models;

namespace PokemonTeamAnalyzerRazorUI.Pages
{
    public class TypeAnalyzerModel : PageModel
    {

        private readonly IPokemonData _data;
        public TypeAnalyzerModel(IPokemonData data)
        {
            _data = data;
        }



        [BindProperty]
        public Dictionary<Pokemon, List<TypeEffectiveness>> PokemonWeaknesses { get; set; } = new Dictionary<Pokemon, List<TypeEffectiveness>>();
        [BindProperty]
        public Dictionary<Pokemon, List<TypeEffectiveness>> PokemonResistances { get; set; } = new Dictionary<Pokemon, List<TypeEffectiveness>>();

        [BindProperty]
        public int[] TotalX4 { get; set; }
        [BindProperty]
        public int[] TotalX2 { get; set; }
        [BindProperty]
        public int[] TotalHalf { get; set; }
        [BindProperty]
        public int[] TotalFourth { get; set; }
        [BindProperty]
        public int[] TotalZero { get; set; }
        [BindProperty]
        public int[] TotalWeaknesses { get; set; }
        [BindProperty]
        public int[] TotalResistances { get; set; }

        [BindProperty]
        public int[] Offsets { get; set; }
        /// <summary>
        /// Contains a list of TypeName Id's that have a neutral difference between resistances and weaknesses
        /// </summary>
        [BindProperty]
        public List<int> OffsetNeutral { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more weaknesses than resistances represented in the given list of pokemon
        /// </summary>
        [BindProperty]
        public List<Tuple<int, int>> OffsetWeaknesses { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more resistances than weaknesses represented in the given list of pokemon
        /// </summary>
        [BindProperty]
        public List<Tuple<int, int>> OffsetResistances { get; set; }



        public void OnGet()
        {



        }

        public IActionResult OnGetCallPokemonSearchViewComponent(string searchString)
        {

            return ViewComponent("PokemonSearch", new { pokemonNameSearchString = searchString });

        }

        public IActionResult OnGetCallPokemonSearchBoxViewComponent()
        {

            return ViewComponent("PokemonSearchBox");

        }

        public IActionResult OnGetCallTypeAnalysisPokemonSelection(string pokemonName)
        {

            return ViewComponent("TypeAnalysisPokemonSelection", new { pokemonName = pokemonName });

        }

        public async Task<IActionResult> OnGetCallTypeAnalysisViewComponent(string pokemonNames)
        {

            TotalX4 = new int[PokemonType.NumberOfTypes];
            TotalX2 = new int[PokemonType.NumberOfTypes];
            TotalHalf = new int[PokemonType.NumberOfTypes];
            TotalFourth = new int[PokemonType.NumberOfTypes];
            TotalZero = new int[PokemonType.NumberOfTypes];

            List<Pokemon> pokemon = new List<Pokemon>();
            string[] pkmnNames = JsonSerializer.Deserialize<string[]>(pokemonNames);

            if (pkmnNames.Length <= 0)
            {

                return ViewComponent("TypeAnalysis");

            }

            //Query database for pokemon type info
            foreach (var pkmnName in pkmnNames)
            {

                PokemonModel? model = await _data.GetPokemonByName(pkmnName);
                if (model != null)
                {

                    //Fill out PokemonType info, checking for type errors and the like
                    Pokemon pkmn = new Pokemon();
                    pkmn.Name = pkmnName;
                    var type1 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type1);
                    if (type1 != null)
                    {
                        pkmn.Type1 = type1.Value;
                    }

                    if (model.Type2 != null)
                    {

                        var type2 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type2);
                        if (type2 != null)
                        {
                            pkmn.Type2 = type2.Value;
                        }

                    }

                    pokemon.Add(pkmn);

                }
                else
                {
                    Console.WriteLine("Couldnt find pokemon: " + pkmnName);
                }

            }

            if (pokemon.Count > 0)
            {

                TotalWeaknesses = TeamAnalyzer.GetTeamTypeWeaknesses(pokemon);
                TotalResistances = TeamAnalyzer.GetTeamTypeResistances(pokemon);

                TotalZero = new int[PokemonType.NumberOfTypes];
                TotalFourth = new int[PokemonType.NumberOfTypes];
                TotalHalf = new int[PokemonType.NumberOfTypes];
                TotalX2 = new int[PokemonType.NumberOfTypes];
                TotalX4 = new int[PokemonType.NumberOfTypes];

                //Gather individual type matchup data
                foreach (var pkmn in pokemon)
                {

                    List<TypeEffectiveness> weaknessesAndResistances = new List<TypeEffectiveness>();

                    //Get individual pokemon weaknesses and resistances for usage in the TypeFactorInformation pop-ups
                    //and for use here in the type summary
                    var weaknesses = PokemonType.GetAllWeaknesses(pkmn);
                    var resistances = PokemonType.GetAllResistances(pkmn);

                    PokemonWeaknesses.Add(pkmn, weaknesses);
                    PokemonResistances.Add(pkmn, resistances);

                    if(PokemonWeaknesses.Count <= 0)
                    {
                        Console.WriteLine("Its empty");
                    }
                    if (PokemonResistances.Count <= 0)
                    {
                        Console.WriteLine("Its empty");
                    }

                    weaknessesAndResistances.AddRange(weaknesses);
                    weaknessesAndResistances.AddRange(resistances);

                    foreach (var effectiveness in weaknessesAndResistances)
                    {
                        switch (effectiveness.Effectiveness)
                        {
                            case 0.0f:
                                TotalZero[((int)effectiveness.AttackingType)] += 1;
                                break;
                            case 0.25f:
                                TotalFourth[((int)effectiveness.AttackingType)] += 1;
                                break;
                            case 0.5f:
                                TotalHalf[((int)effectiveness.AttackingType)] += 1;
                                break;
                            case 2.0f:
                                TotalX2[((int)effectiveness.AttackingType)] += 1;
                                break;
                            case 4.0f:
                                TotalX4[((int)effectiveness.AttackingType)] += 1;
                                break;
                        }
                    }

                }


                Offsets = new int[PokemonType.NumberOfTypes];
                OffsetNeutral = new List<int>();
                OffsetWeaknesses = new List<Tuple<int, int>>();
                OffsetResistances = new List<Tuple<int, int>>();

                //Calculate offsets
                for (int i = 0; i < PokemonType.NumberOfTypes; i++)
                {

                    int offset = TotalWeaknesses[i] - TotalResistances[i];
                    Offsets[i] = offset;

                }

                //Populate respective arrays for easy data access in the cshtml file
                for (int i = 0; i < PokemonType.NumberOfTypes; i++)
                {

                    int offset = Offsets[i];
                    if (offset < 0)
                    {
                        OffsetResistances.Add(new Tuple<int, int>(i, Math.Abs(offset)));
                    }
                    else if (offset > 0)
                    {
                        OffsetWeaknesses.Add(new Tuple<int, int>(i, Math.Abs(offset)));
                    }
                    else
                    {
                        OffsetNeutral.Add(i);
                    }

                }

            }

            TypeAnalysisData analysisData = new TypeAnalysisData();
            analysisData.TotalX4 = TotalX4;
            analysisData.TotalX2 = TotalX2;
            analysisData.TotalHalf = TotalHalf;
            analysisData.TotalFourth = TotalFourth;
            analysisData.TotalZero = TotalZero;
            analysisData.TotalWeaknesses = TotalWeaknesses;
            analysisData.TotalResistances = TotalResistances;
            analysisData.OffsetNeutral = OffsetNeutral;
            analysisData.Offsets = Offsets;
            analysisData.OffsetResistances = OffsetResistances;
            analysisData.OffsetWeaknesses = OffsetWeaknesses;
            analysisData.PokemonWeaknesses = PokemonWeaknesses;
            analysisData.PokemonResistances = PokemonResistances;

            return ViewComponent("TypeAnalysis", new {data = analysisData});

        }
        
        public async Task<IActionResult> OnGetCallTypeFactorTooltipViewComponent(string pokemonNames, int typeId, string effectivenessString)
        {

            List<Pokemon> pokemon = new List<Pokemon>();
            List<Pokemon> filteredPokemon = new List<Pokemon>();
            string[] pkmnNames = JsonSerializer.Deserialize<string[]>(pokemonNames);

            foreach(var name in pkmnNames)
            {

                PokemonModel? model = await _data.GetPokemonByName(name);
                if(model != null)
                {

                    Pokemon pkmn = new Pokemon();
                    pkmn.Name = name;
                    var type1 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type1);
                    if(type1 != null)
                    {

                        pkmn.Type1 = type1.Value;

                    }
                    if(model.Type2 != null)
                    {
                        var type2 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type2);
                        if (type2 != null)
                        {

                            pkmn.Type2 = type2.Value;

                        }
                    }
                    

                    pokemon.Add(pkmn);

                }

            }


            float effectiveness = float.Parse(effectivenessString);

            Dictionary<Pokemon, List<TypeEffectiveness>> pokemonWeaknesses = new Dictionary<Pokemon, List<TypeEffectiveness>>();
            Dictionary<Pokemon, List<TypeEffectiveness>> pokemonResistances = new Dictionary<Pokemon, List<TypeEffectiveness>>();
            foreach(var pkmn in pokemon)
            {
                pokemonWeaknesses.Add(pkmn, PokemonType.GetAllWeaknesses(pkmn));
                pokemonResistances.Add(pkmn, PokemonType.GetAllResistances(pkmn));
            }
            

            if(effectiveness >= 2.0f) //Only check weaknesses if this is a weakness tooltip
            {

                foreach(var pair in pokemonWeaknesses)
                {

                    foreach(var weakness in pair.Value)
                    {
                        Console.WriteLine("weakness" + weakness.AttackingType);
                        if(weakness.AttackingType == (PokemonType.TypeName)typeId && weakness.Effectiveness == effectiveness)
                        {
                            filteredPokemon.Add(pair.Key);
                        }
                    }

                }

            }
            else if(effectiveness <= 0.5f) //Only check resistances if this is a resistance tooltip
            {

                foreach (var pair in pokemonResistances)
                {

                    foreach (var resistance in pair.Value)
                    {
                        if (resistance.AttackingType == (PokemonType.TypeName)typeId && resistance.Effectiveness == effectiveness)
                        {
                            filteredPokemon.Add(pair.Key);
                        }
                    }

                }

            }

            return ViewComponent("TypeFactorTooltip", new { pokemon = filteredPokemon, typeid = typeId, typeEffectiveness = effectiveness });

        }

    }
}
