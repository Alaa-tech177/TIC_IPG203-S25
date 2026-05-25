// ======================================
// student contributing name: Kinana Alsied 
// ====================================-=

using System;
using CarRepairCenter.Models;
using CarRepairCenter.Services;
using CarRepairCenter.Validators;
using CarRepairCenter.Statistics;

namespace CarRepairCenter
{
    class Program
    {
        private static int _nextId = 1;
        private static RepairCenter _center = new RepairCenter();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PrintHeader();

            bool running = true;
            while (running)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": AddEngineRepair();     break;
                    case "2": AddTireRepair();        break;
                    case "3": AddElectricalRepair();  break;
                    case "4": ProcessAllRepairs();    break;
                    case "5": ShowAllSummaries();     break;
                    case "6": ShowTotalRequests();    break;
                    case "7": DeleteRequest();        break;
                    case "0":
                        Console.WriteLine("\n  Goodbye! \n");
                        running = false;
                        break;
                    default:
                        PrintError("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // ─── Menu UI 
        static void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║          Car Repair Center            ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
        }

        static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  ┌─────────────────────────────────────────┐");
            Console.WriteLine("  │  1. Add engine repair                   │");
            Console.WriteLine("  │  2. add tire repair                     │");
            Console.WriteLine("  │  3. Add electrical repair               │");
            Console.WriteLine("  │  4. Process all pending repairs         │");
            Console.WriteLine("  │  5. show all Summaries                  │");
            Console.WriteLine("  │  6. show Total requests                 │");
            Console.WriteLine("  │  7. Delete a pending Request            │");
            Console.WriteLine("  │  0. Exit                                │");
            Console.WriteLine("  └─────────────────────────────────────────┘");
            Console.ResetColor();
        }

        //  Input Helpers 
        static string ReadPlate()
        {
            while (true)
            {
                Console.Write("  Car Plate (min 3 chars): ");
                string plate = Console.ReadLine()?.Trim();
                if (RepairValidator.IsValidPlate(plate)) return plate.ToUpper();
                PrintError("Invalid plate. Must be at least 3 characters.");
            }
        }

        static string ReadOwnerName()
        {
            while (true)
            {
                Console.Write("  Owner Name: ");
                string name = Console.ReadLine()?.Trim();
                if (RepairValidator.IsValidOwnerName(name)) return name;
                PrintError("Owner name cannot be empty.");
            }
        }

        //  Event Handler Delegate
    
        static void OnRepairDone(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"   EVENT: {message}");
            Console.ResetColor();
        }

        //  Add Repairs
        static void AddEngineRepair()
        {
            PrintSection("New Engine Repair");
            string plate = ReadPlate();
            string owner = ReadOwnerName();

            Console.Write("  Engine Type (e.g. V6, Diesel, Turbo): ");
            string engineType = Console.ReadLine()?.Trim();

            var repair = new EngineRepair(_nextId++, plate, owner, engineType);
            repair.OnRepairCompleted += OnRepairDone;   // Subscribe to event (Delegates & Events)
            _center.AddRequest(repair);

            PrintSuccess($"Engine repair #{repair.Id} added for {plate}.");
        }

        static void AddTireRepair()
        {
            PrintSection("New Tire Repair");
            string plate = ReadPlate();
            string owner = ReadOwnerName();

            int count = 0;
            while (true)
            {
                Console.Write("  Number of Tires to Replace (1-4): ");
                if (int.TryParse(Console.ReadLine(), out count) && RepairValidator.IsValidTireCount(count))
                    break;
                PrintError("Please enter a number between 1 and 4.");
            }

            var repair = new TireRepair(_nextId++, plate, owner, count);
            repair.OnRepairCompleted += OnRepairDone;   
            _center.AddRequest(repair);

            PrintSuccess($"Tire repair #{repair.Id} added for {plate}.");
        }

        static void AddElectricalRepair()
        {
            PrintSection("New Electrical Repair");
            string plate = ReadPlate();
            string owner = ReadOwnerName();

            Console.Write("  System Name (e.g. ABS, Airbag, Battery): ");
            string system = Console.ReadLine()?.Trim();

            var repair = new ElectricalRepair(_nextId++, plate, owner, system);
            repair.OnRepairCompleted += OnRepairDone;  
            _center.AddRequest(repair);

            PrintSuccess($"Electrical repair #{repair.Id} added for {plate}.");
        }

        //  Process 
        static void ProcessAllRepairs()
        {
            PrintSection("Processing All Pending Repairs");
            _center.ProcessAll();   // Polymorphism: calls correct override for the type
            Console.WriteLine();
        }

        //  Summaries 
        static void ShowAllSummaries()
        {
            PrintSection("All Repair Summaries");
            var requests = _center.GetRequests();

            if (requests.Count == 0)
            {
                Console.WriteLine("  No requests found.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {"#ID",-5} {"TYPE",-8} {"PLATE",-12} {"OWNER",-17} {"DETAILS",-15} STATUS");
            Console.WriteLine(new string('─', 72));
            Console.ResetColor();

            foreach (var r in requests)
            {
                Console.ForegroundColor = r.Status == "Completed" ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine(r.GetRepairSummary());
                Console.ResetColor();
            }
        }

        //  Total 
        static void ShowTotalRequests()
        {
            PrintSection("Total Requests");
            Console.ForegroundColor = ConsoleColor.Cyan;
            // Static class usage: RepairStatistics holds the cumulative historical count
            Console.WriteLine($"  Total repair requests ever registered: {RepairStatistics.TotalRequests}");
            Console.ResetColor();
        }

        //  Delete
        static void DeleteRequest()
        {
            PrintSection("Delete Pending Request");
            var pending = _center.GetRequests().FindAll(r => r.Status == "Pending");

            if (pending.Count == 0)
            {
                Console.WriteLine("  No pending requests to delete.");
                return;
            }

            Console.WriteLine("  Pending requests:");
            foreach (var r in pending)
                Console.WriteLine(r.GetRepairSummary());

            Console.Write("\n  Enter Request ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_center.RemoveRequest(id))
                    PrintSuccess($"Request #{id} removed successfully.");
                else
                    PrintError($"Could not remove request #{id}. It may not exist or is already completed.");
            }
            else
            {
                PrintError("Invalid ID.");
            }
        }

        //  Print Helpers 
        static void PrintSection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n── {title} ──");
            Console.ResetColor();
        }

        static void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Success! {msg}");
            Console.ResetColor();
        }

        static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Error! {msg}");
            Console.ResetColor();
        }
    }
}
