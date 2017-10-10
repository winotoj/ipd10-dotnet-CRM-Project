using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    public class User
    {
        private string _FirstName;
        private string _LastName;
        private string _Username;
        private string _Email;
        private string _Role;
        private int _UserId;

        public User()
        {

        }
        public User(string firstName, string lastName, string username, string role )
        {
            _FirstName = firstName;
            _LastName = lastName;
            _Username = username;
            _Role = role;
        }

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }

        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }

        public int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }

    }
}
