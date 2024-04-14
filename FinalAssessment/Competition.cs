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

        public bool DeleteCompetitor(int compNumber)
        {
            Competitor competitor = competitors.FirstOrDefault(c => c.GetCompNumber == compNumber);
            if (competitor != null)
            {
                competitors.Remove(competitor);
                return true;
            }
            return false;
        }


        public void ClearAll()
        {
            competitors.Clear();
        }

        public List<Competitor> GetAllByEvent(int eventId)
        {
            return competitors.Where(c => c.CompEvent != null && c.CompEvent.GetEventNo() == eventId).ToList();
        }

        public List<int> GetAvailableEventIds()
        {
            return competitors
                .Where(c => c.CompEvent != null)
                .Select(c => c.CompEvent.GetEventNo())
                .Distinct()
                .ToList();
        }

        public void SaveToFile(string filePath)
        {
            var csvLines = new List<string>();
            csvLines.Add("CompNumber,CompName,CompAge,Hometown,NewPB,EventID,EventType,Venue,VenueID,EventDateTime,Record,Distance,WinningTime,NewRecord,Placed,RaceTime,Qualified,MostRecentWin,CareerWins,Medals,PersonalBest");

            foreach (var competitor in competitors)
            {
               
                string competitorDetails = competitor.ToFile();  
                string eventDetails = competitor.CompEvent != null ? competitor.CompEvent.ToFile() : "?,?,?,?,?,?,?,?";
                string resultsDetails = competitor.Results != null ? competitor.Results.ToFile() : ",,";
                string historyDetails = competitor.History != null ? competitor.History.ToFile() : ",,,";

                
                string line = $"{competitorDetails},{eventDetails},{resultsDetails},{historyDetails}";
                csvLines.Add(line);
            }

           
            File.WriteAllLines(filePath, csvLines);
        }

        public void LoadFromFile(string filePath)
        {
            competitors.Clear(); 
            var csvLines = File.ReadAllLines(filePath).Skip(1);  

            foreach (var line in csvLines)
            {
                var fields = line.Split(',');

                try
                {
                    if (fields.Length < 21)  
                    {
                        Console.WriteLine($"Skipped line due to insufficient data: {line}");
                        continue;
                    }

                    
                    var competitor = new Competitor(
                        int.Parse(fields[0].Trim()),  // CompNumber
                        fields[1].Trim(),  // CompName
                        int.Parse(fields[2].Trim()),  // CompAge
                        fields[3].Trim()   // Hometown
                    );

                    
                    competitor.SetNewPB(bool.Parse(fields[4].Trim()));

                    
                    if (!string.IsNullOrEmpty(fields[6].Trim()))
                    {
                        Event evnt = null;
                        switch (fields[6].Trim())
                        {
                            case "BreastStroke":
                                evnt = new BreastStroke(
                                    int.Parse(fields[5].Trim()),  // EventNo
                                    fields[7].Trim(),  // Venue
                                    int.Parse(fields[8].Trim()),  // VenueID
                                    fields[9].Trim(),  // EventDateTime
                                    double.Parse(fields[10].Trim()),  // Record
                                    fields[6].Trim(),  // EventType
                                    int.Parse(fields[11].Trim()),  // Distance
                                    double.Parse(fields[12].Trim()),  // WinningTime
                                    bool.Parse(fields[13].Trim())  // NewRecord
                                );
                                break;
                                
                        }
                        competitor.CompEvent = evnt;
                    }

                    
                    if (!string.IsNullOrEmpty(fields[14].Trim()))
                    {
                        competitor.Results = new Result(
                            int.Parse(fields[14].Trim()),  // Placed
                            double.Parse(fields[15].Trim()),  // RaceTime
                            bool.Parse(fields[16].Trim())  // Qualified
                        );
                    }

                    if (!string.IsNullOrEmpty(fields[17].Trim()))
                    {
                        competitor.History = new CompHistory(
                            fields[17].Trim(),  // MostRecentWin
                            int.Parse(fields[18].Trim()),  // CareerWins
                            fields[19].Split('|').Select(m => m.Trim()).ToList(),  // Medals
                            double.Parse(fields[20].Trim())  // PersonalBest
                        );
                    }

                    competitors.Add(competitor);  
                    Console.WriteLine($"Loaded {competitor.GetCompName}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error parsing data for competitor from line: {line}. Error: {ex.Message}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Data format error (missing fields): {line}. Error: {ex.Message}");
                }
            }
        }



        public void SortCompetitorsByAge()
        {
           
            competitors.Sort((c1, c2) => c1.GetCompAge().CompareTo(c2.GetCompAge()));

            
            Console.WriteLine("Competitors sorted by age (Lowest to Highest):");
            foreach (var competitor in competitors)
            {
                
                Console.WriteLine($"Number: {competitor.GetCompNumber}, Name: {competitor.GetCompName()}, Age: {competitor.GetCompAge()}, Hometown: {competitor.GetHometown()}");
            }
        }

        public void Winners(int target)
        {
            var winners = competitors.Where(c => c.GetCareerWins() != null && c.GetCareerWins() > target);
            foreach (var winner in winners)
            {
                Console.WriteLine(winner);
            }
        }



        public void PrintComp()
        {
            if (competitors.Count == 0)
            {
                Console.WriteLine("No competitors to display.");
            }
            else
            {
                foreach (Competitor competitor in competitors)
                {
                    Console.WriteLine(competitor.ToString());
                }
            }
        }

        

    }
}
