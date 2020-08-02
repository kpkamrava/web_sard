using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace web_lib
{


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LoginAuth : AuthorizeAttribute, IAuthorizationFilter
    {
        string Role;
        public LoginAuth(  _UserRol._Rolls  role  ) : base()
        {


            // var ll = new List<_UserRol._Rolls>();
            
            Role = role.ToString() ; //String.Join(",", _UserRol.RolList.Where(a => a.getId() >= (int)rolesUP).Select(a => a.getEnum()));


        }
        public LoginAuth() : base()
        {


            // var ll = new List<_UserRol._Rolls>();

            Role = ""; //String.Join(",", _UserRol.RolList.Where(a => a.getId() >= (int)rolesUP).Select(a => a.getEnum()));


        }
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
            if (string.IsNullOrWhiteSpace( Role)==false)
            {
                var isAuthorized = user._getRolAny(Role) || user._getRolAny(_UserRol._Rolls.SuperVisor.ToString());
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

        public static bool _getRolAny(this ClaimsPrincipal User,_Rolls roll)
        {

            if (User.Identity.IsAuthenticated)
            {
               var r= User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value;
               return r.Contains(roll.ToString())|| r.Contains(_Rolls.SuperVisor.ToString());

            }
            return false;
        }
        public static bool _getRolAny(this ClaimsPrincipal User, string roll)
        {

            if (User.Identity.IsAuthenticated)
            {

                return User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Role).Value.Contains(roll.ToString());

            }
            return false;
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
                var rol =int.Parse( (User.Claims.Single(a => a.Type == System.Security.Claims.ClaimTypes.Dsa).Value));

                return rol;

            }
            return 0;
        }

        public enum _Rolls
        { SuperVisor, Bascul, SakoBargiriMahvate, AddEditUser, AddEditCustomer, EtelahatePaie, Contract, PermitPortage };

        public static  List<_ROL> RolList = new List<_ROL> 
           {
                new _ROL(_Rolls.SuperVisor,"SuperVisor"),
               new _ROL(_Rolls.Bascul,"ورود و خروج بار"),
                 new _ROL(_Rolls.SakoBargiriMahvate,"سکوی بارگیری محوطه"),
                new _ROL(_Rolls.AddEditUser,"کاربران"),
                new _ROL(_Rolls.AddEditCustomer,"مشتریان"),
               new _ROL(_Rolls.EtelahatePaie,"اطلاعات پایه"),
               new _ROL(_Rolls.Contract,"قراردادها"),
              new _ROL(_Rolls.PermitPortage,"اجازه ثبت بیشتراز قرارداد"),
               
            }; 
         public static _ROL _findRol(string name = "", int? id = null, _Rolls? enu = null)
        {
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                return RolList.Single(a => a.Contains(name));
            }
            if (id.HasValue)
            {
                return RolList.Single(a => a.Contains(id.Value));
            }
            if (enu.HasValue)
            {
                return RolList.Single(a => a.Contains(enu.Value.ToString()));
            }
            return null;
        }
         public class _ROL
        {
            public _ROL() { }
        //    public _ROL(int Id, string Name, string Text) { this.Id = Id; this.Name = Name; this.Text = Text; }
            public _ROL(  _Rolls rolll, string Text) {
             
                this.enu = rolll; this.Id = (int)rolll; this.Name = rolll.ToString(); this.Text = Text; 
            }
            private int Id;
            private _Rolls enu;
            private String Name;
            private String Text; 
           
            public bool Contains(int id)
            { return this.Id == id; }
            public _Rolls getEnum()
            { return enu; }
            public bool Contains(string Name)
            { return this.Name == Name; }
            public override String ToString()
            {
                return Name;
            }
            public String getName()
            {
                return Name;
            }


            public String getText()
            {
                return Text;
            }
            public int getId()
            {
                return Id;
            }

        }
    }

}
