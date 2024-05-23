using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    


    public class Butterfly : Event
    {
        private string eventType { get; set; }
        private int distance { get; set; }
        private double winningTime { get; set; }
        private bool newRecord { get; set; }

        public Butterfly(int eventNo, string venue, int venueID, string eventDateTime, double record, string eventType, int distance, double winningTime, bool newRecord) : base(eventNo, venue, venueID, eventDateTime, record)
        {
            this.eventType = eventType;
            this.distance = distance;
            this.winningTime = winningTime;
            this.newRecord = newRecord;
        }

        public override string ToString()
        {
            return $"Event Number: {GetEventNo()}, Venue ID: {GetVenueID()}, Venue: {GetVenue()}, Event DateTime: {GetEventDateTime()}, Record: {GetRecord()}\n" +
           $"Event Type: {eventType}, Distance: {distance}m, Winning Time: {winningTime}s, New Record: {(newRecord ? "Yes" : "No")}";
        }

        public override Event GetEvent(string venue, int venueID)
        {
            if (!string.IsNullOrEmpty(venue))
                return new Butterfly(GetEventNo(), venue, 0, GetEventDateTime(), GetRecord(), eventType, distance, winningTime, newRecord);
            else
                return new Butterfly(GetEventNo(), "", venueID, GetEventDateTime(), GetRecord(), eventType, distance, winningTime, newRecord);
        }
        public string GetEventType()
        {
            return eventType;
        }

        public int GetDistance()
        {
            return distance;
        }

        public double GetWinningTime()
        {
            return winningTime;
        }

        public bool GetNewRecord()
        {

            return newRecord;
        }

        public bool IsNewRecord()
        {
            return winningTime < record;
        }

        public override string ToFile()
        {
            return $"{GetEventNo()},{GetEventType()},{GetVenue()},{GetVenueID()},{GetEventDateTime()},{GetRecord()},{GetDistance()},{GetWinningTime()},{(GetNewRecord() ? "True" : "False")}";
        }

        public void SetNewRecord(bool newRecord)
        {
            this.newRecord = newRecord;
        }


    }





}
