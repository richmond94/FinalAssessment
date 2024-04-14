using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public class Competitor
    {
        private int compNumber;
        private string compName;
        private int compAge;
        private string hometown;
        private bool newPB;
        private Event compEvent;
        private Result results;
        private CompHistory history;

        

        public Competitor(int compNumber, string compName, int compAge, string hometown)
        {
            this.compNumber = compNumber;
            this.compName = compName;
            this.compAge = compAge;
            this.hometown = hometown;
            
        }

        public override string ToString() 
        {
            string eventDesc = CompEvent != null ? CompEvent.ToString() : "No event";
            string resultsDesc = Results != null ? Results.ToString() : "No results";
            string historyDesc = History != null ? History.ToString() : "No history";
            return $"Competitor Number: {compNumber}, Name: {compName}, Age: {compAge}, Hometown: {hometown}, New Personal Best: {newPB}\nEvent: {eventDesc}\nResult: {resultsDesc}\nHistory: {historyDesc}\n";
        }

        public bool IsNewPB()
        {
            if (Results == null || History == null)
                return false;

            
            if (!newPB && Results.raceTime < History.personalBest)
            {
                History.personalBest = Results.raceTime;
                newPB = true;
                return true;
            }

            return false;
        }



        public string ToFile()
        {

            return $"{compNumber},{compName},{compAge},{hometown},{newPB}";
        }

        public Event CompEvent
        {
            get { return compEvent; }
            set { compEvent = value; }
        }

        public Result Results
        {
            get { return results; }
            set {  results = value; }
        }

        public CompHistory History
        { 
            get { return history; } 
            set {  history = value; }
        }

        public int GetCompNumber
        {
            get { return compNumber; }
        }

        public string GetCompName() 
        {
            return compName;        
        }

        public int GetCompAge()
        {
            return compAge;
        }

        public string GetHometown()
        {
            return hometown;
        }

        public bool GetNewPB
        {
            get { return newPB; }
            set { GetNewPB = value; }
        }

        public void SetNewPB(bool value)
        {
            newPB = value;
        }

        public int GetCareerWins()
        {
            if (results != null)
            {
                return history.careerWins;
            }
            else
            {
                return 0; 
            }
        }
    }
}
