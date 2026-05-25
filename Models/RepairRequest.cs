// ====================================-====
// student contributing name: [Lama Mohammad Bakro]
// =====================-====================

using System;
using CarRepairCenter.Interfaces;

// Abstraction: abstract class that implements IRepairService
// Encapsulation: all fields are private; access is controlled via properties
namespace CarRepairCenter.Models
{
    public abstract class RepairRequest : IRepairService
    {
        // Delegates & Events: used to notify when a repair is completed
        public delegate void RepairCompletedHandler(string message);
        public event RepairCompletedHandler OnRepairCompleted;

        // Encapsulation
        private readonly int _id;       // Read-only — set once in constructor, never changed
        private string _carPlate;
        private string _ownerName;
        private string _status;         // No public setter 

        // Id can't be modified after construction
        public int Id => _id;

        // Car Plate validate on set
        public string CarPlate
        {
            get => _carPlate;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Car plate cannot be empty.");
                _carPlate = value.Trim().ToUpper();
            }
        }

        public string OwnerName
        {
            get => _ownerName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Owner name cannot be empty.");
                _ownerName = value.Trim();
            }
        }

        // Status is publicly readable but set directly by _status 
        public string Status => _status;

        protected RepairRequest(int id, string carPlate, string ownerName)
        {
            _id = id;
            CarPlate = carPlate;
            OwnerName = ownerName;
            _status = "Pending";
        }

        // Abstraction 
        protected abstract void PerformRepair();

        // Template Method + Polymorphism
        public void StartRepair()
        {
            PerformRepair();
            _status = "Completed";      // Direct field access
            OnRepairCompleted?.Invoke($"Repair completed for car: {CarPlate}");
        }

        public abstract string GetRepairSummary();

        // use IRepairService as parameter type 
        public static void DisplayServiceInfo(IRepairService service)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"  [Service Info] {service.GetRepairSummary()}");
            Console.ResetColor();
        }
    }
}
