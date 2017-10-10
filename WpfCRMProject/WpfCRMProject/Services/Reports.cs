using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCRMProject.Services;

namespace WpfCRMProject.Services
{
    class Reports
    {
        private string connString = @"Server=tcp:vwdotnetproject.database.windows.net,1433;Initial Catalog=CrmProject;Persist Security Info=False;User ID=vajiwinoto;Password=VW@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection conn;

        public Reports()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            conn.Open();
        }

        public List<Customer> DisplayListOfPurchesOfCustomers()
        {

            List<Customer> listCustomer = new List<Customer>();
            SqlCommand getCommand = new SqlCommand(@"SELECT  customers.Company_name, Count(Sales.customer_id) as numberofsales from Sales
left join customers on sales.customer_id = customers.customer_id
group by company_name
", conn);
            getCommand.CommandType = System.Data.CommandType.Text;
           
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                while (reader.Read())
                {                
                    
                    Customer customer = new Customer
                    {
                       
                        CompanyName = (string)reader["company_name"],
                        SalesRepId = (int)reader["numberofsales"]
                       
                    };
                    listCustomer.Add(customer);
                }
            }
            return listCustomer;

        }


    }
}
