using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzerTests
{

    //Test methods from the PokemonType helper class

    [TestClass]
    public class PokemonTypeTests
    {
        /// <summary>
        /// Helper method to test for equality of two TypeEffectiveness instances
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool TypeEffectivenessEqual(TypeEffectiveness a, TypeEffectiveness b)
        {

            return (a.AttackingType == b.AttackingType &&
                    a.DefendingType1 == b.DefendingType1 &&
                    a.DefendingType2 == b.DefendingType2 &&
                    a.Effectiveness == b.Effectiveness);

        }

        [TestMethod]
        public void Test_GetTypeEffectiveness_Weakness()
        {

            //Tests if it can accurately return type matchups so its more of a test for the type chart as well
            var waterBeatsFire = PokemonType.GetTypeEffectiveness(PokemonType.TypeName.Water, PokemonType.TypeName.Fire);

            Assert.IsTrue(waterBeatsFire.Effectiveness == 2.0f);


        }

        [TestMethod]
        public void Test_GetTypeEffectiveness_Resistance()
        {

            var steelResistsNormal = PokemonType.GetTypeEffectiveness(PokemonType.TypeName.Normal, PokemonType.TypeName.Steel);
            Assert.IsTrue(steelResistsNormal.Effectiveness == 0.5f);

        }

        [TestMethod]
        public void Test_GetEffectivenessDual_DoubleWeaknesses()
        {

            //Test if dual types correctly stack their weaknesses
            var fireFlyingvsRock = PokemonType.GetTypeEffectivenessDual(PokemonType.TypeName.Rock,
                                                                        PokemonType.TypeName.Fire,
                                                                        PokemonType.TypeName.Flying);

            Assert.IsTrue(fireFlyingvsRock.Effectiveness == 4.0f);

        }

        [TestMethod]
        public void Test_GetEffectivenessDual_SingleWeakness()
        {

            //Test if dual types correctly return the effectiveness when when only one type contributes to a weakness
            var fireFightingvsWater = PokemonType.GetTypeEffectivenessDual(PokemonType.TypeName.Water,
                                                                           PokemonType.TypeName.Fire,
                                                                           PokemonType.TypeName.Fighting);

            Assert.IsTrue(fireFightingvsWater.Effectiveness == 2.0f);

        }

        [TestMethod]
        public void Test_GetTypeEffectivenessDual_TypeCanceling()
        {

            //Test if dual types correctly cancel out weaknesses and resistances
            var waterIcevsFire = PokemonType.GetTypeEffectivenessDual(PokemonType.TypeName.Fire,
                                                                      PokemonType.TypeName.Water,
                                                                      PokemonType.TypeName.Ice);

            Assert.IsTrue(waterIcevsFire.Effectiveness == 1.0f);

        }

        [TestMethod]
        public void Test_GetTypeEffectivenessDual_Immunity()
        {

            //Test immunity as a factor in dual types
            var normalGhostvsFighting = PokemonType.GetTypeEffectivenessDual(PokemonType.TypeName.Fighting,
                                                                             PokemonType.TypeName.Normal,
                                                                             PokemonType.TypeName.Ghost);

            Assert.IsTrue(normalGhostvsFighting.Effectiveness == 0.0f);

        }



        [TestMethod]
        public void Test_GetAllWeaknesses_SingleType()
        {

            //Test GetAllWeaknesses(type1, ?type2=null) returns accurate list

            List<TypeEffectiveness> actualweaknesses = PokemonType.GetAllWeaknesses(PokemonType.TypeName.Dark);

            List<TypeEffectiveness> expectedweaknesses = new List<TypeEffectiveness>();
            expectedweaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Fighting,
                                                         PokemonType.TypeName.Dark,
                                                         null,
                                                         2.0f));
            expectedweaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Bug,
                                                         PokemonType.TypeName.Dark,
                                                         null,
                                                         2.0f));
            expectedweaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Fairy,
                                                         PokemonType.TypeName.Dark,
                                                         null,
                                                         2.0f));

            if(actualweaknesses.Count != expectedweaknesses.Count)
            {
                Assert.Fail("The two collections do not have the same number of weaknesses");
            }

            for(int i = 0; i < expectedweaknesses.Count; i++)
            {

                bool isEqual = TypeEffectivenessEqual(actualweaknesses[i], expectedweaknesses[i]);
                Assert.IsTrue(isEqual, "Expected weaknesses did not match actual weaknesses");

            }

        }

        [TestMethod]
        public void Test_GetAllWeaknesses_DualType()
        {

            //Test GetAllWeaknesses(type1, type2) returns accurate list
            List<TypeEffectiveness> actualWeaknesses = PokemonType.GetAllWeaknesses(PokemonType.TypeName.Normal, PokemonType.TypeName.Steel);

            List<TypeEffectiveness> expectedWeaknesses = new List<TypeEffectiveness>();
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Fire,
                                                         PokemonType.TypeName.Normal,
                                                         PokemonType.TypeName.Steel,
                                                         2.0f));
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Fighting,
                                                         PokemonType.TypeName.Normal,
                                                         PokemonType.TypeName.Steel,
                                                         4.0f));
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Ground,
                                                         PokemonType.TypeName.Normal,
                                                         PokemonType.TypeName.Steel,
                                                         2.0f));

            if(actualWeaknesses.Count != expectedWeaknesses.Count)
            {
                Assert.Fail("Expected weakness count and actual weakness count do not match");
            }

            for(int i = 0;i < expectedWeaknesses.Count; i++)
            {

                bool isEqual = TypeEffectivenessEqual(expectedWeaknesses[i], actualWeaknesses[i]);
                Assert.IsTrue(isEqual);

            }

        }

        [TestMethod]
        public void Test_GetAllWeaknesses_PokemonOverload()
        {

            //Test accurate results for overload where it takes a TeamAnalyzer.DataTypes.Pokemon as input

            Pokemon charizard = new Pokemon();
            charizard.Name = "charizard";
            charizard.Type1 = PokemonType.TypeName.Fire;
            charizard.Type2 = PokemonType.TypeName.Flying;

            List<TypeEffectiveness> actualWeaknesses = PokemonType.GetAllWeaknesses(charizard);

            List<TypeEffectiveness> expectedWeaknesses = new List<TypeEffectiveness>();
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Water,
                                                         PokemonType.TypeName.Fire,
                                                         PokemonType.TypeName.Flying,
                                                         2.0f));
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Electric,
                                                         PokemonType.TypeName.Fire,
                                                         PokemonType.TypeName.Flying,
                                                         2.0f));
            expectedWeaknesses.Add(new TypeEffectiveness(PokemonType.TypeName.Rock,
                                                         PokemonType.TypeName.Fire,
                                                         PokemonType.TypeName.Flying,
                                                         4.0f));

            if (actualWeaknesses.Count != expectedWeaknesses.Count)
            {
                Assert.Fail("Expected weakness count and actual weakness count do not match");
            }

            for (int i = 0;i < expectedWeaknesses.Count; i++)
            {

                bool isEqual = TypeEffectivenessEqual(expectedWeaknesses[i], actualWeaknesses[i]);
                Assert.IsTrue(isEqual, "Expected weaknesses did not match actual weaknesses");

            }

        }

    }
}
