using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using UserRegistrationMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationMVC.Controllers
{
    public class AccountController : Controller
    {
        private string connection = ConfigurationManager.ConnectionStrings["UserDBConnection"].ConnectionString;

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid) {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("InsertUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        ViewBag.Message = "Successfully Registered!";
                        return RedirectToAction("Login", "Account");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50000)
                        {
                            ViewBag.Error = "Email already exists!";
                        }
                        else
                        {
                            ViewBag.Error = "An error occurred!";
                        }
                    }
                }
            }
            return View();
        }

        
        [HttpGet]
        public ActionResult GetUser(string email = null, string username = null)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(username))
            {
                return View();
            }

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("GetUserByEmailOrUsername", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", (object)username ?? DBNull.Value);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var user = new
                    {
                        UserId = reader["UserId"],
                        Username = reader["Username"],
                        Email = reader["Email"]
                    };
                    return Json(user, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

                var response = await client.GetAsync("api/account/login");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetUser", "Account");
                }
                else
                {
                    ViewBag.Error = "Invalid credentials.";
                    return View();
                }
            }
        }


    }
}