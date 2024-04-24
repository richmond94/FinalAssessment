using FinalAssessment;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;


public class Program
{
    static Competition competition = new Competition();

    public static void Main(string[] args)
    {
        competition.LoadFromFile(@"D:\College Submissions\OOPS\FinalAssessmentCsv\Competitor.csv");
        while (true)
        {
            
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Competitor");
            Console.WriteLine("2. Delete Competitor");
            Console.WriteLine("3. Clear All Competitors");
            Console.WriteLine("4. Print Competition Details");
            Console.WriteLine("5. Get All Competitors By Event");
            Console.WriteLine("6. Load Competition Data from File");
            Console.WriteLine("7. Save Competition Data to File");
            Console.WriteLine("8. View Competition Index");
            Console.WriteLine("9. Sort Competitors By Age");
            Console.WriteLine("10. View Winners");
            Console.WriteLine("11. View Qualifiers");
            Console.WriteLine("12. Exit");

            Console.Write("Enter your choice: ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AddCompetitor();
                    break;
                case 2:
                    Console.Clear();
                    DeleteCompetitor();
                    break;
                case 3:
                    Console.Clear();
                    competition.ClearAll();
                    break;
                case 4:
                    Console.Clear();
                    competition.PrintComp();
                    break;
                case 5:
                    Console.Clear();
                    GetAllByEvent();
                    break;
                case 6:
                    Console.Clear();
                    competition.LoadFromFile(@"D:\College Submissions\OOPS\FinalAssessmentCsv\Competitor.csv");
                    break;
                case 7:
                    Console.Clear();
                    competition.SaveToFile(@"D:\College Submissions\OOPS\FinalAssessmentCsv\Competitor.csv");
                    break;
                case 8:
                    Console.Clear();
                    competition.DisplayCompIndex();
                    break;
                case 9:
                    Console.Clear();
                    competition.SortCompetitorsByAge();
                    break;
                case 10:
                    Console.Clear();
                    competition.Winners(20);
                    break;
                case 11:
                    Console.Clear();
                    competition.GetQualifiers();
                    break;
                case 12:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose a number between 1 and 12.");
                    break;
            }
        }
    }

   

    static void DeleteCompetitor()
    {
        Console.WriteLine("Enter Competitor Number to Delete:");
        int compNumber = int.Parse(Console.ReadLine());

        if (competition.DeleteCompetitor(compNumber))
        {
            Console.WriteLine("Competitor deleted successfully.");
        }
        else
        {
            Console.WriteLine("Competitor not found.");
        }
    }

    static void GetAllByEvent()
    {
        try
        {
            Console.WriteLine("Available Event IDs:");
            var eventIds = competition.GetAvailableEventIds();
            if (eventIds.Count == 0)
            {
                Console.WriteLine("No events available.");
                return;
            }

            foreach (var id in eventIds)
            {
                Console.WriteLine($"Event ID: {id}");
            }

           
            Console.WriteLine("\nEnter Event ID from the list above:");
            int eventId = int.Parse(Console.ReadLine());

            var competitorsByEvent = competition.GetAllByEvent(eventId);
            if (competitorsByEvent.Count > 0)
            {
                foreach (var competitor in competitorsByEvent)
                {
                    Console.WriteLine(competitor);
                }
            }
            else
            {
                Console.WriteLine("No competitors found for the specified event.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the event ID.");
        }
    }

    static void AddCompetitor()
    {
        Console.WriteLine("Enter Competitor Number:");
        int compNumber = GetValidCompNumber();

        if (competition.CheckCompetitor(compNumber))
        {
            Console.WriteLine("A competitor with this number already exists. Please use a different number.");
            return;
        }

        string compName = GetValidText("Enter Competitor Name:");
        int compAge = GetValidCompAge();
        string hometown = GetValidText("Enter Competitor Hometown:");

        int eventNo = GetValidEventID();

        Event compEvent = null; 

        if (competition.CheckEvent(eventNo))
        {
            compEvent = competition.GetEvent(eventNo);
            if (compEvent == null)
            {
                Console.WriteLine("No event found with the provided ID.");
                return;
            }
            Console.WriteLine("Event found and will be attached to the new competitor.");
        }
        else
        {
            Console.WriteLine("No existing event found with this ID. Proceeding to create a new event.");

            string eventType = GetValidEventType();  
            int venueID = GetValidVenueID();
            string venue = GetValidText("Venue:");
            string eventDateTime = GetValidEventDateTime();
            double record = GetValidDouble("Record: ");
            Console.WriteLine("Distance:");
            int distance = GetValidDistance();
            double winningTime = GetValidDouble("Winning Time: ");
            bool newRecord = false;

            switch (eventType.ToLower())
            {
                case "breaststroke":
                    compEvent = new BreastStroke(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                    newRecord = ((BreastStroke)compEvent).IsNewRecord();
                    ((BreastStroke)compEvent).SetNewRecord(newRecord);
                    break;
                case "backstroke":
                    compEvent = new BackStroke(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                    newRecord = ((BackStroke)compEvent).IsNewRecord();
                    ((BackStroke)compEvent).SetNewRecord(newRecord);
                    break;
                case "butterfly":
                    compEvent = new Butterfly(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                    newRecord = ((Butterfly)compEvent).IsNewRecord();
                    ((Butterfly)compEvent).SetNewRecord(newRecord);
                    break;
                case "frontcrawl":
                    compEvent = new FrontCrawl(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, newRecord);
                    newRecord = ((FrontCrawl)compEvent).IsNewRecord();
                    ((FrontCrawl)compEvent).SetNewRecord(newRecord);
                    break;
                
            }

            
        }

        Console.WriteLine("Enter Result Details:");
        Console.WriteLine("Placed:");
        int placed = GetValidPlacement();
        double raceTime = GetValidDouble("Race Time: ");
        Result results = new Result(placed, raceTime, false);
        bool qualified = results.isQualified();
        results.qualified = qualified;

        Console.WriteLine("Enter Competitor's Competition History:");
        string mostRecentWin = GetValidText("Place Of Most Recent Win:");
        Console.WriteLine("How Many Wins In Career:");
        int careerWins = GetValidCareerWins();
        Console.WriteLine("Medals Acquired:");
        List<string> medals = Console.ReadLine().Split(',').ToList();
        double personalBest = GetValidDouble("Personal Best Time: ");
        CompHistory history = new CompHistory(mostRecentWin, careerWins, medals, personalBest);

        Competitor competitor = new Competitor(compNumber, compName, compAge, hometown)
        {
            CompEvent = compEvent,
            Results = results,
            History = history
        };

        bool newPB = competitor.IsNewPB();
        competitor.SetNewPB(newPB);
        competition.AddCompetitor(competitor);
        Console.WriteLine("Competitor added successfully.");
    }

    private static string GetValidEventType()
    {
        
        var eventTypes = new List<string> { "breastStroke", "backStroke", "butterfly", "frontCrawl" };

       
        Console.WriteLine("Available Event Types:");
        for (int i = 0; i < eventTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {eventTypes[i]}");
        }

        int choice = 0;
        do
        {
            Console.WriteLine("Please enter the number corresponding to the event type:");

            
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= eventTypes.Count)
            {
                break;  
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select a valid number from the list.");
            }
        }
        while (true);

        
        return eventTypes[choice - 1]; 
    }

    static int GetValidEventID()
    {
        int eventNo = 0;
        while (eventNo < 1 || eventNo > 100)
        {
            Console.WriteLine("Enter Event ID (1-100):");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out eventNo) || eventNo < 1 || eventNo > 100)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 100.");
                eventNo = 0; 
            }
        }
        return eventNo; 
    }

    private static int GetValidVenueID()
    {
        int venueID = -1;
        while (venueID < 0)
        {
            Console.WriteLine("Enter Venue ID:");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out venueID) || venueID < 0)
            {
                Console.WriteLine("Invalid input.");
                venueID = -1;
            }
        }
        return venueID;
    }

   
    private static string GetValidVenue()
    {
        string venue = "";
       
        Regex validTextRegex = new Regex(@"^[a-zA-Z\s]+$");

        while (true)
        {
            Console.WriteLine("Enter Venue:");
            venue = Console.ReadLine();

           
            if (string.IsNullOrWhiteSpace(venue) || !validTextRegex.IsMatch(venue))
            {
                Console.WriteLine("Invalid venue. Please enter a valid venue (text only).");
            }
            else
            {
                break; 
            }
        }
        return venue;
    }

    private static int GetValidDistance()
    {
        int distance = 0;
        while (distance < 50 || distance > 1500)
        {
            Console.WriteLine("Enter Distance (between 50 and 1500 meters):");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out distance) || distance < 50 || distance > 1500)
            {
                Console.WriteLine("Invalid input. Please enter a distance between 50 and 1500 meters.");
                distance = 0;
            }
        }
        return distance;
    }

    private static double GetValidWinningTime()
    {
        double winningTime = -1;
        while (true)
        {
            Console.WriteLine("Enter Winning Time (in seconds, e.g., 120.50):");
            string input = Console.ReadLine();
            if (IsValidDoubleWithTwoDecimals(input))
            {
                winningTime = double.Parse(input);
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive number for the winning time with exactly two decimal places.");
            }
        }
        return winningTime;
    }

    
    private static bool IsValidDoubleWithTwoDecimals(string numberStr)
    {
        
        if (double.TryParse(numberStr, out double testDouble) && testDouble > 0)
        {
            
            int decimalIndex = numberStr.IndexOf('.');
            if (decimalIndex != -1) 
            {
                
                string decimalPart = numberStr.Substring(decimalIndex + 1);
                if (decimalPart.Length == 2)
                {
                    return true; 
                }
            }
        }
        return false;
    }

    private static int GetValidCompNumber()
    {
        int compNumber;
        while (true)
        {
            Console.WriteLine("Enter Competitor Number (100-999):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out compNumber) && compNumber >= 100 && compNumber <= 999)
            {
                break;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 100 and 999.");
            }
        }
        return compNumber;
    }

    private static int GetValidCompAge()
    {
        int compAge = 0;
        while (true)
        {
            Console.WriteLine("Enter Competitor Age (must be greater than 0):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out compAge) && compAge > 0)
            {
                break;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer for age.");
            }
        }
        return compAge;
    }

    private static string GetValidText(string prompt, bool allowDigits = false)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty. Please enter valid text.");
                continue;
            }

            
            if (!allowDigits && input.Any(char.IsDigit))
            {
                Console.WriteLine("Input must not contain numbers. Please enter valid text.");
                continue;
            }

            return input;
        }
    }

    private static double GetValidDouble(string prompt)
    {
        Regex twoDecimalPattern = new Regex(@"^\d+\.\d{2}$");
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (twoDecimalPattern.IsMatch(input) && double.TryParse(input, out double result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number with exactly two decimal places (e.g., 123.45).");
            }
        }
    }

    private static int GetValidPlacement()
    {
        int placed = 0;
        while (true)
        {
            Console.WriteLine("Enter Placement (1-8):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out placed) && placed >= 1 && placed <= 8)
            {
                break;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
            }
        }
        return placed;
    }

    private static int GetValidCareerWins()
    {
        int careerWins;
        while (true)
        {
            Console.WriteLine("Enter Career Wins (0 or more):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out careerWins) && careerWins >= 0)
            {
                break;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non-negative integer.");
            }
        }
        return careerWins;
    }

    private static string GetValidEventDateTime()
    {
        DateTime dateTime;
        while (true)
        {
            Console.WriteLine("Enter Event Date/Time (format: YYYY/MM/DD HH:MM):");
            string input = Console.ReadLine();

            
            if (!string.IsNullOrEmpty(input) && DateTime.TryParseExact(input, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return input;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a date and time in the correct format.");
            }
        }
    }
}
