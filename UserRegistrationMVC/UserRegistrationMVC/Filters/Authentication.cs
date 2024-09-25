using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace UserRegistrationMVC.Filters
{
    public class Authentication : AuthorizationFilterAttribute
    {
        private object webapi_security;

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var authHeader = filterContext.Request.Headers.Authorization;
            if (authHeader == null)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            else
            {
                string auth_String = filterContext.Request.Headers.Authorization.Parameter;
                string original_String = Encoding.UTF8.GetString(Convert.FromBase64String(auth_String));

                string username = original_String.Split(':')[0];
                string password = original_String.Split(':')[1];

                if (!UserRegistrationMVC.Models.webapi_security.ValidateUsers(username, password))
                {
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
            base.OnAuthorization(filterContext);

        }
    }
}