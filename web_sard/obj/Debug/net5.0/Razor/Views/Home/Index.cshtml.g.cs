#pragma checksum "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afc9a074172f90202c1621a835798c5c3dfd99bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
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
#line 1 "D:\Source\Repos\web_sardGitHub\web_sard\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sardGitHub\web_sard\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Source\Repos\web_sardGitHub\web_sard\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afc9a074172f90202c1621a835798c5c3dfd99bc", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<web_sard.Models.tbls.home.mainclass>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "داشبورد";

    web_db.TblSalMali sal= ViewBag.fromyear;


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">خوش آمدید</h1>\r\n\r\n</div>\r\n\r\n");
#nullable restore
#line 15 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
  

           var now = DateTime.Now.Date;
    var dd = new Dictionary<string, IEnumerable<web_sard.Models.tbls.home.countport>> {
         {"امروز",Model.countports.Where(a => a.date==now)  },
         {"دیروز",Model.countports.Where(a => a.date==now.AddDays(-1)) },
         {"هفته اخیر",Model.countports.Where(a => a.date>=now.AddDays(-7)) },
         {"ماه اخیر",Model.countports.Where(a => a.date>=now.AddMonths(-1)) },
         {"امسال",Model.countports },
     };




#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n\r\n\r\n");
#nullable restore
#line 33 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
     foreach (var f in Model.countports.Select(a => a.location).Distinct())
    {
        var loc = cl._ListLocation.SingleOrDefault(a => a.Id == f);
        if (loc==null)
        {
            break;
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-md-6\">\r\n            <div class=\"card\">\r\n                <div class=\"card-body\">\r\n                    <h4 class=\"card-title\">");
#nullable restore
#line 43 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                                      Write(loc.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <ul class=\"list-group\">\r\n");
#nullable restore
#line 45 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                         foreach (var itemm in dd)
                        {


                            var counts = itemm.Value.Where(a => a.location == f);


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"list-group-item disabled\">");
#nullable restore
#line 51 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                                                            Write(itemm.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 52 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                             foreach (var item in counts.GroupBy(a => new { a.KindTitle, a.Kindcode }))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li class=\"list-group-item\">\r\n                                    ");
#nullable restore
#line 55 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                               Write(item.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-car\"></i>\r\n                                    ");
#nullable restore
#line 56 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                               Write(item.Sum(a => a.PackingCount).ToKilo());

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-boxes\"></i>\r\n                                    ");
#nullable restore
#line 57 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                               Write(item.Sum(a => a.WeightNet).ToKilo());

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-box-open\"></i>\r\n                                    <span");
            BeginWriteAttribute("class", " class=\"", 1943, "\"", 2079, 4);
            WriteAttributeValue("", 1951, "badge", 1951, 5, true);
            WriteAttributeValue(" ", 1956, "bg-", 1957, 4, true);
#nullable restore
#line 58 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
WriteAttributeValue("", 1960, web_sard.Models.tbls.portage.kindPortage.listkindcontract.Single(a=>a.code==item.Key.Kindcode).classcolor, 1960, 108, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2068, "float-left", 2069, 11, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 58 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                                                                                                                                                                              Write(item.Key.KindTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n                                </li>\r\n");
#nullable restore
#line 61 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"

                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"
                             


                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </ul>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 70 "D:\Source\Repos\web_sardGitHub\web_sard\Views\Home\Index.cshtml"


    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<web_sard.Models.tbls.home.mainclass> Html { get; private set; }
    }
}
#pragma warning restore 1591
