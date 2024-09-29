using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using UserRegistrationMVC.Models;

namespace UserRegistrationMVC.Filters
{
    public class Authentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var authHeader = filterContext.Request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Basic")
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing or invalid Authorization header");
                return;
            }

            try
            {
                string authString = authHeader.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authString));

                string[] credentials = originalString.Split(':');
                string username = credentials[0];
                string password = credentials[1];

                if (!webapi_security.ValidateUser(username, password))
                {
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
                }
            }
            catch (Exception)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Authorization header format");
            }
        }
    }
}
