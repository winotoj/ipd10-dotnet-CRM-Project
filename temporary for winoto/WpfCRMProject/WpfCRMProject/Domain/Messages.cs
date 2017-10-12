using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    class Messages
    {

        private string _Subject;
        private string _Note;
        private DateTime _CreatedDate;
        private string _Type;
        private int _CustomerId;
        private int _UserId;
        
        public int MessageId { get; set; }
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
        public int CustomerID
        {
            get
            {
                return _CustomerId;
            }
            set
            {
                _CustomerId = value;
            }

        }
        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                _Subject = value;
            }
        }

        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
            }
        }

        public DateTime CreatedDate
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

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
    }
}
