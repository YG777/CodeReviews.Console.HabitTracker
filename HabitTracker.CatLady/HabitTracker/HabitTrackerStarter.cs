using habitTracker;

namespace HabitTracker
{
    internal class HabitTrackerStarter
    {     
        bool closeApp = false;
        internal void StartMenu()
        {
         MenuHelper.Welcome();
            while (!closeApp)
            {
                var recordsDB = new RecordsDB();
                MenuHelper.DisplayMenu();
                var input = Console.ReadLine();
                switch (input)
                {
                    case "Q":
                        closeApp = true;
                        break;
                    case "V":
                        MenuHelper.GetAllRecords();
                        break;
                    case "A":
                        _ = new RunningRecord();
                        RunningRecord? runningRecord = MenuHelper.GetInputData();
                        recordsDB.AddRecord(runningRecord);                      
                        break;
                    case "U":
                        var id = MenuHelper.GetRecordId();
                        MenuHelper.GetOptionsAndUpdate(id);                     
                        break;
                    case "D":
                        id = MenuHelper.GetRecordId();
                        recordsDB.DeleteRecord(id);
                        break;
                    default:
                        MenuHelper.EnterValidValue();
                        break;
                }
                if (closeApp)
                {
                    MenuHelper.Quit();
                }
            }   
        }  
    }
}
