using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public class CompHistory
    {
        public string mostRecentWin { get; set; }
        public int careerWins { get; set; }
        public List<string> medals { get; set; }
        public double personalBest { get; set; }

        public CompHistory(string mostRecentWin, int careerWins, List<string> medals, double personalBest)
        {
            this.mostRecentWin = string.IsNullOrEmpty(mostRecentWin) ? "No recent wins" : mostRecentWin;
            this.careerWins = careerWins;
            this.medals = medals ?? new List<string>();
            this.personalBest = personalBest;
        }

        public override string ToString()
        {
            var medalsOutput = medals != null ? String.Join(", ", medals) : "No medals";
            return $"Most Recent Win: {mostRecentWin}, Career Wins: {careerWins}, Medals: {medalsOutput}, Personal Best: {personalBest}";
        }

        public string ToFile()
        {
            return $"{mostRecentWin},{careerWins},{String.Join("|", medals)},{personalBest}";
        }
    }
}
