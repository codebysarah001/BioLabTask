using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UserRegistrationMVC.Models
{

    public class webapi_security
    {

        private static readonly string connection = ConfigurationManager.ConnectionStrings["UserDBConnection"].ConnectionString;
        public static bool ValidateUsers(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "SELECT COUNT(*) FROM users WHERE username = @Username AND password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@Username", username));
                    cmd.Parameters.Add(new SqlParameter("@Password", password)); 
                    con.Open();

                    int result = (int)cmd.ExecuteScalar(); 

                    return result > 0; 
                }
            }
        }

    }
}