using System;
using System.Collections.Generic;

class Menu
{
    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Patients;");
            Console.WriteLine("2. Appointments;");
            Console.WriteLine("3. Doctors;");
            Console.WriteLine("4. Nurses;");
            Console.WriteLine("5. Exit.");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                Patient_Program.patientMain();
                break;

                case "2":
                Appointment_Program.appointmentMain();
                break;
                
                case "3":
                Doctor_Program.doctorMain();
                break;

                case "4":
                Nurse_Program.nurseMain();
                break;

                case "5":
                running = false;
                break;

                default:
                Console.WriteLine("Invalid option.");
                break;
            }
        }
    }
}