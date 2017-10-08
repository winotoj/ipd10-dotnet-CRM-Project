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
        private string _Country;
        private string _Postal;
        private string _Phone;
        private string _Fax;
        private string _ContactFirstName;
        private string _ContactLastName;
        private DateTime _CreatedDate;
        private bool _Status;
        private int _SalesRepId;
        private string _Email;
        private string _LastPurchaseDate;
        private string _Amount;

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
                    
                    throw new ArgumentOutOfRangeException("Company Name must be between 2 and 80 char", value);

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
                    throw new ArgumentOutOfRangeException("Street must be between 2 and 50 char", value);
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
                    throw new ArgumentOutOfRangeException("City must be between 2 and 50 char", value);
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
                    throw new ArgumentOutOfRangeException("Province must be 2 char", value);
                }
            }
        }
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                if (value.Length > 1 && value.Length < 51)
                {
                    _Country = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Country must be between 2 and 50 char", value);
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
                if (value.Length > 5 && value.Length < 11)
                {
                    _Postal = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Postal Code must be between 6 and 10 char", value);
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
                    throw new ArgumentOutOfRangeException("Phone must be between 9 and 16 char", value);
                }
            }
        }
        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                if (value.Length > 9 && value.Length < 16)
                {
                    _Fax = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Fax must be between 9 and 16 char", value);
                }
            }
        }
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;


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
                    throw new ArgumentOutOfRangeException("First name must be between 2 and 40 char", value);
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
                    throw new ArgumentOutOfRangeException("Last name must be between 2 and 40 char", value);
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
                _CreatedDate = value;
            }
        }
        public string LastPurchaseDate
        {
            get
            {
                return _LastPurchaseDate;
            }
            set
            {
                _LastPurchaseDate = value;
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
            set
            {
                _SalesRepId = value;
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
                    throw new ArgumentOutOfRangeException("Email must be between 2 and 80 char", value);
                }
            }
        }
    }
}
