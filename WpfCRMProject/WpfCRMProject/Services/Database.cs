using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        ////public List<Customer> GetAllCustomers()
        ////{
        ////    //DataSet dataSet = new DataSet();
        ////    List<Customer> listCustomer = new List<Customer>();
        ////    //SqlCommand getCommand = new SqlCommand("SELECT * FROM Customers AS c LEFT JOIN Sales as s ON c.customer_id = s.customer_id WHERE c.salesrep_id = @salesrep_id ", conn);
        ////   SqlCommand getCommand = new SqlCommand("SELECT * fROM customers as c RIGHT JOIN v_Sales_LatestPurchase as v on c.customer_id = v.customer_id WHERE c.salesrep_id = @salesrep_id", conn);
        ////    getCommand.Parameters.Add(new SqlParameter("salesrep_id", Application.Current.Resources["UserName"]));
        ////    using (SqlDataReader reader = getCommand.ExecuteReader())
        ////    {
        ////        while (reader.Read())
        ////        {
        ////            //MessageBox.Show("this is date" + reader["purchase_date"]);
        ////            Customer customer = new Customer
        ////            {
        ////                CustomerId = (int)reader["Customer_Id"],
        ////                CompanyName = (string)reader["company_name"],
        ////                Street = (string)reader["street"],
        ////                City = (string)reader["city"],
        ////                Province = (string)reader["province"],
        ////                Postal = (string)reader["postal"],
        ////                Phone = (string)reader["phone"],
        ////                ContactFirstName = (string)reader["contact_firstname"],
        ////                ContactLastName = (string)reader["contact_lastname"],
        ////                CreateDate = (DateTime)reader["created_date"],
        ////                Status = (bool)reader["status"],
        ////                Email = (string)reader["email"],
        ////                LastPurchaseDate = (DateTime)reader["purchase_date"],
        ////                Amount = (decimal)reader["amount"]
        ////            };
        ////            listCustomer.Add(customer);

        ////        }
        ////    }
        ////    return listCustomer;
        ////}

        public List<Customer> GetAllCustomers()
        {
            List<Customer> listCustomer = new List<Customer>();
            SqlCommand getCommand = new SqlCommand("SELECT * fROM customers as c LEFT JOIN v_Sales_LatestPurchase as v on c.customer_id = v.customer_id WHERE c.salesrep_id = @salesrep_id and c.status=1", conn);
            getCommand.Parameters.Add(new SqlParameter("salesrep_id", Application.Current.Resources["UserName"]));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string amt = (reader["amount"] == DBNull.Value ? "" : ((decimal)reader["amount"]).ToString("F02", CultureInfo.InvariantCulture));
                    string pd = reader["purchase_date"].ToString();
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
                        Email = (string)reader["email"],
                        SalesRepId = (int)reader["salesrep_Id"],
                        LastPurchaseDate = pd,
                        Amount = amt
                    };
                    listCustomer.Add(customer);
                }
            }
            return listCustomer;
        }

        public void AddPerson(Customer c)
        {
            String query = @"INSERT INTO CUSTOMERS (company_name, street, city 
                                                        , province, postal
                                                        , phone
                                                        , contact_firstname
                                                        , contact_lastname
                                                        , created_date
                                                        , status
                                                        , salesrep_id
                                                        , email
                                                        , country) 
                                                        VALUES(@CompanyName, @Street, 
                                                               @City, @Province, 
                                                               @Postal, @Phone, 
                                                               @FirstName, @LastName, 
                                                               @CreatedDate, @Status,
                                                               @Salesrep_id, @Email,
                                                               @Country)";

            SqlCommand insertCommand = new SqlCommand(query, conn);
            insertCommand.Parameters.Add(new SqlParameter("CompanyName", c.CompanyName));
            insertCommand.Parameters.Add(new SqlParameter("Street", c.Street));
            insertCommand.Parameters.Add(new SqlParameter("City", c.City));
            insertCommand.Parameters.Add(new SqlParameter("Province", c.Province));
            insertCommand.Parameters.Add(new SqlParameter("Postal", c.Postal));
            insertCommand.Parameters.Add(new SqlParameter("Phone", c.Phone));
            insertCommand.Parameters.Add(new SqlParameter("FirstName", c.ContactFirstName));
            insertCommand.Parameters.Add(new SqlParameter("LastName", c.ContactLastName));
            insertCommand.Parameters.Add(new SqlParameter("CreatedDate", c.CreateDate));
            insertCommand.Parameters.Add(new SqlParameter("Status", c.Status));
            insertCommand.Parameters.Add(new SqlParameter("Salesrep_id", Application.Current.Resources["UserName"]));
            insertCommand.Parameters.Add(new SqlParameter("Email", c.Email));
            insertCommand.Parameters.Add(new SqlParameter("Country", c.Country));
            insertCommand.ExecuteNonQuery();
        }

        public List<Customer> GetOpportunities()
        {
            List<Customer> listOpportunities = new List<Customer>();
            SqlCommand getCommand = new SqlCommand("SELECT * FROM CUSTOMERS WHERE STATUS = 0 AND SALESREP_ID = @SALESREP_ID", conn);
            getCommand.Parameters.Add(new SqlParameter("SALESREP_ID", Application.Current.Resources["UserName"]));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                while (reader.Read())
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
                        Email = (string)reader["email"],
                    };
                    listOpportunities.Add(customer);
                }

            }
            return listOpportunities;

        }

        public void UpdateCustomer(Customer c)
        {
            String update = @"UPDATE CUSTOMERS  SET company_name =@CompanyName,
                                                street = @Street,
                                                city = @City,
                                                province = @Province, 
                                                postal = @Postal,
                                                phone = @Phone,
                                                contact_firstname = @FirstName,
                                                contact_lastname = @LastName,
                                                status = @Status,
                                                email = @Email,
                                                country = @Country
                                            WHERE salesrep_id = @Salesrep_id";
 
            SqlCommand insertCommand = new SqlCommand(update, conn);
            insertCommand.Parameters.Add(new SqlParameter("CompanyName", c.CompanyName));
            insertCommand.Parameters.Add(new SqlParameter("Street", c.Street));
            insertCommand.Parameters.Add(new SqlParameter("City", c.City));
            insertCommand.Parameters.Add(new SqlParameter("Province", c.Province));
            insertCommand.Parameters.Add(new SqlParameter("Postal", c.Postal));
            insertCommand.Parameters.Add(new SqlParameter("Phone", c.Phone));
            insertCommand.Parameters.Add(new SqlParameter("FirstName", c.ContactFirstName));
            insertCommand.Parameters.Add(new SqlParameter("LastName", c.ContactLastName));
            insertCommand.Parameters.Add(new SqlParameter("Status", c.Status));
            insertCommand.Parameters.Add(new SqlParameter("Salesrep_id", Application.Current.Resources["UserName"]));
            insertCommand.Parameters.Add(new SqlParameter("Email", c.Email));
            insertCommand.Parameters.Add(new SqlParameter("Country", c.Country));
            insertCommand.ExecuteNonQuery();
        }

    }
}