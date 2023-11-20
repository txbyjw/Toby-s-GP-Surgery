using System;
using System.Collections.Generic;

class Nurse
{
    private string FirstName;
    private string LastName;
    private string Speciality;

    public string firstName
    {
        get {return FirstName;}
        set {FirstName = value;}
    }

    public string lastName
    {
        get {return LastName;}
        set {LastName = value;}
    }

    public string speciality
    {
        get {return Speciality;}
        set {Speciality = value;}
    }
}
public class Nurse_Program
{
    public static void nurseMain()
    {
        List<Nurse> nurses = new List<Nurse>();
        LoadNursesFromFile("nursedata.txt", nurses);

        bool running = true;
        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add a nurse;");
            Console.WriteLine("2. List all nurses;");
            Console.WriteLine("3. Search for a nurse;");
            Console.WriteLine("4. Exit.");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AddNurse(nurses);
                    break;
                case "2":
                    ListNurses(nurses);
                    break;
                case "3":
                    SearchNurses(nurses);
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        SaveNursesToFile("nursedata.txt", nurses);
    }

    static void AddNurse(List<Nurse> nurses)
    {
        Console.WriteLine("Enter the nurse's first name:");
        string FirstName = Console.ReadLine();
        Console.WriteLine("Enter the nurse's last name:");
        string LastName = Console.ReadLine();
        Console.WriteLine("Enter the nurse's speciality:");
        string Speciality = Console.ReadLine();

        nurses.Add(new Nurse { firstName = FirstName, lastName = LastName, speciality = Speciality });
        string filePath = @"C:\Users\tobyj\OneDrive\Desktop\Toby's Surgery Software\Program\data\nursedata.txt";
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var nurse in nurses)
                {
                    sw.WriteLine($"Name: {nurse.firstName} {nurse.lastName}, Specialisation: {nurse.speciality}");
                }
                Console.WriteLine("Nurse details written to nursedata.txt");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
        }
    }

    static void ListNurses(List<Nurse> nurses)
    {
        foreach (var nurse in nurses)
        {
            Console.WriteLine($"Name: {nurse.firstName} {nurse.lastName}, Specialisation: {nurse.speciality}");
        }
    }

    static void SearchNurses(List<Nurse> nurses)
    {
        Console.WriteLine("Enter the nurse's last name to search:");
        string searchLastName = Console.ReadLine();
        var foundNurses = nurses.FindAll(p => p.lastName.Equals(searchLastName));
        if (foundNurses.Count > 0)
        {
            Console.WriteLine("Matching nurses:");
            foreach (var nurse in foundNurses)
            {
                Console.WriteLine($"Name: {nurse.firstName} {nurse.lastName}, Specialisation: {nurse.speciality}");
            }
        }
        else
        {
            Console.WriteLine("No matching nurses found.");
        }

    }

    static void LoadNursesFromFile(string filePath, List<Nurse> nurses)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] nurseData = line.Split(',');
                if (nurseData.Length == 3)
                {
                    string firstName = nurseData[0];
                    string lastName = nurseData[1];
                    string speciality = nurseData[2];
                    Nurse nurse = new Nurse()
                    {
                    firstName = firstName,
                    lastName = lastName,
                    speciality = speciality
                    };
                    nurses.Add(nurse);
                }
            }
        }
    }

    static void SaveNursesToFile(string filePath, List<Nurse> nurses)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var nurse in nurses)
            {
                sw.WriteLine($"{nurse.firstName},{nurse.lastName},{nurse.speciality}");
            }
        }
    }
}