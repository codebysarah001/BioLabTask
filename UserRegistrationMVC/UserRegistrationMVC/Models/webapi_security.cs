using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace UserRegistrationMVC.Models
{
    public class webapi_security
    {
        private static readonly string connection = ConfigurationManager.ConnectionStrings["UserDBConnection"].ConnectionString;

        public static bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("ValidateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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
