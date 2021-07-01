using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection; 
using System.Linq; 
using web_lib;
namespace web_sard
{
    public static class sys
    {
        public enum _SystemEnum
        {
            [classAttr.KPColor(ClassIcon = "fa-apple-alt")] [web_lib.classAttr.KPvalus(Description ="سیستم اصلی",Description2 ="")] Main,
            [classAttr.KPColor(ClassIcon = "fa-credit-card")] [web_lib.classAttr.KPvalus(Description = "سیستم مالی", Description2 = "Acc")] Mali,
            [classAttr.KPColor(ClassIcon = "fa-snowflake")] [web_lib.classAttr.KPvalus(Description = "سیستم کنترل دما", Description2 = "Temp")] Temp,
            [classAttr.KPColor(ClassIcon = "fa-calendar-alt")] [web_lib.classAttr.KPvalus(Description = "سیستم تقویم کاری", Description2 = "Note")] Note,
             [classAttr.KPColor(ClassIcon = "fa-address-card-o")] [web_lib.classAttr.KPvalus(Description = "سیستم مشتریان", Description2 = "Us")] Us,
        }
        public static IHtmlContent _menuSys(this IHtmlHelper th,   _SystemEnum Gair)
        {
            //    var urlHelper = GetContext(htmlHelper).RequestServices.GetRequiredService<IUrlHelper>();

            var urlHelperFactory = th.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(th.ViewContext);



 


            var str1 = @$"";
            foreach (var item in _SystemEnum.Main.GetAllItems().Where(a=>a!=Gair))
            { 
                str1 += @$"
           <li>
            <a class='nav-link ' href='{urlHelper.Action("Index","Home",new {Area=  item.ToKPvalusAttr().Description2 } ) }' ><i class='fa {item.ToKPColorAttr().ClassIcon}'></i>
            <span>{item.ToKPvalusAttr().Description}</span></a>
           </li> 
            ";

            }
            var str = @$"

 <li data-toggle='collapse' data-target='#pppsys' class='collapsed'>
            <a href='#' class='text-success'><i class='fa {Gair.ToKPColorAttr().ClassIcon} fa-lg'></i> {Gair.ToKPvalusAttr().Description}<span class='arrow'></span></a>
            <ul class='sub-menu collapse' id='pppsys'>
{str1}
               
            </ul>
        </li>

";

            return th.Raw(str);
        }



    }
}
