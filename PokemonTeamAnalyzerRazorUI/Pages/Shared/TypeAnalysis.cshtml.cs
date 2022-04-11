using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DataAccess.Data;
using DataAccess.Models;

using UsageStatCollector.TeamAnalyzer;
using UsageStatCollector.TeamAnalyzer.DataTypes;

using UsageStatCollector.Helpers;

namespace PokemonTeamAnalyzerRazorUI.Pages.Shared
{
    public class TypeAnalysisModel : PageModel
    {

        private readonly IPokemonData _data;
        public TypeAnalysisModel(IPokemonData data)
        {
            _data = data;
        }

        public int[] TotalX4 { get; set; }
        public int[] TotalX2 { get; set; }
        public int[] TotalHalf { get; set; }
        public int[] TotalFourth { get; set; }
        public int[] TotalZero { get; set; }
        public int[] TotalWeaknesses { get; set; }
        public int[] TotalResistances { get; set; }

        public int[] Offsets { get; set; }
        /// <summary>
        /// Contains a list of TypeName Id's that have a neutral difference between resistances and weaknesses
        /// </summary>
        public List<int> OffsetNeutral { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more weaknesses than resistances represented in the given list of pokemon
        /// </summary>
        public List<Tuple<int, int>> OffsetWeaknesses { get; set; }
        /// <summary>
        /// Contains a tuple(TypeName Id, quantity) of pokemon types that have more resistances than weaknesses represented in the given list of pokemon
        /// </summary>
        public List<Tuple<int, int>> OffsetResistances { get; set; }

        public async Task OnGet(string pokemonNames)
        {

            TotalX4 = new int[PokemonType.NumberOfTypes];
            TotalX2 = new int[PokemonType.NumberOfTypes];
            TotalHalf = new int[PokemonType.NumberOfTypes];
            TotalFourth = new int[PokemonType.NumberOfTypes];
            TotalZero = new int[PokemonType.NumberOfTypes];

            List<Pokemon> pokemon = new List<Pokemon>();
            string[] pkmnNames = JsonSerializer.Deserialize<string[]>(pokemonNames);

            if(pkmnNames.Length <= 0)
            {
                Console.WriteLine("No names sent");
            }

            //Query database for pokemon type info
            foreach(var pkmnName in pkmnNames)
            {

                PokemonModel? model = await _data.GetPokemonByName(pkmnName);
                if(model != null)
                {

                    //Fill out PokemonType info, checking for type errors and the like
                    Pokemon pkmn = new Pokemon();
                    var type1 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type1);
                    if(type1 != null)
                    {
                        pkmn.Type1 = type1.Value;
                    }

                    if(model.Type2 != null)
                    {

                        var type2 = PokemonTypeHelpers.ConvertDBTypeStringToPokemonType(model.Type2);
                        if(type2 != null)
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

            if(pokemon.Count > 0)
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
                    weaknessesAndResistances.AddRange(PokemonType.GetAllWeaknesses(pkmn));
                    weaknessesAndResistances.AddRange(PokemonType.GetAllResistances(pkmn));

                    foreach(var effectiveness in weaknessesAndResistances)
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
                for(int i = 0;i < PokemonType.NumberOfTypes; i++)
                {

                    int offset = TotalWeaknesses[i] - TotalResistances[i];
                    Offsets[i] = offset;

                }

                //Populate respective arrays for easy data access in the cshtml file
                for(int i = 0; i < PokemonType.NumberOfTypes; i++)
                {

                    int offset = Offsets[i];
                    if(offset < 0)
                    {
                        OffsetResistances.Add(new Tuple<int, int>(i, Math.Abs(offset)));
                    }else if(offset > 0)
                    {
                        OffsetWeaknesses.Add(new Tuple<int, int>(i, Math.Abs(offset)));
                    }
                    else
                    {
                        OffsetNeutral.Add(i);
                    }

                }

            }

        }

        public bool ValidateTypeId(int id)
        {

            return id != 18 && id != -1;

        }
    }
}
