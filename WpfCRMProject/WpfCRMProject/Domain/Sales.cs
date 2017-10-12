using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    class Sales
    {
        decimal _Amount;
        string _TDate;

        public decimal Amount
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
        public string TDate
        {
            get
            {
                return _TDate;
            }
            set
            {
                _TDate = value;
                ;
            }
        }

    }
}
