using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using model;
using util;

namespace dao
{  
        public interface ITransportManagementService
        {
            bool AddVehicle(Vehicle vehicle);
            bool UpdateVehicle(Vehicle vehicle);
            bool DeleteVehicle(int vehicleId);

            bool AddRoute(Route route);
            bool ScheduleTrip(Trip trip);
            bool ScheduleTrip(int vehicleId, int routeId, DateTime departure, DateTime arrival); 

            bool CancelTrip(int tripId);

            bool AddPassenger(Passenger passenger);
            bool BookTrip(Booking booking); 

            bool BookTrip(int tripId, int passengerId, DateTime bookingDate); bool CancelBooking(int bookingId);
            List<Booking> GetBookingsByPassenger(int passengerId);
            List<Booking> GetBookingsByTrip(int tripId);
            bool AllocateDriver(int tripId, int driverId);
            List<Driver> GetAvailableDrivers();

    }
}
    
    

