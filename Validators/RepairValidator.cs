// ===========================-===========
// student contributing name: Toulin Masalkhi
// ====================================

// Static Class: methods for input validation
namespace CarRepairCenter.Validators
{
    public static class RepairValidator
    {
        public static bool IsValidPlate(string plate)
            => !string.IsNullOrWhiteSpace(plate) && plate.Trim().Length >= 3;

        public static bool IsValidOwnerName(string name)
            => !string.IsNullOrWhiteSpace(name);

        public static bool IsValidTireCount(int count)
            => count >= 1 && count <= 4;
    }
}
