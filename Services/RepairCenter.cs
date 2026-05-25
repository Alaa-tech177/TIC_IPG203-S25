// ======================================
// student contributing name: Alaa Na'ni
// ====================================
using System;
using System.Collections.Generic;
using System.Linq;
using CarRepairCenter.Interfaces;
using CarRepairCenter.Models;
using CarRepairCenter.Statistics;

// Service class: manages list of repair requests
// Polymorphism by ProcessAll() 
namespace CarRepairCenter.Services
{
    public class RepairCenter
    {
        // Encapsulation: private list 
        private List<RepairRequest> _requests = new List<RepairRequest>();

        // Adds a repair request and updates the statistics counter
        public void AddRequest(RepairRequest r)
        {
            _requests.Add(r);
            RepairStatistics.IncrementTotalRequests(); // Static class 
        }

        // Removes a PENDING request by ID + returns false if not found or completed
        public bool RemoveRequest(int id)
        {
            var req = _requests.FirstOrDefault(r => r.Id == id && r.Status == "Pending");
            if (req == null) return false;
            _requests.Remove(req);
            return true;
        }

        // Polymorphism
        public void ProcessAll()
        {
            var pending = _requests.Where(r => r.Status == "Pending").ToList();
            if (pending.Count == 0)
            {
                Console.WriteLine("  No pending repairs to process.");
                return;
            }
            foreach (var r in pending)
                r.StartRepair();   
        }

        // Uses IRepairService as a type to print info for all requests
        public void DisplayAllServiceInfo()
        {
            foreach (IRepairService service in _requests)
                RepairRequest.DisplayServiceInfo(service);
        }

        public List<RepairRequest> GetRequests() => _requests;
    }
}
