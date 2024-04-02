using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalAssessment
{
    public class BreastStroke : Event
    {
        private string eventType { get; set; }
        private int distance { get; set; }
        private double winningTime { get; set; }
        private bool newRecord { get; set; }

        public BreastStroke(int eventNo, string venue, int venueID, string eventDateTime, double record, string eventType, int distance, double winningTime, bool newRecord) : base(eventNo, venue, venueID, eventDateTime, record)
        {
            this.eventType = eventType;
            this.distance = distance;
            this.winningTime = winningTime;
            this.newRecord = newRecord;
        }

        public override string ToString()
        {
            return $"Event Number: {GetEventNo()}\n" +
                   $"Venue: {GetVenue()}\n" +
                   $"Venue ID: {GetVenueID()}\n" +
                   $"Event Date Time: {GetEventDateTime()}\n" +
                   $"Record: {record}\n" +
                   $"Event Type: {eventType}\n" +
                   $"Distance: {distance}m\n" +
                   $"Winning Time: {winningTime}s\n" +
                   $"New Record: {(newRecord ? "Yes" : "No")}";
        }

        public bool IsNewRecord()
        {
            return winningTime < record;
        }

        public string ToFile()
        {

            string filePath = @"D:\College Submissions\OOPS\FinalAssessmentCsv\BreastStroke.csv";


            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
                writer.WriteLine("Event Number,Venue,Venue ID,Event Date Time,Record,Event Type,Distance,Winning Time,New Record");              
                writer.WriteLine($"{GetEventNo()},{GetVenue()},{GetVenueID()},{GetEventDateTime()},{record},{eventType},{distance},{winningTime},{(newRecord ? "Yes" : "No")}");
            }

            return filePath;
        }


    }
}
