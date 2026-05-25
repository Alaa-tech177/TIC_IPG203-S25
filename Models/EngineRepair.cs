// ======================================
// student contributing name: Batoul Habhab
// ======================================

using System;

// Inheritance & Polymorphism: inherit from RepairRequest and override abstract methods
namespace CarRepairCenter.Models
{
    public class EngineRepair : RepairRequest
    {
        // encapsulation
        private string _engineType;

        public EngineRepair(int id, string carPlate, string ownerName, string engineType)
            : base(id, carPlate, ownerName)
        {
            _engineType = string.IsNullOrWhiteSpace(engineType) ? "Standard" : engineType.Trim();
        }

        // Polymorphism: implementation of  abstract PerformRepair()
        protected override void PerformRepair()
        {
            Console.WriteLine($"  [Engine] Diagnosing and repairing '{_engineType}' engine for {CarPlate}...");
        }

        // Polymorphism: implement the abstract GetRepairSummary()
        public override string GetRepairSummary()
        {
            return $"  #{Id} | ENGINE  | Plate: {CarPlate,-10} | Owner: {OwnerName,-15} | Engine: {_engineType,-12} | Status: {Status}";
        }
    }
}
