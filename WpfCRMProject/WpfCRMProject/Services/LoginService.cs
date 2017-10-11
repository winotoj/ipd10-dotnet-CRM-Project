using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WpfCRMProject.Domain;

namespace WpfCRMProject.Services
{
    public class LoginService
    {
        public User Login(string username, string password)
        {
// WJ-SQL COMMAND HAS TO BE CHANGED, IF YOU ENTER 'OR''=' FOR USER NAME N PASS WILL BYPASS
            SqlConnection con = new SqlConnection(@"Server=tcp:vwdotnetproject.database.windows.net,1433;Initial Catalog=CrmProject;Persist Security Info=False;User ID=vajiwinoto;Password=VW@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SalesReps WHERE username = '" + username + "'  AND PASSWORD = '" + password + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            con.Close();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string firstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                string lastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                string role = dataSet.Tables[0].Rows[0]["role"].ToString();
                string email = dataSet.Tables[0].Rows[0]["email"].ToString();
                User user = new User(firstName, lastName, username, role, email);
               
                return user;
            }

            return null;
        }
    }
}
