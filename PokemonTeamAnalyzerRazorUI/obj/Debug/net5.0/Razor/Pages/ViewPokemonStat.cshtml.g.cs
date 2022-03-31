#pragma checksum "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e04663a0286b8bbd1e28b4ebcf6fb3cd88210f63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PokemonTeamAnalyzer.Pages.Pages_ViewPokemonStat), @"mvc.1.0.razor-page", @"/Pages/ViewPokemonStat.cshtml")]
namespace PokemonTeamAnalyzer.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\_ViewImports.cshtml"
using PokemonTeamAnalyzer;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e04663a0286b8bbd1e28b4ebcf6fb3cd88210f63", @"/Pages/ViewPokemonStat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc8250fdf459f95834ec102526ed58e09f2951d6", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ViewPokemonStat : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
  
    ViewData["Title"] = "View Pokemon Stat";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>View Pokemon Stat</h1>\r\n\r\n<h1>Usage Stats for ");
#nullable restore
#line 9 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
               Write(Model.PokemonName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n");
#nullable restore
#line 10 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
 foreach (var pkmnStat in Model.PokemonStats)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>\r\n        Gen ");
#nullable restore
#line 14 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
       Write(pkmnStat.Generation);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 14 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
                            Write(pkmnStat.Format);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </h3>\r\n");
            WriteLiteral("    <h5>Raw Count</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 19 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.RawCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Abilities</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 24 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.Abilities);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Items</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 29 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.Items);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Spreads</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 34 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.Spreads);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Moves</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 39 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.Moves);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Teammates</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 44 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.Teammates);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
            WriteLiteral("    <h5>Checks And Counters</h5>\r\n    <p>\r\n        ");
#nullable restore
#line 49 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"
   Write(pkmnStat.ChecksAndCounters);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 51 "C:\Users\Dad\source\repos\PokemonTeamAnalyzerSolution\PokemonTeamAnalyzerRazorUI\Pages\ViewPokemonStat.cshtml"

}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PokemonTeamAnalyzerRazorUI.Pages.ViewPokemonStatModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PokemonTeamAnalyzerRazorUI.Pages.ViewPokemonStatModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PokemonTeamAnalyzerRazorUI.Pages.ViewPokemonStatModel>)PageContext?.ViewData;
        public PokemonTeamAnalyzerRazorUI.Pages.ViewPokemonStatModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591