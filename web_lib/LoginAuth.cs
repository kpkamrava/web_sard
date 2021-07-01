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
    public class Auth : AuthorizeAttribute, IAuthorizationFilter
    {
        string Role;
        public Auth(  string  role  ) : base()
        {


            // var ll = new List<_UserRol._Rolls>();
            
            Role = role.ToString() ; //String.Join(",", _UserRol.RolList.Where(a => a.getId() >= (int)rolesUP).Select(a => a.getEnum()));


        }
        public Auth() : base()
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
                //var isAuthorized = user._getRolAny(Role) || user._getRolAny(_User._Rolls.SuperVisor.ToString());
                //if (!isAuthorized)
                //{
                //    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                //    return;
                //}
            }

         
        }
   
    }
 
}
