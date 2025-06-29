using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myexceptions
{
   
    
        public class VehicleNotFoundException : Exception
        {
            public VehicleNotFoundException(string message) : base(message) { }
        }

        public class BookingNotFoundException : Exception
        {
            public BookingNotFoundException(string message) : base(message) { }
        }
    public class InvalidCapacityException : Exception
    {
        public InvalidCapacityException(string message) : base(message) { }
    }

    public class InvalidVehicleTypeException : Exception
    {
        public InvalidVehicleTypeException(string message) : base(message) { }
    }
}


