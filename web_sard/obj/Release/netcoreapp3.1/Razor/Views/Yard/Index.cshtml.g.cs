#pragma checksum "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "795f547f9ec338910a79bdc7db01c02a99a5b642"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Yard_Index), @"mvc.1.0.view", @"/Views/Yard/Index.cshtml")]
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
#line 1 "D:\source\repos\web_sard\web_sard\Views\_ViewImports.cshtml"
using web_sard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\source\repos\web_sard\web_sard\Views\_ViewImports.cshtml"
using web_sard.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\source\repos\web_sard\web_sard\Views\_ViewImports.cshtml"
using web_lib;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"795f547f9ec338910a79bdc7db01c02a99a5b642", @"/Views/Yard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6151872f8deac43f66091f989a98fd777f10775", @"/Views/_ViewImports.cshtml")]
    public class Views_Yard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<web_sard.Models.tbls.portage.portage>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "View", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
  
    ViewData["Title"] = "لیست خودرو های داخل محوطه";
    web_db.TblContractType ContractType = (web_db.TblContractType)ViewData["type"];
    List< web_db.TblGroup> groups = (List<web_db.TblGroup>)ViewData["ListGroup"];
    int? kindPortage = (int?)ViewData["kindPortage"];
    Guid? fkgroup = (Guid?)ViewData["fkgroup"];


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<p>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "795f547f9ec338910a79bdc7db01c02a99a5b6424022", async() => {
                WriteLiteral(" همه");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-idtype", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
             WriteLiteral(ContractType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idtype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                  WriteLiteral(fkgroup);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkgroup", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                   WriteLiteral(null);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 6, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 517, "btn", 517, 3, true);
            AddHtmlAttributeValue("  ", 520, "btn-outline-dark", 522, 18, true);
            AddHtmlAttributeValue(" ", 538, "fa", 539, 3, true);
            AddHtmlAttributeValue(" ", 541, "fa-th", 542, 6, true);
#nullable restore
#line 16 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue(" ", 547, kindPortage==null?" active":"", 548, 33, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 581, "shadow", 582, 7, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 17 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
     foreach (var item in web_sard.Models.tbls.portage.kindPortage.listkindcontract.Where(a => (ContractType.IsEntry ? a.isEntry : false) || (ContractType.IsExit ? (a.isEntry == false) : false)))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "795f547f9ec338910a79bdc7db01c02a99a5b6428579", async() => {
                WriteLiteral(" ");
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                                                                                                                                Write(item.txt);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-idtype", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                 WriteLiteral(ContractType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idtype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                      WriteLiteral(fkgroup);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkgroup", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                       WriteLiteral(item.code);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 7, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 922, "btn", 922, 3, true);
            AddHtmlAttributeValue("  ", 925, "btn-outline-", 927, 14, true);
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue("", 939, item.classcolor, 939, 18, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 957, "fa", 958, 3, true);
            AddHtmlAttributeValue(" ", 960, "fa-list", 961, 8, true);
#nullable restore
#line 19 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue(" ", 968, kindPortage==item.code?" active":"", 969, 38, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 1007, "shadow", 1008, 7, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 20 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>\r\n<hr />\r\n<p>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "795f547f9ec338910a79bdc7db01c02a99a5b64213559", async() => {
                WriteLiteral(" بدون گروه");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-idtype", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
             WriteLiteral(ContractType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idtype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                      WriteLiteral(kindPortage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
              WriteLiteral(Guid.Empty);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkgroup", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 6, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1191, "btn", 1191, 3, true);
            AddHtmlAttributeValue("  ", 1194, "btn-outline-dark", 1196, 18, true);
            AddHtmlAttributeValue(" ", 1212, "fa", 1213, 3, true);
            AddHtmlAttributeValue(" ", 1215, "fa-th", 1216, 6, true);
#nullable restore
#line 28 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue(" ", 1221, fkgroup==Guid.Empty?" active":"", 1222, 35, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 1257, "shadow", 1258, 7, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 29 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
     foreach (var item in groups)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "795f547f9ec338910a79bdc7db01c02a99a5b64217920", async() => {
                WriteLiteral(" ");
#nullable restore
#line 33 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                                              Write(item.Title);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-idtype", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 31 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                 WriteLiteral(ContractType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-idtype", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["idtype"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                      WriteLiteral(kindPortage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-kindPortage", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["kindPortage"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                  WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fkgroup", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fkgroup"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 7, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1468, "btn", 1468, 3, true);
            AddHtmlAttributeValue("  ", 1471, "btn-outline-", 1473, 14, true);
#nullable restore
#line 33 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue("", 1485, item.Class, 1485, 13, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 1498, "fa", 1499, 3, true);
            AddHtmlAttributeValue(" ", 1501, "fa-list", 1502, 8, true);
#nullable restore
#line 33 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue(" ", 1509, fkgroup==item.Id?" active":"", 1510, 32, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 1542, "shadow", 1543, 7, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 34 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<div class=\"row\">\r\n\r\n");
#nullable restore
#line 39 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
     foreach (var item in Model)
    {
        var kindcontract = web_sard.Models.tbls.portage.kindPortage.listkindcontract.Find(a => a.code == item.KindCode);


#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\" col-lg-4 colSearch\">\r\n            <div class=\"card mb-4 shadow\">\r\n                <div class=\"card-header\">\r\n                    ");
#nullable restore
#line 46 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
               Write(item.Code);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 46 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                            Write(item.Customer.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <span");
            BeginWriteAttribute("class", " class=\"", 1980, "\"", 2041, 5);
            WriteAttributeValue("", 1988, "badge", 1988, 5, true);
            WriteAttributeValue(" ", 1993, "bg-", 1994, 4, true);
#nullable restore
#line 47 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 1997, kindcontract.classcolor, 1997, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2023, "float-left", 2024, 11, true);
            WriteAttributeValue(" ", 2034, "shadow", 2035, 7, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 47 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                   Write(kindcontract.txt);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"badge bg-secondary bg-pill \">");
#nullable restore
#line 47 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                                                            Write(item.Date1time);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i></span>\r\n                </div>\r\n                <div class=\"card-body\">\r\n\r\n                    <ul class=\"list-group list-group-flush m-0 p-0\">\r\n                        <li class=\"list-group-item\">\r\n                            <span");
            BeginWriteAttribute("class", " class=\"", 2352, "\"", 2407, 5);
            WriteAttributeValue("", 2360, "badge", 2360, 5, true);
            WriteAttributeValue(" ", 2365, "bg-", 2366, 4, true);
#nullable restore
#line 53 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 2369, kindcontract.classcolor, 2369, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2395, "fa", 2396, 3, true);
            WriteAttributeValue(" ", 2398, "fa-truck", 2399, 9, true);
            EndWriteAttribute();
            WriteLiteral("> </span>");
#nullable restore
#line 53 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                             Write(item.CarMashin);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <span");
            BeginWriteAttribute("class", " class=\"", 2467, "\"", 2525, 5);
            WriteAttributeValue("", 2475, "badge", 2475, 5, true);
            WriteAttributeValue(" ", 2480, "bg-", 2481, 4, true);
#nullable restore
#line 54 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 2484, kindcontract.classcolor, 2484, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2510, "fa", 2511, 3, true);
            WriteAttributeValue(" ", 2513, "fa-user-cog", 2514, 12, true);
            EndWriteAttribute();
            WriteLiteral("> </span> ");
#nullable restore
#line 54 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                 Write(item.CarRanande);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </li>\r\n                        <li class=\"list-group-item\">\r\n                            <span");
            BeginWriteAttribute("class", " class=\"", 2672, "\"", 2733, 5);
            WriteAttributeValue("", 2680, "badge", 2680, 5, true);
            WriteAttributeValue(" ", 2685, "bg-", 2686, 4, true);
#nullable restore
#line 57 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 2689, kindcontract.classcolor, 2689, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2715, "fa", 2716, 3, true);
            WriteAttributeValue(" ", 2718, "fa-car-battery", 2719, 15, true);
            EndWriteAttribute();
            WriteLiteral("> </span>");
#nullable restore
#line 57 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                   Write(item.CarShMashin);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <span");
            BeginWriteAttribute("class", " class=\"", 2795, "\"", 2851, 5);
            WriteAttributeValue("", 2803, "badge", 2803, 5, true);
            WriteAttributeValue(" ", 2808, "bg-", 2809, 4, true);
#nullable restore
#line 58 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 2812, kindcontract.classcolor, 2812, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2838, "fa", 2839, 3, true);
            WriteAttributeValue(" ", 2841, "fa-mobile", 2842, 10, true);
            EndWriteAttribute();
            WriteLiteral("> </span>");
#nullable restore
#line 58 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                              Write(item.CarTell);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </li>\r\n                        <li class=\"list-group-item\">\r\n");
#nullable restore
#line 61 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                             if (ContractType.IsProduct1Packing0)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span");
            BeginWriteAttribute("class", " class=\"", 3096, "\"", 3149, 5);
            WriteAttributeValue("", 3104, "badge", 3104, 5, true);
            WriteAttributeValue(" ", 3109, "bg-", 3110, 4, true);
#nullable restore
#line 63 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 3113, kindcontract.classcolor, 3113, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 3139, "fa", 3140, 3, true);
            WriteAttributeValue(" ", 3142, "fa-hdd", 3143, 7, true);
            EndWriteAttribute();
            WriteLiteral("> </span>");
#nullable restore
#line 63 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                               Write(item.Weight1.GetValueOrDefault().ToString("###0.##"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                                                                                                                                         

                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                             if (item.Txt.IsEmpty() == false)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span");
            BeginWriteAttribute("class", " class=\"", 3378, "\"", 3437, 5);
            WriteAttributeValue("", 3386, "badge", 3386, 5, true);
            WriteAttributeValue(" ", 3391, "bg-", 3392, 4, true);
#nullable restore
#line 68 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
WriteAttributeValue("", 3395, kindcontract.classcolor, 3395, 26, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 3421, "fa", 3422, 3, true);
            WriteAttributeValue(" ", 3424, "fa-paperclip", 3425, 13, true);
            EndWriteAttribute();
            WriteLiteral("> </span>\r\n");
#nullable restore
#line 69 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                           Write(item.Txt);

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                         
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </li>\r\n                    </ul>\r\n\r\n\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "795f547f9ec338910a79bdc7db01c02a99a5b64232276", async() => {
                WriteLiteral("\r\n                        انتخاب\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 75 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
                                           WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3657, "btn", 3657, 3, true);
            AddHtmlAttributeValue(" ", 3660, "btn-", 3661, 5, true);
#nullable restore
#line 75 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
AddHtmlAttributeValue("", 3665, kindcontract.classcolor, 3665, 26, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue(" ", 3691, "w-100", 3692, 6, true);
            AddHtmlAttributeValue(" ", 3697, "shadow-sm", 3698, 10, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n");
#nullable restore
#line 82 "D:\source\repos\web_sard\web_sard\Views\Yard\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("     \r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<web_sard.Models.tbls.portage.portage>> Html { get; private set; }
    }
}
#pragma warning restore 1591