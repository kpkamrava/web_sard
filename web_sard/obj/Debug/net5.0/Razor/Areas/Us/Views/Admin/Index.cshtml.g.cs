#pragma checksum "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c9cd7acd07e568d87cef4fbccd28c1683f54878"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Us_Views_Admin_Index), @"mvc.1.0.view", @"/Areas/Us/Views/Admin/Index.cshtml")]
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
#line 1 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c9cd7acd07e568d87cef4fbccd28c1683f54878", @"/Areas/Us/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Areas/Us/Views/_ViewImports.cshtml")]
    public class Areas_Us_Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(" \r\n");
#nullable restore
#line 2 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<h4>TblConf</h4>\r\n<hr />\r\n\r\n<div class=\"row justify-content-center\">\r\n    <div class=\"col-md-6\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 14 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_SefareshMoshtarianEnable, "bool"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 15 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_UrlSite, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 16 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_WeightViewEnable, "bool"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 17 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_WeightZarib, "int"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_WeightVahed, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 19 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_WeightMax, "int"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 20 "D:\Source\Repos\web_sard\web_sard\Areas\Us\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.US_WeightMin, "int"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
