using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageStatCollector.TeamAnalyzer.DataTypes
{
    public static class PokemonType
    {

        public enum TypeName{ 
            
            Normal = 0,
            Fire = 1,
            Water = 2,
            Grass = 3,
            Electric = 4,
            Ice = 5,
            Fighting = 6,
            Poison = 7,
            Ground = 8,
            Flying = 9,
            Psychic = 10,
            Bug = 11,
            Rock = 12,
            Ghost = 13,
            Dragon = 14,
            Dark = 15,
            Steel = 16,
            Fairy = 17
        
        }

        public static int NumberOfTypes = 18;
        //[OffensiveType, DefensiveType]
        private static float[,] TypeChart = { 
                                             {1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 0, 1, 1, 0.5f, 1}, //Normal
                                             {1, 0.5f, 0.5f, 2, 1, 2, 1, 1, 1, 1, 1, 2, 0.5f, 1, 0.5f, 1, 2, 1}, //Fire
                                             {1, 2, 0.5f, 0.5f, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 0.5f, 1, 1, 1}, //Water
                                             {1, 0.5f, 2, 0.5f, 1, 1, 1, 0.5f, 2, 0.5f, 1, 0.5f, 2, 1, 0.5f, 1, 0.5f, 1 }, //Grass
                                             {1, 1, 2, 0.5f, 0.5f, 1, 1, 1, 0, 2, 1, 1, 1, 1, 0.5f, 1, 1, 1 }, //Electric
                                             {1, 0.5f, 0.5f, 2, 1, 0.5f, 1, 1, 2, 2, 1, 1, 1, 1, 2, 1, 0.5f, 1 }, //Ice
                                             {2, 1, 1, 1, 1, 2, 1, 0.5f, 1, 0.5f, 0.5f, 0.5f, 2, 0, 1, 2, 2, 0.5f }, //Fighting
                                             {1, 1, 1, 2, 1, 1, 1, 0.5f, 0.5f, 1, 1, 1, 0.5f, 0.5f, 1, 1, 0, 2 }, //Poison
                                             {1, 2, 1, 0.5f, 2, 1, 1, 2, 1, 0, 1, 0.5f, 2, 1, 1, 1, 2, 1 }, //Ground
                                             {1, 1, 1, 2, 0.5f, 1, 2, 1, 1, 1, 1, 2, 0.5f, 1, 1, 1, 0.5f, 1 }, //Flying
                                             {1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 0.5f, 1, 1, 1, 1, 0, 0.5f, 1 }, //Psychic
                                             {1, 0.5f, 1, 2, 1, 1, 0.5f, 0.5f, 1, 0.5f, 2, 1, 1, 0.5f, 1, 2, 0.5f, 0.5f }, //Bug
                                             {1, 2, 1, 1, 1, 2, 0.5f, 1, 0.5f, 2, 1, 2, 1, 1, 1, 1, 0.5f, 1}, //Rock
                                             {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 0.5f, 1, 1 }, //Ghost
                                             {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0.5f, 0}, //Dragon
                                             {1, 1, 1, 1, 1, 1, 0.5f, 1, 1, 1, 2, 1, 1, 2, 1, 0.5f, 1, 0.5f }, //Dark
                                             {1, 0.5f, 0.5f, 1, 0.5f, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0.5f, 2 }, //Steel
                                             {1, 0.5f, 1, 1, 1, 1, 2, 0.5f, 1, 1, 1, 1, 1, 1, 2, 2, 0.5f, 1 } //Fairy
        };

        /// <summary>
        /// Returns the effectiveness of the given type matchup
        /// </summary>
        /// <param name="attackingType"></param>
        /// <param name="defendingType"></param>
        /// <returns>new TypeEffectiveness class containing attacking type, defending type, and the damage multiplier</returns>
        public static TypeEffectiveness GetTypeEffectiveness(PokemonType.TypeName attackingType, PokemonType.TypeName defendingType)
        {

            return new TypeEffectiveness(attackingType, defendingType, null, TypeChart[((int)attackingType), ((int)defendingType)]);

        }
        /// <summary>
        /// Returns the effectiveness of and given dual type matchup
        /// </summary>
        /// <param name="attackingType"></param>
        /// <param name="defendingType"></param>
        /// <param name="defendingType2"></param>
        /// <returns></returns>
        public static TypeEffectiveness GetTypeEffectivenessDual(PokemonType.TypeName attackingType, PokemonType.TypeName defendingType, PokemonType.TypeName defendingType2)
        {
            float effectiveness1 = TypeChart[((int)attackingType), ((int)defendingType)];
            float effectiveness2 = TypeChart[((int)attackingType), ((int)defendingType2)];

            return new TypeEffectiveness(attackingType, defendingType, defendingType2, effectiveness1 * effectiveness2);

        }

        /// <summary>
        /// Returns a list of all weaknesses of a single type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<TypeEffectiveness> GetAllWeaknesses(PokemonType.TypeName type)
        {

            List<TypeEffectiveness> typeEffectivenesses = new List<TypeEffectiveness>();

            for(int i = 0; i < NumberOfTypes; i++)
            {

                float effectiveness = TypeChart[i, ((int)type)];

                if(effectiveness >= 2)
                {
                    PokemonType.TypeName attackingType = (PokemonType.TypeName)i;
                    typeEffectivenesses.Add(new TypeEffectiveness(attackingType, type, null, effectiveness));
                }

            }

            return typeEffectivenesses;

        }
        /// <summary>
        /// Returns a list of all resistances of a single type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<TypeEffectiveness> GetAllResistances(PokemonType.TypeName type) {

            List<TypeEffectiveness> typeEffectivenesses = new List<TypeEffectiveness>();

            for (int i = 0; i < NumberOfTypes; i++) {

                float effectiveness = TypeChart[i, ((int)type)];

                if(effectiveness <= 0.5f)
                {

                    PokemonType.TypeName attackingType = (PokemonType.TypeName)i;
                    typeEffectivenesses.Add(new TypeEffectiveness(attackingType, type, null, effectiveness));

                }
            
            }

            return typeEffectivenesses;

        }
        /// <summary>
        /// Returns a list of all weaknesses of a dual type
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static List<TypeEffectiveness> GetAllWeaknessesDual(PokemonType.TypeName type1, PokemonType.TypeName type2)
        {

            List<TypeEffectiveness> typeEffectivenesses = new List<TypeEffectiveness>();

            for(int i = 0; i < NumberOfTypes; i++)
            {

                float type1Effectiveness = TypeChart[i, ((int)type1)];
                float type2Effectiveness = TypeChart[i, ((int)type2)];

                float dualEffectiveness = type1Effectiveness * type2Effectiveness;

                if(dualEffectiveness >= 2)
                {

                    PokemonType.TypeName attackingType = (PokemonType.TypeName)i;
                    typeEffectivenesses.Add(new TypeEffectiveness(attackingType, type1, type2, dualEffectiveness) );

                }

            }

            return typeEffectivenesses;

        }
        /// <summary>
        /// Returns a list of all resistances of a dual type
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static List<TypeEffectiveness> GetAllResistancesDual(PokemonType.TypeName type1, PokemonType.TypeName type2)
        {

            List<TypeEffectiveness> typeEffectivenesses = new List<TypeEffectiveness>();

            for (int i = 0; i < NumberOfTypes; i++)
            {

                float type1Effectiveness = TypeChart[i, ((int)type1)];
                float type2Effectiveness = TypeChart[i, ((int)type2)];

                float dualEffectiveness = type1Effectiveness * type2Effectiveness;

                if (dualEffectiveness <= 0.5f)
                {

                    PokemonType.TypeName attackingType = (PokemonType.TypeName)i;
                    typeEffectivenesses.Add(new TypeEffectiveness(attackingType, type1, type2, dualEffectiveness));

                }

            }

            return typeEffectivenesses;

        }

    }



    /// <summary>
    /// Contains information about a specific type matchup
    /// </summary>
    public class TypeEffectiveness
    {

        PokemonType.TypeName AttackingType;
        PokemonType.TypeName DefendingType1;
        PokemonType.TypeName? DefendingType2;
        float Effectiveness;

        public TypeEffectiveness(PokemonType.TypeName attType, PokemonType.TypeName defType, PokemonType.TypeName? defType2, float eff)
        {
            AttackingType = attType;
            DefendingType1 = defType;
            DefendingType2 = defType2;
            Effectiveness = eff;
        }

        public void Print()
        {
            Console.WriteLine("Attacking Type: " + AttackingType);
            Console.WriteLine("Defending Type1: " + DefendingType1);
            Console.WriteLine("Defending Type2: " + DefendingType2);
            Console.WriteLine("Effectiveness " + Effectiveness);
        }

    }

}
