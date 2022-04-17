using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using DataAccess.Data;
using DataAccess.SqlAccess;
using DataAccess.Models;

using System.IO;
using System.Threading.Tasks;

namespace DataAccessTests
{
    [TestClass]
    public class PokemonDataTests
    {

        IConfiguration config;
        public PokemonDataTests()
        {
            config = GetConfiguration();
        }

        /// <summary>
        /// Creates IConfiguration using appsettings.json in PokemonTeamAnalyzerRazorUI
        /// </summary>
        /// <returns></returns>
        public IConfiguration GetConfiguration()
        {

            //Creates config using appsettings.json from PokemonTeamAnalyzerRazorUI

            IConfiguration config;
            string appsettingsDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\PokemonTeamAnalyzerRazorUI";

            var configBuilder = new ConfigurationBuilder().SetBasePath(appsettingsDirectory).AddJsonFile("appsettings.json");

            config = configBuilder.Build();
            return config;

        }

        [TestMethod]
        public async Task Test_GetPokemonByName()
        {

            //Test basic GetPokemonByName()
            PokemonData data = new PokemonData(new SqlDataAccess( config ));

            PokemonModel? model = await data.GetPokemonByName("charizard");
            Assert.IsTrue(model.Name == "charizard");
            Assert.IsTrue(model.Type1 == "FIRE");
            Assert.IsTrue(model.Type2 == "FLYING");

        }

        [TestMethod]
        public async Task Test_GetPokemonByName_InvalidName()
        {

            //Test invalid name in GetPokemonByName()
            PokemonData data = new PokemonData(new SqlDataAccess( config ));

            PokemonModel? model = await data.GetPokemonByName("abcdefg");

            Assert.IsNull(model);

        }
    }
}
