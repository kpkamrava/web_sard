#pragma checksum "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e122805999111d4d15de8dfdde90df8c9ccfb78"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Server_AccCustomer), @"mvc.1.0.view", @"/Views/Server/AccCustomer.cshtml")]
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
#line 1 "D:\Source\Repos\web_sard\web_sard\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sard\web_sard\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e122805999111d4d15de8dfdde90df8c9ccfb78", @"/Views/Server/AccCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Views/_ViewImports.cshtml")]
    public class Views_Server_AccCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<web_db.TblPortageMoney>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AccCustomer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "server", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("isModal", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateFacktorCustomer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
  

    ViewData["Title"] = "";


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"btn-group\">\r\n");
#nullable restore
#line 9 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
     foreach (var item in cl._ListContractType(User._getuserSalMaliDef()))
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
         if (Context.Request.IsAjaxRequest())
        {

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e122805999111d4d15de8dfdde90df8c9ccfb785729", async() => {
#nullable restore
#line 12 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                                         Write(item.Title);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 275, "btn", 275, 3, true);
            AddHtmlAttributeValue(" ", 278, "btn-light", 279, 10, true);
            AddHtmlAttributeValue(" ", 288, "fa", 289, 3, true);
            AddHtmlAttributeValue(" ", 291, "fa-outdent", 292, 11, true);
#nullable restore
#line 12 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
AddHtmlAttributeValue(" ", 302, item.Id==ViewBag.idcontracttype?"active":"", 303, 46, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 12 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                  WriteLiteral(ViewBag.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 12 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                         WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idcontracttype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 13 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e122805999111d4d15de8dfdde90df8c9ccfb7810490", async() => {
#nullable restore
#line 17 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                         Write(item.Title);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 552, "btn", 552, 3, true);
            AddHtmlAttributeValue(" ", 555, "btn-light", 556, 10, true);
            AddHtmlAttributeValue(" ", 565, "fa", 566, 3, true);
            AddHtmlAttributeValue(" ", 568, "fa-outdent", 569, 11, true);
#nullable restore
#line 17 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
AddHtmlAttributeValue(" ", 579, item.Id==ViewBag.idcontracttype?"active":"", 580, 46, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                 WriteLiteral(ViewBag.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                        WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idcontracttype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 18 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"

        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div> \r\n<br />\r\n<div class=\"btn-group\">\r\n");
#nullable restore
#line 24 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
     foreach (var item in web_sard.Models.tbls.portage.kindPortage.listkindcontract)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
         if (Context.Request.IsAjaxRequest())
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e122805999111d4d15de8dfdde90df8c9ccfb7815831", async() => {
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                                                                               Write(item.txt);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 993, "btn", 993, 3, true);
            AddHtmlAttributeValue(" ", 996, "btn-light", 997, 10, true);
            AddHtmlAttributeValue(" ", 1006, "fa", 1007, 3, true);
            AddHtmlAttributeValue(" ", 1009, "fa-outdent", 1010, 11, true);
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
AddHtmlAttributeValue(" ", 1020, item.code==ViewBag.kind?"active":"", 1021, 38, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                             WriteLiteral(ViewBag.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                    WriteLiteral(ViewBag.idcontracttype);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idcontracttype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                                             WriteLiteral(item.code);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kind", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 29 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e122805999111d4d15de8dfdde90df8c9ccfb7821509", async() => {
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                                                                Write(item.txt);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1307, "btn", 1307, 3, true);
            AddHtmlAttributeValue(" ", 1310, "btn-light", 1311, 10, true);
            AddHtmlAttributeValue(" ", 1320, "fa", 1321, 3, true);
            AddHtmlAttributeValue(" ", 1323, "fa-outdent", 1324, 11, true);
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
AddHtmlAttributeValue(" ", 1334, item.code==ViewBag.kind?"active":"", 1335, 38, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                             WriteLiteral(ViewBag.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                    WriteLiteral(ViewBag.idcontracttype);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idcontracttype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idcontracttype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                                                                                                                                             WriteLiteral(item.code);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kind", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kind"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 34 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"

        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n  \r\n");
#nullable restore
#line 39 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
 if (ViewBag.idcontracttype == null) { return; }

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e122805999111d4d15de8dfdde90df8c9ccfb7827470", async() => {
                WriteLiteral(@"
    <div class=""card"">

        <div class=""card-body"">

            <table class=""table table-striped table-responsive table-responsive-sm"">
                <thead class=""table-dark"">
                    <tr>
                        <th>

                        </th>
                        <th>
                            ");
#nullable restore
#line 53 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                       Write(Html.DisplayNameFor(model => model.Txt));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </th>

                        <th>
                            محل
                        </th>
                        <th>
                            جعبه و محصول
                        </th>

                        <th>
                            ماشین
                        </th>
                        <th>
                            نوع
                        </th>
                        <th>
                            مقدار
                        </th>

                        <th>
                            نرخ
                        </th>
                        <th>
                            حساب
                        </th>
                        <th>
                            زمان
                        </th>
                        <th>
                            فاکتور
                        </th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 88 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                     foreach (var item in Model.OrderBy(a => a.FkPortageNavigation.KindTitle))
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <tr>\r\n\r\n                            <td>\r\n                                <label> <input type=\"checkbox\" name=\"ids\"");
                BeginWriteAttribute("value", " value=\"", 3329, "\"", 3345, 1);
#nullable restore
#line 93 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
WriteAttributeValue("", 3337, item.Id, 3337, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" checked />  ");
#nullable restore
#line 93 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                                                                                                  Write(item.FkPortageNavigation.KindTitle);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n\r\n                            </td>\r\n\r\n                            <td>\r\n                                ");
#nullable restore
#line 98 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Txt));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n\r\n\r\n                            <td>\r\n                                ");
#nullable restore
#line 103 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                            Write((cl._ListLocation.SingleOrDefault(a=>a.Id== item.Fklocation)??new web_db.TblLocation()).Title);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n\r\n                                ");
#nullable restore
#line 107 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                            Write((cl._ListPacking.SingleOrDefault(a => item.FkPacking == a.Id)??new web_db.TblPacking()).Title);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                ");
#nullable restore
#line 108 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                            Write((cl._ListProduct.SingleOrDefault(a=>item.FkProduct==a.Id)??new web_db.TblProduct()).Title);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 111 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(cl._ListCar.Single(a => item.FkCar == a.Id).Title);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 114 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.Kind);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 117 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.Weight.gadrmotlagh().ToMoney());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n\r\n                            <td>\r\n                                ");
#nullable restore
#line 121 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.PriceOneWeight.gadrmotlagh().ToMoney());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 124 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.PriceSum.gadrmotlagh().ToMoney());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n\r\n                            <td>\r\n                                ");
#nullable restore
#line 128 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.Date.ToPersianDate());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <th>\r\n                                ");
#nullable restore
#line 131 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                           Write(item.FkPortageNavigation.OtcodeResid);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            </th>\r\n                        </tr>\r\n");
#nullable restore
#line 135 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </tbody>\r\n                <tfoot>\r\n                    <tr>\r\n                         \r\n                        <td colspan=\"8\">\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 144 "D:\Source\Repos\web_sard\web_sard\Views\Server\AccCustomer.cshtml"
                       Write(Model.Sum(a => a.PriceSum.gadrmotlagh()).ToMoney());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </td>


                        <td>
                        </td>
                    </tr>
                </tfoot>
            </table>

        </div>
        <div class=""card-header"">
            <button type=""submit"" class=""btn btn-success fa fa-outdent"">ارسال به برنامه حسابداری </button>
        </div>
    </div>



");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<web_db.TblPortageMoney>> Html { get; private set; }
    }
}
#pragma warning restore 1591
