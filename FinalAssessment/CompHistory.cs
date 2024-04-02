using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public class CompHistory
    {
        public String mostRecentWin;
        public int careerWins;
        public List<string> medals;
        public Double personalBest;

        public CompHistory(string mostRecentWin, int careerWins, List<string> medals, double personalBest)
        {
            mostRecentWin = mostRecentWin;
            careerWins = careerWins;
            medals = medals;
            personalBest = personalBest;
        }
    }
}
