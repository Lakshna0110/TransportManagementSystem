using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{ 
        public class Driver
        {
            public int DriverID { get; set; }
            public string DriverName { get; set; }
            public string LicenseNumber { get; set; }
            public string PhoneNumber { get; set; }
            public string Status { get; set; }
        public Driver() { }

        public Driver(int driverID, string driverName, string licenseNumber, string phoneNumber, string status)
        {
            DriverID = driverID;
            DriverName = driverName;
            LicenseNumber = licenseNumber;
            PhoneNumber = phoneNumber;
            Status = status;
        }
    }
}
