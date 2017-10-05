using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    public class User
    {
        private string firstName;
        private string lastName;
        private string username;

        public User(string firstName, string lastName, string username )
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }        
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
        }

    }
}
