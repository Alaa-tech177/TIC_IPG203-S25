// ======================================
// student contributing name: Toulin Masalkhi
// ====================================

// Static Class: tracks cumulative repair statistics across the entire application
// TotalRequests is cumulative —  increases without decrements
namespace CarRepairCenter.Statistics
{
    public static class RepairStatistics
    {
        // Static Property: counts every repair ever added (never decremented)
        public static int TotalRequests { get; private set; } = 0;

        // Static Method: increments the cumulative count when a new repair is registered
        public static void IncrementTotalRequests()
        {
            TotalRequests++;
        }
    }
}
