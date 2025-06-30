using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Trip
    {
        public int TripID { get; set; }
        public int VehicleID { get; set; }
        public int RouteID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; }
        public string TripType { get; set; }
        public int MaxPassengers { get; set; }  
    public Trip() { }
        public Trip(int tripID, int vehicleID, int routeID, DateTime departureDate, DateTime arrivalDate, string status, string tripType, int maxPassengers)
        {
            TripID = tripID;
            VehicleID = vehicleID;
            RouteID = routeID;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Status = status;
            TripType = tripType;
            MaxPassengers = maxPassengers;
        }
    }
}


