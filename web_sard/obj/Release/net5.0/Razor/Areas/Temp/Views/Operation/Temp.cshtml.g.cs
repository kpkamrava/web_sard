#pragma checksum "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5edef28bc8ff5334910b86a61729ac9a69717905"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Temp_Views_Operation_Temp), @"mvc.1.0.view", @"/Areas/Temp/Views/Operation/Temp.cshtml")]
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
#line 1 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5edef28bc8ff5334910b86a61729ac9a69717905", @"/Areas/Temp/Views/Operation/Temp.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Areas/Temp/Views/_ViewImports.cshtml")]
    public class Areas_Temp_Views_Operation_Temp : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<web_db._temp.TblTemp>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "addTempRow", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ismodal", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
  
    ViewData["Title"] = "وضعیت روز";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card\">\r\n    <div>\r\n        <h4>");
#nullable restore
#line 8 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
       Write(Model._Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n        <hr />\r\n        <div class=\"row\">\r\n\r\n\r\n\r\n\r\n            <div class=\"col-sm-10\">\r\n");
#nullable restore
#line 16 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                 if (Model.FkuserTaiid.HasValue)
                {

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
            Write(Html.DisplayFor(model => model.UserTaiid.Title));

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                 }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n\r\n");
#nullable restore
#line 20 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
             if (Model.Txt.IsEmpty() == false)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 23 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
               Write(Html.DisplayNameFor(model => model.Txt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 26 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
               Write(Html.DisplayFor(model => model.Txt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 28 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <div class=\" col-sm-3\">\r\n                <div class=\" card\">\r\n");
#nullable restore
#line 33 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                     if (Model.Sign != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <img");
            BeginWriteAttribute("src", " src=\"", 868, "\"", 899, 1);
#nullable restore
#line 35 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue("", 874, Model.Sign.imgToBase64(), 874, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\" />\r\n");
#nullable restore
#line 36 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <div class=\"card-header\">\r\n");
#nullable restore
#line 40 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                     if (Model.FkuserTaiid.HasValue == false)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                   Write(Html._Sign(Url.Action("addDoc", "Operation", new { id = Model.Id, kind = "Sign" }), "امضای ثبت کننده", "btn-primary"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                                                                                               
                    } 

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div> \r\n\r\n                    <div class=\"card-body\">\r\n                        ");
#nullable restore
#line 47 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                   Write(Html.DisplayNameFor(model => model.FkuserAdd));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :    ");
#nullable restore
#line 47 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                       Write(Html.DisplayFor(model => model.UserAdd.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\" col-sm-3\">\r\n                <div class=\" card\">\r\n");
#nullable restore
#line 55 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                     if (Model.Sign != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <img");
            BeginWriteAttribute("src", " src=\"", 1694, "\"", 1730, 1);
#nullable restore
#line 57 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue("", 1700, Model.SignTaiid.imgToBase64(), 1700, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\" />\r\n");
#nullable restore
#line 58 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <div class=\"card-header\">\r\n");
#nullable restore
#line 62 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                     if (User._getRolAny(_UserRol._Rolls.TempAdmin))
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                   Write(Html._Sign(Url.Action("addDoc", "Operation", new { id = Model.Id, kind = "SignTaiid" }), "امضای تایید کننده", "btn-success"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                         if (Model.FkuserTaiid.HasValue)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 2186, "\"", 2276, 1);
#nullable restore
#line 67 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue("", 2193, Url.Action("addDoc", "Operation", new { id = Model.Id, kind = "RemoveSignTaiid" }), 2193, 83, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning\">حذف تایید</a>\r\n");
#nullable restore
#line 68 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"

                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                         
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n\r\n\r\n                    <div class=\"card-body \">\r\n                        ");
#nullable restore
#line 76 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                   Write(Html.DisplayNameFor(model => model.FkuserTaiid));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :    ");
#nullable restore
#line 76 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                         Write(Html.DisplayFor(model => model.UserTaiid.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n        </div>\r\n    </div>\r\n    <div>\r\n       \r\n            ");
#nullable restore
#line 88 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
       Write(Html.ActionLink("ویرایش", "Create", new { id = Model.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n         \r\n            |");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5edef28bc8ff5334910b86a61729ac9a6971790513554", async() => {
                WriteLiteral("برگشت به صفحه اصلی");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    </div>\r\n</div>\r\n\r\n\r\n \r\n<div id=\"accordion\">\r\n");
#nullable restore
#line 98 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
       int i = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 99 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
     foreach (var item in cl._ListLocation.Where(a => a.FkP == null&&a.ForProduct).OrderBy(a => a.Code))
    {
        i++;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"card\">\r\n            <div class=\"card-header\">\r\n                <a class=\"card-link\" data-toggle=\"collapse\"");
            BeginWriteAttribute("href", " href=\"", 3168, "\"", 3188, 2);
            WriteAttributeValue("", 3175, "#collapse", 3175, 9, true);
#nullable restore
#line 104 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue("", 3184, i, 3184, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 105 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
               Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </a>\r\n            </div>\r\n            <div");
            BeginWriteAttribute("id", " id=\"", 3283, "\"", 3300, 2);
            WriteAttributeValue("", 3288, "collapse", 3288, 8, true);
#nullable restore
#line 108 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue("", 3296, i, 3296, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 3301, "\"", 3335, 2);
            WriteAttributeValue("", 3309, "collapse", 3309, 8, true);
#nullable restore
#line 108 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
WriteAttributeValue(" ", 3317, i==1?"show":"", 3318, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" data-parent=""#accordion"">
                <div class=""card-body"">

                    <table class=""table"">
                        <thead>
                            <tr>
                                <th>
                                    مکان
                                </th>

                                <th>
                                    حداقل دما
                                </th>
                                <th>
                                    حداکثر دما
                                </th>
                                <th>
                                    میانگین دما

                                </th>
                                <th>
                                    دمای موتورخانه
                                </th>
                                <th>
                                    رطوبت
                                </th>
                                <th>

                                </th>
                 ");
            WriteLiteral("           </tr>\r\n                        </thead>\r\n");
#nullable restore
#line 139 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                         foreach (var item2 in cl._ListLocation.Where(a => a.FkP == item.Id).OrderBy(a => a.Code))
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n\r\n                                <td>");
#nullable restore
#line 143 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                               Write(item2.CodeFull);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                <td>");
#nullable restore
#line 145 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                               Write(Model.TblTempRows.Where(a =>a.Location==item2.CodeFull&& a.MinDama.HasValue ).Min(a => a.MinDama).ToDama());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 146 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                               Write(Model.TblTempRows.Where(a => a.Location == item2.CodeFull && a.MaxDama.HasValue).Max(a => a.MaxDama).ToDama());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 147 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                Write(((Model.TblTempRows.Where(a => a.Location == item2.CodeFull && a.MaxDama.HasValue).Max(a => a.MaxDama)+Model.TblTempRows.Where(a => a.MinDama.HasValue).Min(a => a.MinDama))/2).ToDama());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 148 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                               Write(Model.TblTempRows.Where(a => a.Location == item2.CodeFull && a.MotorDama.HasValue).Max(a => a.MotorDama).ToDama());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 149 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                               Write(Model.TblTempRows.Where(a => a.Location == item2.CodeFull && a.R.HasValue).Max(a => a.R).ToDama());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                <td>\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5edef28bc8ff5334910b86a61729ac9a6971790520863", async() => {
                WriteLiteral("ثبت مقدار");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-idTemp", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 152 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                     WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idTemp"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idTemp", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idTemp"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 152 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"
                                                                                                    WriteLiteral(item2.CodeFull);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["location"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-location", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["location"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 155 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"





                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </table>\r\n\r\n\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 169 "D:\Source\Repos\web_sardGitHub\web_sard\Areas\Temp\Views\Operation\Temp.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<web_db._temp.TblTemp> Html { get; private set; }
    }
}
#pragma warning restore 1591
