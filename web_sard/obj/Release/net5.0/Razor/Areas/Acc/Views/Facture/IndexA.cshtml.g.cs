#pragma checksum "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2afe483fbc2140714e14b5b416507bdc09c8395a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Acc_Views_Facture_IndexA), @"mvc.1.0.view", @"/Areas/Acc/Views/Facture/IndexA.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2afe483fbc2140714e14b5b416507bdc09c8395a", @"/Areas/Acc/Views/Facture/IndexA.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Areas/Acc/Views/_ViewImports.cshtml")]
    public class Areas_Acc_Views_Facture_IndexA : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<web_db.TblCustomer>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AccCustomer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ismodal", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success fa fa-pager"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
  
    ViewData["Title"] = "لیست عملیات مالی";


    bool SanadShode = ViewBag.SanadShode;
    Guid fkContractType = ViewBag.fkContractType;
    web_sard.Models.tbls.portage.kindPortage.kindPortageEnum? kindPortage = ViewBag.kindPortage;


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>لیست عملیات ");
#nullable restore
#line 13 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
            Write(cl._ListContractType(User._getuserSalMaliDef()).Single(a=>a.Id==fkContractType).Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                     Write((kindPortage.HasValue? kindPortage.Value.ToKPvalusAttr().Description:""));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                                                                 Write((SanadShode? "سند شده" : "سند نشده"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<div>\r\n");
#nullable restore
#line 16 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
     foreach (var item in web_sard.Models.tbls.portage.kindPortage.listkindcontract)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2afe483fbc2140714e14b5b416507bdc09c8395a6430", async() => {
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                                                                                                Write(item.txt);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 641, "btn", 641, 3, true);
            AddHtmlAttributeValue(" ", 644, "btn-light", 645, 10, true);
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
AddHtmlAttributeValue("  ", 654, (item.code== (int?)kindPortage)?"active":"", 656, 46, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-SanadShode", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                              WriteLiteral(SanadShode);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SanadShode"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-SanadShode", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SanadShode"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                                     WriteLiteral(fkContractType);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkContractType"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkContractType", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkContractType"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                                                                             WriteLiteral(item.code);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 19 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2afe483fbc2140714e14b5b416507bdc09c8395a11545", async() => {
                WriteLiteral("همه");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 872, "btn", 872, 3, true);
            AddHtmlAttributeValue(" ", 875, "btn-light", 876, 10, true);
#nullable restore
#line 20 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
AddHtmlAttributeValue(" ", 885, (null==  kindPortage)?"active":"", 886, 36, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-SanadShode", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 20 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                               WriteLiteral(SanadShode);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SanadShode"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-SanadShode", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SanadShode"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 20 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                      WriteLiteral(fkContractType);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkContractType"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkContractType", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkContractType"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 20 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                                                                              WriteLiteral(null);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n\r\n            <th>\r\n                ");
#nullable restore
#line 29 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.NationalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 35 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.Mob));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.Addras));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            \r\n            <th>\r\n                ");
#nullable restore
#line 45 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayNameFor(model => model.IdOtherSystem));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 52 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n\r\n            <td>\r\n                ");
#nullable restore
#line 57 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 60 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.NationalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 63 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 66 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.Mob));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 69 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.Addras));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n           \r\n            <td>\r\n                ");
#nullable restore
#line 73 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
           Write(Html.DisplayFor(modelItem => item.IdOtherSystem));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n\r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2afe483fbc2140714e14b5b416507bdc09c8395a20249", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                              WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                  WriteLiteral(fkContractType);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idcontracttype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
                                                                                                                    WriteLiteral((int?)kindPortage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kind", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 81 "D:\Source\Repos\web_sard\web_sard\Areas\Acc\Views\Facture\IndexA.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<web_db.TblCustomer>> Html { get; private set; }
    }
}
#pragma warning restore 1591
