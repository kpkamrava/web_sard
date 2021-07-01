namespace web_sard
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="LoginAuth" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LoginAuth : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Defines the Role.
        /// </summary>
        internal string Role;

        public LoginAuth(_UserRol._Rolls role) : base()
        {


            // var ll = new List<_UserRol._Rolls>();

            Role = role.ToString(); //String.Join(",", _UserRol.RolList.Where(a => a.getId() >= (int)rolesUP).Select(a => a.getEnum()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginAuth"/> class.
        /// </summary>
        public LoginAuth() : base()
        {


            // var ll = new List<_UserRol._Rolls>();

            Role = ""; //String.Join(",", _UserRol.RolList.Where(a => a.getId() >= (int)rolesUP).Select(a => a.getEnum()));
        }

        /// <summary>
        /// The OnAuthorization.
        /// </summary>
        /// <param name="context">The context<see cref="AuthorizationFilterContext"/>.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            // you can also use registered services
            if (string.IsNullOrWhiteSpace(Role) == false)
            {
                var isAuthorized = user._getRolAny(Role) || user._getRolAny(_UserRol._Rolls.SuperVisor.ToString());
                if (Role == _UserRol._Rolls.MainSuperVisor.ToString())
                {
                    isAuthorized = user._getRolAny(_UserRol._Rolls.MainSuperVisor.ToString());
                }
                if (!isAuthorized)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }
        }
    }


    public static class _UserRol
    {

        public static string _getuser(this ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.Identity.Name;

            }
            return null;
        }


        public static Guid? _getuserid(this ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid rol = Guid.Parse(User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

                return rol;

            }
            return null;
        }


        public static bool _getRolAny(this ClaimsPrincipal User, _Rolls roll)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (roll == _UserRol._Rolls.MainSuperVisor)
                {
                    var r = User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value;
                    return r.Contains(_Rolls.MainSuperVisor.ToString());

                }
                else
                {
                    var r = User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value;
                    return r.Contains(roll.ToString()) || r.Contains(_Rolls.SuperVisor.ToString());

                }

            }
            return false;
        }
        public static string _getRolsSTR(this ClaimsPrincipal User)
        {

          
                    var r = User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value;
                    return r;
             
            
        }


        public static bool _getRolAny(this ClaimsPrincipal User, string roll)
        {

            if (User.Identity.IsAuthenticated)
            {

                return User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value.Contains(roll.ToString());

            }
            return false;
        }


        public static string _getuserPermis(this ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            {
                var rol = (User.Claims.Single(a => a.Type == "Permis").Value);

                return rol;

            }
            return null;
        }

        public static int[] _getuserYears(this ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Any(a => a.Type == "Years") == false)
                {
                    return new int[] { };
                }
                var rol = (User.Claims.SingleOrDefault(a => a.Type == "Years")).Value.FromJson<int[]>();

                return rol;

            }
            return new int[] { };
        }
        public static string _getuserGiveName(this ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            {
                var rol = (User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.GivenName).Value);

                return rol;

            }
            return null;
        }


        public static int _getuserSalMaliDef(this ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            {
                var rol = int.Parse((User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Dsa).Value));

                return rol;

            }
            return 0;
        }


        public enum _Rolls
        {

            [web_lib.classAttr.KPvalus(Description = "Super Visor")]  SuperVisor,
            [web_lib.classAttr.KPvalus(Description = "ورود و خروج بار")] Bascul,
            [web_lib.classAttr.KPvalus(Description = "سکوی بارگیری محوطه")] SakoBargiriMahvate,
            [web_lib.classAttr.KPvalus(Description = "جابجایی")] Movement,
            [web_lib.classAttr.KPvalus(Description = "کاربران")] AddEditUser,
            [web_lib.classAttr.KPvalus(Description = "مشتریان")] AddEditCustomer,
            [web_lib.classAttr.KPvalus(Description = "اطلاعات پایه")] EtelahatePaie,
            [web_lib.classAttr.KPvalus(Description = "قراردادها")] Contract,
            [web_lib.classAttr.KPvalus(Description = "ثبت و ویرایش قرارداد")] ContractEdit,
            [web_lib.classAttr.KPvalus(Description = "عملیات مالی")] Mali,
            [web_lib.classAttr.KPvalus(Description = "اجازه ثبت بیشتراز قرارداد")] PermitPortage,
            [web_lib.classAttr.KPvalus(Description = "ثبت کننده دما")] Temp,
            [web_lib.classAttr.KPvalus(Description = "مدیریت دما")] TempAdmin,
            [web_lib.classAttr.KPvalus(Description = "مدیریت سامانه مشتریان")] Us,

            [web_lib.classAttr.KPvalus(Description = " یاداوریها")] Note ,
            [web_lib.classAttr.KPvalus(Description = "مدیریت یاداوریها")] NoteAdmin,
     
            [web_lib.classAttr.KPvalus(Description = "MainSuperVisor")] MainSuperVisor
        };


        


        public static _Rolls _ParseRoll(string name = "")
        { 
            return (_Rolls)Enum.Parse(typeof(_Rolls), name);
            
        }

 
    }
}
