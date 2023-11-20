using System;
using System.Collections.Generic;

class Doctor
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialisation { get; set; }
}
public class Doctor_Program
{
    public static void doctorMain()
    {
        List<Doctor> doctors = new List<Doctor>();
        LoadDoctorsFromFile("doctordata.txt", doctors);

        bool running = true;
        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add a doctor;");
            Console.WriteLine("2. List all doctors;");
            Console.WriteLine("3. Search for a doctor;");
            Console.WriteLine("4. Exit.");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AddDoctor(doctors);
                    break;
                case "2":
                    ListDoctors(doctors);
                    break;
                case "3":
                    SearchDoctors(doctors);
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        SaveDoctorsToFile("doctordata.txt", doctors);
    }

    static void AddDoctor(List<Doctor> doctors)
    {
        Console.WriteLine("Enter the doctor's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter the doctor's last name:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter the doctor's speciality:");
        string speciality = Console.ReadLine();

        doctors.Add(new Doctor { FirstName = firstName, LastName = lastName, Specialisation = speciality });
        string filePath = @"C:\Users\tobyj\OneDrive\Desktop\Toby's Surgery Software\Program\data\doctordata.txt";
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var doctor in doctors)
                {
                    sw.WriteLine($"Name: {doctor.FirstName} {doctor.LastName}, Specialisation: {doctor.Specialisation}");
                }
                Console.WriteLine("Doctor details written to doctordata.txt");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
        }
    }

    static void ListDoctors(List<Doctor> doctors)
    {
        foreach (var doctor in doctors)
        {
            Console.WriteLine($"Name: {doctor.FirstName} {doctor.LastName}, Specialisation: {doctor.Specialisation}");
        }
    }

    static void SearchDoctors(List<Doctor> doctors)
    {
        Console.WriteLine("Enter the doctor's last name to search:");
        string searchLastName = Console.ReadLine();
        var foundDoctors = doctors.FindAll(p => p.LastName.Equals(searchLastName));
        if (foundDoctors.Count > 0)
        {
            Console.WriteLine("Matching doctors:");
            foreach (var doctor in foundDoctors)
            {
                Console.WriteLine($"Name: {doctor.FirstName} {doctor.LastName}, Specialisation: {doctor.Specialisation}");
            }
        }
        else
        {
            Console.WriteLine("No matching doctors found.");
        }

    }

    static void LoadDoctorsFromFile(string filePath, List<Doctor> doctors)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] doctorData = line.Split(',');
                if (doctorData.Length == 3)
                {
                    string firstName = doctorData[0];
                    string lastName = doctorData[1];
                    string speciality = doctorData[2];
                    Doctor doctor = new Doctor()
                    {
                    FirstName = firstName,
                    LastName = lastName,
                    Specialisation = speciality
                    };
                    doctors.Add(doctor);
                }
            }
        }
    }

    static void SaveDoctorsToFile(string filePath, List<Doctor> doctors)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var doctor in doctors)
            {
                sw.WriteLine($"{doctor.FirstName},{doctor.LastName},{doctor.Specialisation}");
            }
        }
    }

}