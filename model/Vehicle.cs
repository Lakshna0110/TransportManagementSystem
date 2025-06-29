using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Model { get; set; }
        public decimal Capacity { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    public Vehicle() { }
        public Vehicle(int vehicleID, string model, decimal capacity, string type, string status)
        {
            VehicleID = vehicleID;
            Model = model;
            Capacity = capacity;
            Type = type;
            Status = status;
        }
    }
}
