using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UsageStatCollector;

using DataAccess;
using DataAccess.SqlAccess;
using DataAccess.Data;

namespace PokemonTeamAnalyzer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IPokemonData, PokemonData>();
            services.AddSingleton<IPokemonUsageData, PokemonUsageData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            UsageStatCollector.Collector.CollectorConfigure(app.ApplicationServices.GetService<IPokemonUsageData>());
            UsageStatCollector.Parser.ParserConfigure(app.ApplicationServices.GetService<IPokemonData>());
            UsageStatCollector.PokemonDataCollector.PokemonDataCollectorConfigure(app.ApplicationServices.GetService<IPokemonData>());

            //Seed PokemonData
            var pokemonDataAccess = app.ApplicationServices.GetService<IPokemonData>();
            bool pokemonTableEmpty = pokemonDataAccess.IsEmpty();
            if (pokemonTableEmpty)
            {
                PokemonDataCollector.CollectPokemonData().Wait();
            }
            else
            {
                Console.WriteLine("Pokemon Data already collected.");
            }

            //Seed or update PokemonUsageData
            var usageDA = app.ApplicationServices.GetService<IPokemonUsageData>();
            bool pokemonUsageTableEmpty = usageDA.IsEmpty();
            if (pokemonUsageTableEmpty)
            {
                Collector.SeedUsageStats().Wait();
            }
            else
            {
                //Collector.UpdateUsageStats().Wait();
            }
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
