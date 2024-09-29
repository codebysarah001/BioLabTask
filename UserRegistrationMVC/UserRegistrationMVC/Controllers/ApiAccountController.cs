using System.Web.Http;
using UserRegistrationMVC.Filters;

namespace UserRegistrationMVC.Controllers
{
    [Authentication]
    [RoutePrefix("api/account")]
    public class ApiAccountController : ApiController
    {
        [HttpGet]
        [Route("login")]
        public IHttpActionResult Login()
        {
            return Ok("User authenticated successfully.");
        }
    }
}
