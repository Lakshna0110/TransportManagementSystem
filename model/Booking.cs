using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int TripID { get; set; }
        public int PassengerID { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public Booking() { }
        public Booking(int bookingID, int tripID, int passengerID, DateTime bookingDate, string status)
        {
            BookingID = bookingID;
            TripID = tripID;
            PassengerID = passengerID;
            BookingDate = bookingDate;
            Status = status;
        }
    }
}
