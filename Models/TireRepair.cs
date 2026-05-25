// ======================================
// student contributin name: Batoul Habhab
// ===================================

using System;

// Inheritance & Polymorphism: inherits from RepairRequest and overrides abstract methods
namespace CarRepairCenter.Models
{
    public class TireRepair : RepairRequest
    {
        // Encapsulation: private field
        private int _tireCount;

        public TireRepair(int id, string carPlate, string ownerName, int tireCount)
            : base(id, carPlate, ownerName)
        {
            _tireCount = tireCount;
        }

        // polymorphism: implementation  abstract PerformRepair()
        protected override void PerformRepair()
        {
            Console.WriteLine($"  [Tires] Replacing {_tireCount} tire(s) on {CarPlate}...");
        }

        // polymorphism: implementation of abstract GetRepairSummary()
        public override string GetRepairSummary()
        {
            return $"  #{Id} | TIRES   | Plate: {CarPlate,-10} | Owner: {OwnerName,-15} | Tires: {_tireCount,-12} | Status: {Status}";
        }
    }
}
