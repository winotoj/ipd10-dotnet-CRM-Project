using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject
{
    class Database
    {
        private string connString = @"Server=tcp:vwdotnetproject.database.windows.net,1433;Initial Catalog=CrmProject;Persist Security Info=False;User ID=vajiwinoto;Password=VW@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            conn.Open();
        }

        public List<Customer> GetAllCustomers()
        {
            DataSet dataSet = new DataSet();
            List<Customer> listCustomer = new List<Customer>();
            SqlCommand getCommand = new SqlCommand("SELECT * FROM Customers AS c LEFT JOIN Sales as s ON c.customer_id = s.customer_id WHERE c.customer_id = @salesrep_id ");
            getCommand.Parameters.Add(new SqlParameter("salesrep_id", dataSet.Tables[0].Rows[0]["salesrep_id"]));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                Customer customer = new Customer
                {
                    CustomerId = (int)reader["Customer_Id"],
                    CompanyName = (string)reader["company_name"],
                    Street = (string)reader["street"],
                    City = (string)reader["city"],
                    Province = (string)reader["province"],
                    Postal = (string)reader["postal"],
                    Phone = (string)reader["phone"],
                    ContactFirstName = (string)reader["contact_firstname"],
                    ContactLastName = (string)reader["contact_lastname"],
                    CreateDate = (DateTime)reader["created_date"],
                    Status = (bool)reader["status"],
                    Email = (string)reader["email"]
                };
            }
        }
    }
}
