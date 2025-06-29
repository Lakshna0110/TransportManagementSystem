using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace model
    {
        public class Payment
        {
            public int PaymentID { get; set; }           
            public int BookingID { get; set; }          
            public decimal Amount { get; set; }
            public string PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
        }
    }

