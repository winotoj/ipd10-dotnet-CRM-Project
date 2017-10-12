﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRMProject.Domain
{
    class Schedule
    {
        private string _Type;
        private string _Note;
        private int _Status;
        private DateTime _ScheduleDate;
        private String _StartTime;
        private String _EndTime;
        private DateTime _CreatedDate;
        private string _Subject;
        private int _SalesRepId;
        private int _CustomerID;
        
        public string CustomerName { get; set; }

        //TODO: add validation
        
        public int Schedule_id { get; set; }

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

        public int Status
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

        public DateTime ScheduleDate
        {
            get
            {
                return _ScheduleDate;
            }
            set
            {
                _ScheduleDate = value;
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

        public string StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }

        }

        public string EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
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

        public int CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
            }
        }
        
    }
}
