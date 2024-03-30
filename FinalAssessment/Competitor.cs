using System;
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
        private BreastStroke compEvent;
        private Result results;
        private CompHistory history;

        public Competitor(int compNumber, string compName, int compAge, string hometown)
        {
            this.compNumber = compNumber;
            this.compName = compName;
            this.compAge = compAge;
            this.hometown = hometown;
            this.newPB = false; 
        }

        public override string ToString() 
        { 
            return $"Competitor Number: {compNumber}\n" +
                   $"Name: {compName}\n" +
                   $"Age: {compAge}\n" +
                   $"Hometown: {hometown}";
        }

        public bool IsNewPB(bool v)
        {
            if (results == null || history == null)
                return false; 

            
            if (!newPB || results.raceTime < history.personalBest)
            {
                
                history.personalBest = results.raceTime;
                newPB = true;
                return true; 
            }

            return false; 
        }

        

        public string ToFile()
        {

            string filePath = @"D:\College Submissions\OOPS\FinalAssessmentCsv\Competitor.csv";


            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("Competitor Number,Competitor Name,Age,Town,Personal Best?");
                writer.WriteLine($"{compNumber},{compName},{compAge},{hometown},{(newPB ? "Yes" : "No")}");

            }

            return filePath;
        }

    }
}
