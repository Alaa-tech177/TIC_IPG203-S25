// ======================================
// student contributing name: Batoul Habhab
// ====================================
using System;

// Inheritance & Polymorphism: inherit from RepairRequest +  overrides abstract methods
namespace CarRepairCenter.Models
{
    public class ElectricalRepair : RepairRequest
    {
        // Encapsulation
        private string _systemName;

        public ElectricalRepair(int id, string carPlate, string ownerName, string systemName)
            : base(id, carPlate, ownerName)
        {
            _systemName = string.IsNullOrWhiteSpace(systemName) ? "General" : systemName.Trim();
        }

        // Polymorphism: concrete implementation of the abstract PerformRepair()
        protected override void PerformRepair()
        {
            Console.WriteLine($"  [Electrical] Troubleshooting '{_systemName}' system on {CarPlate}...");
        }

        // Polymorphism: concrete implementation of the abstract GetRepairSummary()
        public override string GetRepairSummary()
        {
            return $"  #{Id} | ELEC.   | Plate: {CarPlate,-10} | Owner: {OwnerName,-15} | System: {_systemName,-11} | Status: {Status}";
        }
    }
}
