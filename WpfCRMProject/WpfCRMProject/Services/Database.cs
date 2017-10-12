using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfCRMProject.Domain;

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

        public List<Schedule> GetAllAppointment()
        {
            List<Schedule> scheduleList = new List<Schedule>();
            SqlCommand getCommand = new SqlCommand(@"SELECT schedule_id, type, note, status, scheduled_date, created_date, salesrep_id, subject, startTime, endTime," +
                                                    " (select company_name from customers c where s.customer_id = c.Customer_id) as customerName " +
                                                    " FROM Schedules s WHERE salesrep_id = @SalesrepID", conn);
            getCommand.Parameters.Add(new SqlParameter("SalesrepID", Application.Current.Resources["UserName"]));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    var schedule = new Schedule
                    {
                        Schedule_id = (int)reader["schedule_id"],
                        Type = (string)reader["type"],
                        Note = (string)reader["note"],
                        Status = (string)reader["status"],
                        ScheduleDate = (DateTime)reader["scheduled_date"],
                        CreatedDate = (DateTime)reader["created_date"],
                        SalesRepId = (int)reader["salesrep_id"],
                        Subject = (string)reader["subject"],
                        StartTime = (string)reader["startTime"],
                        EndTime = (string)reader["endTime"],
                        //CustomerID = (int)reader["customer_id"],
                        CustomerName = (string)reader["customerName"]

                    };
                    scheduleList.Add(schedule);

                }
            }
            return scheduleList;
        }

        public void AddAppointment(Schedule s)
        {
            
            String query = @"INSERT INTO Schedules ([type], [note]
                                                   ,[scheduled_date]                                                   
                                                   ,[salesrep_id]
                                                   ,[subject]
                                                   ,[startTime]
                                                   ,[endTime]
                                                   ,[customer_id]
                                                                )
                                                    VALUES(@Type, @Note, @ScheduleDate, 
                                                            @Salesrep_id, @Subject, @StartTime, @EndTime, @CustomerID )";
            SqlCommand addAppointment = new SqlCommand(query, conn);
            addAppointment.Parameters.Add(new SqlParameter("Type", s.Type));
            addAppointment.Parameters.Add(new SqlParameter("Note", s.Note));
            addAppointment.Parameters.Add(new SqlParameter("ScheduleDate", s.ScheduleDate));            
            addAppointment.Parameters.Add(new SqlParameter("Salesrep_id", s.SalesRepId));
            addAppointment.Parameters.Add(new SqlParameter("Subject", s.Subject));
            addAppointment.Parameters.Add(new SqlParameter("StartTime", s.StartTime));
            addAppointment.Parameters.Add(new SqlParameter("EndTime", s.EndTime));
            addAppointment.Parameters.Add(new SqlParameter("CustomerID", s.CustomerID));
            addAppointment.ExecuteNonQuery();
        }

        public Boolean CheckAppointment(Schedule s)
        {
            SqlCommand getCommand = new SqlCommand(@"SELECT * FROM Schedules WHERE salesrep_id = @Salesrep_ID AND scheduled_date = @ScheduleDate " +
                                                           "AND (startTime = @StartTime OR endTime = @EndTime) ", conn);
            getCommand.Parameters.Add(new SqlParameter("Salesrep_ID", s.SalesRepId));
            getCommand.Parameters.Add(new SqlParameter("ScheduleDate", s.ScheduleDate));
            getCommand.Parameters.Add(new SqlParameter("StartTime", s.StartTime));
            getCommand.Parameters.Add(new SqlParameter("EndTime", s.EndTime));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;

                }

                return false;

            }
        }

        public void UpdateAppointment(Schedule s)
        {
            string queryUpdate = @"UPDATE Schedules SET note = @Note,
                                                       scheduled_date = @ScheduleDate,
                                                       startTime = @StartTime,
                                                       endTime = @EndTime,
                                                       status = @Status
                                                        WHERE schedule_id = @ScheduleID";

            SqlCommand insertCommand = new SqlCommand(queryUpdate, conn);
            insertCommand.Parameters.Add(new SqlParameter("Note", s.Note));
            insertCommand.Parameters.Add(new SqlParameter("ScheduleDate", s.ScheduleDate));
            insertCommand.Parameters.Add(new SqlParameter("StartTime", s.StartTime));
            insertCommand.Parameters.Add(new SqlParameter("EndTime", s.EndTime));
            insertCommand.Parameters.Add(new SqlParameter("Status", s.Status));
            insertCommand.Parameters.Add(new SqlParameter("ScheduleID", s.Schedule_id));
            insertCommand.ExecuteNonQuery();

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
                                            WHERE salesrep_id = @Salesrep_id & company_id = @companyId";

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
            insertCommand.Parameters.Add(new SqlParameter("Customer_id", c.CustomerId));
            insertCommand.ExecuteNonQuery();
        }
        //to be added: try catch

        // used for scheduled
        public List<Customer> SearchCompanyName(String c)
        {
            List<Customer> listCompany = new List<Customer>();
            string searchCN = "SELECT * FROM Customers WHERE salesrep_id=@Id AND company_name like '%" + c + "%'";
            SqlCommand searchCommand = new SqlCommand(searchCN, conn);
            searchCommand.Parameters.Add(new SqlParameter("Id", Application.Current.Resources["UserName"]));
            //searchCommand.Parameters.Add(new SqlParameter("company", c));
            using (SqlDataReader reader = searchCommand.ExecuteReader())
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
                    listCompany.Add(customer);
                }

            }
            return listCompany;
        }
        public List<User> GetAllUsers()
        {
            List<User> listUser = new List<User>();
            SqlCommand getCommand = new SqlCommand("SELECT * FROM Salesreps", conn);
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    User user = new User
                    {
                        FirstName = (string)reader["firstname"],
                        LastName = (string)reader["lastname"],
                        UserId = (int)reader["username"],
                        Role = (string)reader["role"]
                    };
                    listUser.Add(user);
                }
            }
            return listUser;
        }

        public List<Customer> SearchCompanyCustom(string s)
        {
            List<Customer> listCustomer = new List<Customer>();
            string commandString = "SELECT * fROM (salesreps AS s LEFT JOIN customers AS c On s.username = c.salesrep_Id) LEFT JOIN v_Sales_LatestPurchase AS v ON c.customer_id = v.customer_id WHERE" + s;
            SqlCommand searchCommand = new SqlCommand(commandString, conn);
            using (SqlDataReader reader = searchCommand.ExecuteReader())
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
        public string GetUserEmail(string userId)
        {
            string email = string.Empty;
            SqlCommand getCommand = new SqlCommand("SELECT email FROM Salesreps WHERE username = @userId", conn);
            getCommand.Parameters.Add(new SqlParameter("userId", userId));
            using (SqlDataReader reader = getCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    email = (string)reader["email"];  
                }
            }
            return email;
        }

        public void RecordMessage(string subject, string note, string type, int customerId, int userId)
        {
            string strRecord = @"INSERT INTO Messages (subject,  note, type, msg_date, customer_id, user_id) VALUES (@subject, @note, @type, @msg_date, @customerId, @user_id)";
            try
            {
                SqlCommand recordMsg = new SqlCommand(strRecord, conn);
                recordMsg.Parameters.Add(new SqlParameter("subject", subject));
                recordMsg.Parameters.Add(new SqlParameter("note", note));
                recordMsg.Parameters.Add(new SqlParameter("type", type));
                recordMsg.Parameters.Add(new SqlParameter("msg_date", DateTime.Now));
                recordMsg.Parameters.Add(new SqlParameter("customerId", customerId));
                recordMsg.Parameters.Add(new SqlParameter("user_id", userId));
                recordMsg.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Message Sent but error record data" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Messages> GetMessages(int customerId)
        {
            List<Messages> list = new List<Messages>();
            string strGet = @"SELECT * FROM Messages WHERE customer_id = @customerId ORDER BY msg_date";

            SqlCommand searchCommand = new SqlCommand(strGet, conn);
            searchCommand.Parameters.Add(new SqlParameter("customerId", customerId));
            using (SqlDataReader reader = searchCommand.ExecuteReader())
            {
                while (reader.Read())
                {

                    Messages messages = new Messages
                    {
                        CustomerID = (int)reader["Customer_Id"],
                        CreatedDate = (DateTime)reader["msg_date"],
                        Type = (string)reader["type"],
                        Subject = (string)reader["subject"],
                        Note = (string)reader["note"],
                        UserId = (int)reader["user_id"]
                    };
                    list.Add(messages);

                }
            }

            return list;
        }
        public void AddMessage(int customer_id, string subject, string note, string type, int user_id)
        {

            string add = @"INSERT INTO Messages (customer_id, subject, note, type, msg_date, user_id) VALUES (@customer_id, @subject, @note, @type, @msg_date, @user_id)";
            try
            {
                SqlCommand addCommand = new SqlCommand(add, conn);
                addCommand.Parameters.Add(new SqlParameter("customer_id", customer_id));
                addCommand.Parameters.Add(new SqlParameter("subject", subject));
                addCommand.Parameters.Add(new SqlParameter("note", note));
                addCommand.Parameters.Add(new SqlParameter("type", type));
                addCommand.Parameters.Add(new SqlParameter("msg_date", DateTime.Now));
                addCommand.Parameters.Add(new SqlParameter("user_id", user_id));
                addCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error adding message: " + ex.Message);
            }
        }

        public List<Sales> GetSales(string s)
        {
            List<Sales> list = new List<Sales>();
            SqlCommand getCmd = new SqlCommand(s, conn);
            using (SqlDataReader reader = getCmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    Sales sales = new Sales
                    {
                        Amount = (decimal)reader["amount"],
                        TDate = ((DateTime)reader["purchase_date"]).ToString("yyyy'-'MM'-'dd")

                };
                    list.Add(sales);
                }
            }

            return list;
        }

    }
}