using habitTracker;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace HabitTracker
{
    internal static class MenuHelper
    {
        private static RecordsDB _recordsDB = new();

        internal static string[] MenuOptions = new string[]

         {   "Please select an option:",
                "Q  Quit",
                "V  View all records",
                "A  Add a new record",
                "U  Update a record",
                "D  Delete a record"
         };

        internal static void Welcome()
        {
            Console.WriteLine("Welcome to Habit Tracker!");
        }
    
        internal static void GoBackToMenu()
        {
            Console.WriteLine("Press  any key to return to the Main Menu");
            Console.ReadLine();
            Console.Clear();
        }
        internal static void EnterValidValue()
        {
            Console.WriteLine("Invalid Input, please try again.");
        }
        internal static void Quit()
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
        internal static void DisplayMenu()
        {
            Console.Clear();
            foreach (string option in MenuOptions)
            {
                Console.WriteLine(option);
            };
        }
        internal static RunningRecord GetInputData()
        {
            var runningRecord = new RunningRecord();
            Console.WriteLine("Please enter the date of your run (YYYY-MM-DD):");
            runningRecord.Date = Console.ReadLine();

            Console.WriteLine("Please enter the duration of your run (in minutes):");
            runningRecord.Duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the distance of your run (in km):");
            runningRecord.Distance = int.Parse(Console.ReadLine());
            return runningRecord;
        }

        internal static int GetRecordId()
        {
            Console.WriteLine("Please enter the ID of the record you wish to update:");
            int id = int.Parse(Console.ReadLine());
            return id;
        }

        internal static void GetOptionsAndUpdate(int Id)
        {
            var record = _recordsDB.GetRecordById(Id);
            Console.WriteLine($"This is the record you want to update");
            Console.WriteLine($"ID: {record.Id}, DATE: {record.Date} <-- Enter 1, DURATION: {record.Duration} <-- Enter 2, DISTANCE: {record.Distance} <-- Enter 3");
         
            var inputInProgress = true;

            while (inputInProgress)
            {
                var input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("enter new value for Date");
                    record.Date = Console.ReadLine();
                    Console.WriteLine("this value is udated, select another option or enter 5 to submit");
                } 
                 if(input == "2") 
                {
                    Console.WriteLine("enter new value for Duration");
                    record.Duration = int.Parse(Console.ReadLine());
                    Console.WriteLine("this value is udated, select another option or enter 5 to submit");
                }
                if(input == "3") 
                {
                    Console.WriteLine("enter new value for Distamce");
                    record.Distance = int.Parse(Console.ReadLine());
                    Console.WriteLine("this value is udated, select another option or enter 5 to submit");
                } 
                if(input == "5")
                {
                    Console.WriteLine("Changes submitted. You will return to the main menu.");
                    _recordsDB.UpdateRecord(record);
                    Console.WriteLine("Your record is updated");
                    inputInProgress = false;
                    GoBackToMenu();

                }
                else
                {
                    Console.WriteLine("Invalid input, please try again");
                }
            }
            GoBackToMenu();
        }

        internal static void GetAllRecords()
        {
            var records = _recordsDB.GetAllRecords();
            if (records.Count == 0)
            {
                Console.WriteLine("No records found");
            }
            else
            {
                Console.WriteLine("All records:");
            }
            foreach (var record in records)
            {
                Console.WriteLine($"ID: {record.Id}, DATE: {record.Date}, DURATION: {record.Duration}, DISTANCE: {record.Distance}");
            }
            GoBackToMenu();
        }


    }
}
