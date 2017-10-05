using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject
{
    class Customer
    {
        private string _CompanyName;
        private string _Street;
        private string _City;
        private string _Province;
        private string _Postal;
        private string _Phone;
        private string _ContactFirstName;
        private string _ContactLastName;
        private DateTime _CreatedDate;
        private bool _Status;
        private int _SalesRepId;
        private string _Email;


        //need to change validation using regex
        //combobox for province?
        public int CustomerId { get; set; }
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                if (value.Length > 1 && value.Length < 81)
                {
                    _CompanyName = value;
                }
                else
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
            }
        }
        public string Street
        {
            get
            {
                return _Street;
            }
            set
            {
                if (value.Length > 1 && value.Length < 51)
                {
                    _Street = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                if (value.Length > 1 && value.Length < 51)
                {
                    _City = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string Province
        {
            get
            {
                return _Province;
            }
            set
            {
                if (value.Length == 2)
                {
                    _Province = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string Postal
        {
            get
            {
                return _Postal;
            }
            set
            {
                if (value.Length == 2)
                {
                    _Postal = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                if (value.Length > 9 && value.Length < 16)
                {
                    _Phone = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string ContactFirstName
        {
            get
            {
                return _ContactFirstName;
            }
            set
            {
                if (value.Length > 1 && value.Length < 41)
                {
                    _ContactFirstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public string ContactLastName
        {
            get
            {
                return _ContactLastName;
            }
            set
            {
                if (value.Length > 1 && value.Length < 41)
                {
                    _ContactLastName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
        public DateTime CreateDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = DateTime.Now;
            }
        }
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        public int SalesRepId
        {
            get
            {
                return _SalesRepId;
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
                if (value.Length > 5 && value.Length < 81)
                {
                    _Email = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("String must be between 2 and 50 char", value);
                }
            }
        }
    }
}
