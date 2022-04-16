using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTeamAnalyzer.TeamAnalyzer.DataTypes;

namespace PokemonTeamAnalyzer.Helpers
{
    public static class PokemonTypeHelpers
    {

        //Really regretting storing the types as strings, couldve been so much easier if they were integer id's
        //wont make that mistake again
        public static PokemonType.TypeName? ConvertDBTypeStringToPokemonType(string type)
        {

            type = type.Trim();

            switch (type)
            {

                case "NORMAL":
                    return PokemonType.TypeName.Normal;
                    break;
                case "FIRE":
                    return PokemonType.TypeName.Fire;
                    break;
                case "WATER":
                    return PokemonType.TypeName.Water;
                    break;
                case "GRASS":
                    return PokemonType.TypeName.Grass;
                    break;
                case "ELECTRIC":
                    return PokemonType.TypeName.Electric;
                    break;
                case "ICE":
                    return PokemonType.TypeName.Ice;
                    break;
                case "FIGHTING":
                    return PokemonType.TypeName.Fighting;
                    break;
                case "POISON":
                    return PokemonType.TypeName.Poison;
                    break;
                case "GROUND":
                    return PokemonType.TypeName.Ground;
                    break;
                case "FLYING":
                    return PokemonType.TypeName.Flying;
                    break;
                case "PSYCHIC":
                    return PokemonType.TypeName.Psychic;
                    break;
                case "BUG":
                    return PokemonType.TypeName.Bug;
                    break;
                case "ROCK":
                    return PokemonType.TypeName.Rock;
                    break;
                case "GHOST":
                    return PokemonType.TypeName.Ghost;
                    break;
                case "DRAGON":
                    return PokemonType.TypeName.Dragon;
                    break;
                case "DARK":
                    return PokemonType.TypeName.Dark;
                    break;
                case "STEEL":
                    return PokemonType.TypeName.Steel;
                    break;
                case "FARIY":
                    return PokemonType.TypeName.Fairy;
                    break;

            }

            return null;

        }

    }
}
