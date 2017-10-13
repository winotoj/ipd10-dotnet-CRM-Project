using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    class SalesPerRep
    {
        private int _RepId;
        private decimal _Total;
        private int _Mth;
        private string _FirstName;
        private string _LastName;

        public int RepId
        {
            get
            {
                return _RepId;
            }
            set
            {
                _RepId = value;
            }
        }
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
            }
        }
        public int Mth
        {
            get
            {
                return _Mth;
            }
            set
            {
                _Mth = value;
            }
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
    }
}
