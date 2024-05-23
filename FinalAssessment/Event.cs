using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public abstract class Event
    {
        private int eventNo { get; set; }
        private string venue { get; set; }
        private int venueID { get; set; }
        private string eventDateTime { get; set; }
        protected double record { get; set; }

        protected Event(int eventNo, string venue, int venueID, string eventDateTime, double record)
        {
            this.eventNo = eventNo;
            this.venue = venue;
            this.venueID = venueID;
            this.eventDateTime = eventDateTime;
            this.record = record;
        }
        public virtual string ToFile()
        {
            
            return $"{eventNo}";
        }

        public abstract Event GetEvent(string venue, int venueID);

        public int GetEventNo()
        {
            return this.eventNo;
        }

        public string GetVenue()
        {
            return venue;
        }

        public int GetVenueID()
        {
            return venueID;
        }

        public string GetEventDateTime()
        {
            return eventDateTime;
        }

        public double GetRecord() 
        {
            return record;
        }
    }
}
