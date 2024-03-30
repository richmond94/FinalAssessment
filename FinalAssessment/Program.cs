using FinalAssessment;

class Program
{
    static void Main(string[] args)
    {



        BreastStroke breastStroke = new BreastStroke(1, "Pool A", 123, "2024-03-21 10:00:00", 60.5, "Breaststroke", 100, 65.2, true);
        Competitor competitor = new Competitor(1, "Walter White", 25, "New Mexico");
        competitor.IsNewPB(true);

        Console.WriteLine(breastStroke.ToString());
        Console.WriteLine(competitor.ToString());
        string filePath = breastStroke.ToFile();
        filePath = competitor.ToFile();

        if (File.Exists(filePath))
        {
            Console.WriteLine($"CSV file created at: {filePath}");
        }
        else
        {
            Console.WriteLine("Failed to create CSV file.");
        }

    }
}