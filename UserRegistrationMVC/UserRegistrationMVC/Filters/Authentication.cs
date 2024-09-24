using System;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace UserRegistrationMVC.Filters
{
    public class Authentication : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var authHeader = filterContext.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Substring("Basic ".Length).Trim())).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username == "admin" && password == "password")
                {
                    return;
                }
            }
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
    }
}