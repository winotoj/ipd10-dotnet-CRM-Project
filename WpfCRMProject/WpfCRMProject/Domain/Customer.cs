using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace WpfCRMProject
{
    class Customer : IDataErrorInfo
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


        public int CustomerId { get; set; }
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
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
                _Street = value;
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
                _City = value;
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
                _Province = value;
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
                _Country = value;
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
                _Postal = value;
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
                _Phone = value;
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
                _Fax = value;
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
                _ContactFirstName = value;
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
                _ContactLastName = value;
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
                _Email = value;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "CompanyName")
                {
                    if (string.IsNullOrEmpty(CompanyName) || CompanyName.Length < 2 || CompanyName.Length > 50)
                        result = "Please enter a Name (2-50 Chars)";
                }
                if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street) || Street.Length < 2 || Street.Length > 50)
                        result = "Please enter Street (2-50 chars)";
                }
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City) || City.Length < 2 || City.Length > 50)
                        result = "Please enter a Street (2-50 chars)";
                }
                if (columnName == "Province")
                {
                    if (string.IsNullOrEmpty(Province) || Province.Length != 2)
                        result = "Please enter a Province (2 chars)";
                }
                if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country) || Country.Length < 2 || Country.Length > 20)
                        result = "Please enter a Country (2-20 chars)";
                }
                if (columnName == "Postal")
                {
                    if (string.IsNullOrEmpty(Postal) || Postal.Length < 6 || Postal.Length > 10)
                        result = "Please enter a Postal Code (6-10 chars)";
                }

                if (columnName == "Phone")
                {
                    Regex regex = new Regex(@"\d{3}[- ]?\d{3}[- ]?\d{4}( x\d{4})?|x\d{4}$");
                    if (string.IsNullOrEmpty(Phone))
                    {
                        result = "Please enter a Phone Number (514-123-1234 x1234)";
                    }
                    else if (!regex.Match(Phone).Success)
                    {
                        result = "Please enter a Phone Number (514-123-1234 x1234)";
                    }
                }
                if (columnName == "Fax")
                {
                    if (!(string.IsNullOrEmpty(Fax)))
                    {
                        if (Fax.Length < 9 || Fax.Length > 16)
                            result = "Please enter a Fax Number (9-16 chars)";
                    }

                }
                if (columnName == "ContactFirstName")
                {
                    if (string.IsNullOrEmpty(ContactFirstName) || ContactFirstName.Length < 2 || ContactFirstName.Length > 50)
                        result = "Please enter a Name (2-50 Chars)";
                }
                if (columnName == "ContactLastName")
                {
                    if (string.IsNullOrEmpty(ContactLastName) || ContactLastName.Length < 2 || ContactLastName.Length > 50)
                        result = "Please enter a Name (2-50 Chars)";
                }
                if (columnName == "Email")
                {
                    Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                    if (string.IsNullOrEmpty(Email))
                        result = "Please enter an Email";
                    else if(!regex.Match(Email).Success)
                    {
                        result = "Please enter a valid Email";
                    }
                }

                return result;
            }
        }
    }
}
