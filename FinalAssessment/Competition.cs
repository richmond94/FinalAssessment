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
        private List<Event> events;
       

        public Competition() 
        {
            competitors = new List<Competitor>();
            events = new List<Event>();

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
                        fields[1].Trim(),            // CompName
                        int.Parse(fields[2].Trim()),  // CompAge
                        fields[3].Trim()             // Hometown
                    );

                    competitor.SetNewPB(bool.Parse(fields[4].Trim()));

                    // Create an Event based on EventType
                    if (!string.IsNullOrEmpty(fields[6].Trim()))
                    {
                        competitor.CompEvent = CreateEventFromFields(fields);
                    }

                    competitor.Results = new Result(
                        int.Parse(fields[14].Trim()),  // Placed
                            double.Parse(fields[15].Trim()),  // RaceTime
                            bool.Parse(fields[16].Trim())  // Qualified
                        );

                        competitor.History = new CompHistory(
                            fields[17].Trim(),  // MostRecentWin
                            int.Parse(fields[18].Trim()),  // CareerWins
                            fields[19].Split('|').Select(m => m.Trim()).ToList(),  // Medals
                            double.Parse(fields[20].Trim())  // PersonalBest
                        );

                        competitors.Add(competitor);
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

        private Event CreateEventFromFields(string[] fields)
        {
            int eventNo = int.Parse(fields[5].Trim());
            string venue = fields[7].Trim();
            int venueID = int.Parse(fields[8].Trim());
            string eventDateTime = fields[9].Trim();
            double record = double.Parse(fields[10].Trim());
            int distance = int.Parse(fields[11].Trim());
            double winningTime = double.Parse(fields[12].Trim());
            bool newRecord = bool.Parse(fields[13].Trim());
            string eventType = fields[6].Trim();

            switch (eventType)
            {
                case "BreastStroke":
                    return new BreastStroke(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                case "backStroke":
                    return new BackStroke(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                case "butterfly":
                    return new Butterfly(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                case "frontCrawl":
                    return new FrontCrawl(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                default:
                    throw new ArgumentException($"Unsupported event type: {eventType}");
            }
        }

        public Dictionary<string, string> CompIndex()
        {
           
            Dictionary<string, string> compEventIndex = new Dictionary<string, string>();

            foreach (var competitor in competitors)
            {
                string eventNumber = competitor.CompEvent != null ? competitor.CompEvent.GetEventNo().ToString() : "No Event";
                compEventIndex[competitor.GetCompNumber.ToString()] = eventNumber;
            }

            return compEventIndex;
        }

        public void DisplayCompIndex()
        {
            var index = CompIndex();
            Console.WriteLine("Competition Index (Competitor Number : Event Number):");
            foreach (var entry in index)
            {
                Console.WriteLine($"Competitor No: {entry.Key} is in Event No: {entry.Value}");
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
            var winners = competitors.Where(c => c.GetCareerWins() >= target).ToList();
            if (winners.Count > 0)
            {
                Console.WriteLine($"Competitors with more than {target} career wins:");
                foreach (var competitor in winners)
                {
                   
                    Console.WriteLine($"Number: {competitor.GetCompNumber}, Name: {competitor.GetCompName()}, Age: {competitor.GetCompAge()}, Hometown: {competitor.GetHometown()}");
                }
            }
            else
            {
                Console.WriteLine("No competitors found with more than " + target + " career wins.");
            }
        }

        public void GetQualifiers()
        {
           
            var qualifiers = competitors
                .Where(c => c.Results != null && c.Results.qualified)
                .OrderBy(c => c.GetCompNumber)   
                .ThenBy(c => c.Results.raceTime) 
                .ToList();

            if (qualifiers.Count > 0)
            {
                Console.WriteLine("Qualified Competitors:");
                foreach (var qualifier in qualifiers)
                {
                    Console.WriteLine($"Event ID: {qualifier.CompEvent.GetEventNo()}, Competitor Number: {qualifier.GetCompNumber}, Name: {qualifier.GetCompName()}, Placed: {qualifier.Results.placed}, Race Time: {qualifier.Results.raceTime}");
                }
            }
            else
            {
                Console.WriteLine("No qualifiers found.");
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

        public bool CheckCompetitor(int compNo)
        {
           
            return competitors.Any(c => c.GetCompNumber == compNo);
        }

        public bool CheckEvent(int eventNo)
        {
            return competitors.Any(c => c.CompEvent != null && c.CompEvent.GetEventNo() == eventNo);
        }

        public Competitor GetCompetitor(int compNo)
        {
            return competitors.FirstOrDefault(c => c.GetCompNumber == compNo);
        }

        public Event GetEvent(int eventNo)
        {
            
            var competitor = competitors.FirstOrDefault(c => c.CompEvent != null && c.CompEvent.GetEventNo() == eventNo);
            return competitor?.CompEvent;
        }


    }
}
