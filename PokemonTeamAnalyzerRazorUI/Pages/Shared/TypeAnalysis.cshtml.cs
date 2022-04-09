using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UsageStatCollector.TeamAnalyzer;
using UsageStatCollector.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzerRazorUI.Pages.Shared
{
    public class TypeAnalysisModel : PageModel
    {

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

        public void OnGet(int pokemon1Type1Id, int pokemon1Type2Id, 
                          int pokemon2Type1Id, int pokemon2Type2Id,
                          int pokemon3Type1Id, int pokemon3Type2Id,
                          int pokemon4Type1Id, int pokemon4Type2Id,
                          int pokemon5Type1Id, int pokemon5Type2Id,
                          int pokemon6Type1Id, int pokemon6Type2Id)
        {

            List<Pokemon> pokemon = new List<Pokemon>();

            if(ValidateTypeId(pokemon1Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon1Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon1Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon1Type2Id;
                }

                pokemon.Add(pkmn);
            }
            if (ValidateTypeId(pokemon2Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon2Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon2Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon2Type2Id;
                }

                pokemon.Add(pkmn);
            }
            if (ValidateTypeId(pokemon3Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon3Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon3Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon3Type2Id;
                }

                pokemon.Add(pkmn);
            }
            if (ValidateTypeId(pokemon4Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon4Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon4Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon4Type2Id;
                }

                pokemon.Add(pkmn);
            }
            if (ValidateTypeId(pokemon5Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon5Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon5Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon5Type2Id;
                }

                pokemon.Add(pkmn);
            }
            if (ValidateTypeId(pokemon6Type1Id))
            {
                Pokemon pkmn = new Pokemon();
                pkmn.Type1 = (PokemonType.TypeName)pokemon6Type1Id;
                pkmn.Type2 = null;

                //Validate second type
                if (ValidateTypeId(pokemon1Type2Id))
                {
                    pkmn.Type2 = (PokemonType.TypeName)pokemon6Type2Id;
                }

                pokemon.Add(pkmn);
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
