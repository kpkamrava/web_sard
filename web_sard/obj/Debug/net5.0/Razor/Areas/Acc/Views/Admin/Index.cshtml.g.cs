#pragma checksum "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc0c2c529885f21f483fc0926283318fa226a872"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Acc_Views_Admin_Index), @"mvc.1.0.view", @"/Areas/Acc/Views/Admin/Index.cshtml")]
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
#line 1 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc0c2c529885f21f483fc0926283318fa226a872", @"/Areas/Acc/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Areas/Acc/Views/_ViewImports.cshtml")]
    public class Areas_Acc_Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "تنظیمات";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row justify-content-center\">\r\n    <div class=\"col-md-6\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 9 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.Mali_KindOT, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 10 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.Mali_UrlOT, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 11 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.Mali_UserOT, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 12 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Acc\Views\Admin\Index.cshtml"
       Write(Html._Conf(web_db.TblConf.KeyEnum.Mali_PassOT, "string"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n   \r\n\r\n\r\n        </div>\r\n    </div>\r\n</div> ");
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
