using System;
using System.Collections.Generic;

class Appointment
{
    private DateTime date;
    private DateTime time;

    public DateTime apDate
    {
        get {return date;}
        set {date = value;}
    }
    public DateTime apTime
    {
        get {return time;}
        set {time = value;}
    }
}

public class Appointment_Program
{
    public static void appointmentMain()
    {
        List<Appointment> appointments = new List<Appointment>();
        LoadAppointmentsFromFile("appointmentdata.txt", appointments);

        bool running = true;
        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Schedule an appointment;");
            Console.WriteLine("2. List all appointments;");
            Console.WriteLine("3. Search for an appointment;");
            Console.WriteLine("4. Exit.");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                NewAppointment(appointments);
                break;

                case "2":
                ListAppointments(appointments);
                break;

                case "3":
                SearchAppointments(appointments);
                break;

                case "4":
                running = false;
                break;

                default:
                Console.WriteLine("Invalid option.");
                break;
            }

            SaveAppointmentsToFile("appointmentdata.txt", appointments);
        }
    }

    static void NewAppointment(List<Appointment> appointments)
    {
        bool isDateCorrect = false;
        Console.WriteLine("Enter the date of the appointment: ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            isDateCorrect = true;
        }
        else
        {
            Console.WriteLine("Invalid date format. Appointment not added.");
        }
        Console.WriteLine("Enter the time of the appointment: ");
        if ((DateTime.TryParse(Console.ReadLine(), out DateTime time)) & isDateCorrect)
        {
            appointments.Add(new Appointment {apDate = date, apTime = time});
            string filePath = @"C:\Users\tobyj\OneDrive\Desktop\Toby's Surgery Software\Program\data\appointmentdata.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var appointment in appointments)
                    {
                        sw.WriteLine($"Appointment Date: {appointment.apDate.ToShortDateString()}, Time: {appointment.apTime.ToShortTimeString()}");
                    }
                }
                Console.WriteLine("Appointment details added to system.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid time format. Appointment not added.");
        }
    }

    static void ListAppointments(List<Appointment> appointments)
    {
        foreach (var appointment in appointments)
        {
            Console.WriteLine($"Appointment Date: {appointment.apDate.ToShortDateString()} Time: {appointment.apTime.ToShortTimeString()}.");
        }
    }

    static void SearchAppointments(List<Appointment> appointments)
    {
        Console.WriteLine("Enter the date of the appointment: ");
        string search = Console.ReadLine();
        var foundAppointments = appointments.FindAll(p => p.apDate.Equals(search));
        if (foundAppointments.Count > 0)
        {
            Console.WriteLine("Matching appointments:");
            foreach (var appointment in foundAppointments)
            {
                Console.WriteLine($"Appointment Date: {appointment.apDate.ToShortDateString()} Time: {appointment.apTime.ToShortTimeString()}.");
            }
        }
        else
        {
            Console.WriteLine("No matching appointments found.");
        }
    }

    static void LoadAppointmentsFromFile(string filePath, List<Appointment> appointments)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] appointmentData = line.Split(',');
                if (appointmentData.Length == 2)
                {
                    bool isDateCorrect = false;
                    if (DateTime.TryParse(appointmentData[0], out DateTime date))
                    {
                        isDateCorrect = true;
                        if ((DateTime.TryParse(appointmentData[1], out DateTime time)) & isDateCorrect)
                        {
                            Appointment appointment = new Appointment
                            {
                                apDate = date,
                                apTime = time
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }

        }
    }

    static void SaveAppointmentsToFile(string filePath, List<Appointment> appointments)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var appointment in appointments)
            {
                sw.WriteLine($"{appointment.apDate},{appointment.apTime}");
            }
        }
    }
}