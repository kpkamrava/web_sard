#pragma checksum "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e2450e1edc851cc242cbffa6d4085af604c9739"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Acc_Views__ViewStart), @"mvc.1.0.view", @"/Areas/Acc/Views/_ViewStart.cshtml")]
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
#line 1 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e2450e1edc851cc242cbffa6d4085af604c9739", @"/Areas/Acc/Views/_ViewStart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Areas/Acc/Views/_ViewImports.cshtml")]
    public class Areas_Acc_Views__ViewStart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n\r\n \r\n\r\n\r\n\r\n\r\n");
#nullable restore
#line 12 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
     if (Context.Request.IsAjaxRequest())
    {
        Layout = null;
    }
    else
    {
        Layout = "_LayoutSP";
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
 if (Context.Request.IsAjaxRequest())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        renderAjax(\"#ajaxModal\");\r\n\r\n    </script>\r\n");
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
     if (string.IsNullOrWhiteSpace(ViewBag.txt) == false)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"alert alert-info\">\r\n            <strong>پیغام!</strong> ");
#nullable restore
#line 31 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
                               Write(ViewBag.txt);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
     if (string.IsNullOrWhiteSpace(ViewBag.error) == false)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"alert alert-danger\">\r\n            <strong>خطا!</strong> ");
#nullable restore
#line 38 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
                             Write(ViewBag.error);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 40 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\_ViewStart.cshtml"
     


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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
