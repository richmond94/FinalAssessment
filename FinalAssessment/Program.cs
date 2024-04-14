using FinalAssessment;
using System.ComponentModel;


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
                    AddCompetitor();
                   
                    break;
                case 2:
                    DeleteCompetitor();
                    break;
                case 3:
                    competition.ClearAll();
                    break;
                case 4:
                    competition.PrintComp();
                    break;
                case 5:
                    GetAllByEvent();
                    break;
                case 6:
                    
                    break;
                case 7:
                    competition.SaveToFile(@"D:\College Submissions\OOPS\FinalAssessmentCsv\Competitor.csv");
                    break;
                case 8:
                    //ViewCompIndex();
                    break;
                case 9:
                    competition.SortCompetitorsByAge();
                    break;
                case 10:
                    //ViewWinners();
                    break;
                case 11:
                   // ViewQualifiers();
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

    static void AddCompetitor()
    {
        Console.WriteLine("Enter Competitor Number:");
        int compNumber = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Competitor Name:");
        string compName = Console.ReadLine();

        Console.WriteLine("Enter Competitor Age:");
        int compAge = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Competitor Hometown:");
        string hometown = Console.ReadLine();


        Console.WriteLine("Enter BreastStroke Results");
        Console.WriteLine("Event ID:");
        int eventNo = int.Parse(Console.ReadLine());
        Console.WriteLine("Venue ID:");
        int venueID = int.Parse(Console.ReadLine());
        Console.WriteLine("Venue:");
        string venue = Console.ReadLine();
        Console.WriteLine("Event Date/Time:");
        string eventDateTime = Console.ReadLine();
        Console.WriteLine("Swim Time:");
        double record = double.Parse(Console.ReadLine());
       
        string eventType = "BreastStroke";
        Console.WriteLine("Distance:");
        int distance = int.Parse(Console.ReadLine());
        Console.WriteLine("Winning Time:");
        double winningTime = double.Parse(Console.ReadLine());
       

        BreastStroke compEvent = new BreastStroke(eventNo, venue, venueID, eventDateTime, record, eventType, distance, winningTime, false);
        bool newRecord = compEvent.IsNewRecord();
        compEvent.SetNewRecord(newRecord);


        Console.WriteLine("Enter Result Details:");
        Console.WriteLine("Placed:");
        int placed = int.Parse(Console.ReadLine());
        Console.WriteLine("Race Time:");
        double raceTime = double.Parse(Console.ReadLine());


        Result results = new Result(placed, raceTime, false);
        bool qualified = results.isQualified();
        results.qualified = qualified;


        Console.WriteLine("Enter Competitor's Comp History:");
        Console.WriteLine("Place Of Most Recent Win:");
        string mostRecentWin = Console.ReadLine();
        Console.WriteLine("How Many Wins In Career:");
        int careerWins = int.Parse(Console.ReadLine());
        Console.WriteLine("Medals Acquired:");
        List<string> medals = Console.ReadLine().Split(',').ToList();
        Console.WriteLine("Personal Best Time:");
        double personalBest = double.Parse(Console.ReadLine());

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



}
