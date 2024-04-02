using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssessment
{
    public class Competition
    {
        private List<Competitor> competitors;

        public Competition() 
        {
            competitors = new List<Competitor>();
        }

        public void AddCompetitor(Competitor c)
        {
            competitors.Add(c);
        }

        public void RemoveCompetitor(Competitor c)
        {
            competitors.Remove(c); 
        }

        public void ClearAll()
        {
            competitors.Clear();
        }
    }
}
