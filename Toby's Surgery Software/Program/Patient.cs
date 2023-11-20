using System;
using System.Collections.Generic;

//This class represents the patient with their first and last names, and date of birth;
class Patient
{
    //Setting up encapsulation to keep patient data secure;
    private string FirstName;
    private string LastName;
    private DateTime DateOfBirth;

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

    public DateTime dateOfBirth
    {
        get {return DateOfBirth;}
        set {DateOfBirth = value;}
    }
}
public class Patient_Program
{
    //"Entry Point" of the program;
    public static void patientMain()
    {
        //Initialises list in memory to hold patient information;
        List<Patient> patients = new List<Patient>();
        //Loads the patient information from the data file in the Toby's Surgery Software folder;
        LoadPatientsFromFile("patientdata.txt", patients);
        //Menu Loop
        bool running = true;
        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add a patient;");
            Console.WriteLine("2. List all patients;");
            Console.WriteLine("3. Search for a patient;");
            Console.WriteLine("4. Exit.");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AddPatient(patients);
                    break;
                case "2":
                    ListPatients(patients);
                    break;
                case "3":
                    SearchPatient(patients);
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        //Saves the patient's data to patientdata.txt;
        SavePatientsToFile("patientdata.txt", patients);
    }
    //Function to add a new patient to the list;
    static void AddPatient(List<Patient> patients)
    {
        Console.WriteLine("Enter the patient's first name:");
        string FirstName = Console.ReadLine();
        Console.WriteLine("Enter the patient's last name:");
        string LastName = Console.ReadLine();
        Console.WriteLine("Enter the patient's date of birth (DD-MM-YYYY):");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime DateOfBirth))
        {
            //Save new patient data to the patientdata.txt file.
            patients.Add(new Patient { firstName = FirstName, lastName = LastName, dateOfBirth = DateOfBirth });
            string filePath = @"C:\Users\tobyj\OneDrive\Desktop\Toby's Surgery Software\Program\data\patientdata.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var patient in patients)
                    {
                        sw.WriteLine($"Name: {patient.firstName} {patient.lastName}, Date of Birth: {patient.dateOfBirth.ToShortDateString()}");
                    }
                }
                Console.WriteLine("Patient details written to patientdata.txt");
            }
            //Catch is required to use the 'try' method. It also catches any errors and writes them to the console.
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid date format. Patient not added.");
        }
    }

//Lists patients from patientdata.txt
    static void ListPatients(List<Patient> patients)
    {
        foreach (var patient in patients)
        {
            Console.WriteLine($"Name: {patient.firstName} {patient.lastName}, Date of Birth: {patient.dateOfBirth.ToShortDateString()}");
        }
    }

//Searches for a patient in patientdata.txt
    static void SearchPatient(List<Patient> patients)
    {
        Console.WriteLine("Enter the patient's last name to search:");
        string searchLastName = Console.ReadLine();
        var foundPatients = patients.FindAll(p => p.lastName.Equals(searchLastName));
        if (foundPatients.Count > 0)
        {
            Console.WriteLine("Matching patients:");
            foreach (var patient in foundPatients)
            {
                Console.WriteLine($"Name: {patient.firstName} {patient.lastName}, Date of Birth: {patient.dateOfBirth.ToShortDateString()}");
                //Used ToShortDateString() otherwise it would automatically add the time they entered the information, completely pointless data.
            }
        }
        else
        {
            Console.WriteLine("No matching patient found.");
        }

    }

    //Function to load patients from the file;
    static void LoadPatientsFromFile(string filePath, List<Patient> patients)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] patientData = line.Split(',');
                if (patientData.Length == 3)
                {
                    string firstName = patientData[0];
                    string lastName = patientData[1];
                    if (DateTime.TryParse(patientData[2], out DateTime dateOfBirth))
                    {
                        Patient patient = new Patient
                        {
                            firstName = firstName,
                            lastName = lastName,
                            dateOfBirth = dateOfBirth
                        };
                        patients.Add(patient);
                    }
                }
            }
        }
    }

    //Function to save the patients in memory to the file.
    static void SavePatientsToFile(string filePath, List<Patient> patients)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var patient in patients)
            {
                sw.WriteLine($"{patient.firstName},{patient.lastName},{patient.dateOfBirth}");
            }
        }
    }
}