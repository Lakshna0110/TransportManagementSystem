using NUnit.Framework;
using dao;
using model;
using myexceptions;
using System;
using System.Data.SqlClient; 

namespace TestProject1
{
    public class TransportTests
    {
        private TransportManagementServiceImpl _service;

        [SetUp]
        public void Setup()
        {
            _service = new TransportManagementServiceImpl();
        }

       
        [Test]
        public void AllocateDriver()
        {
            int tripId = 1;     
            int driverId = 1;   

            bool result = _service.AllocateDriver(tripId, driverId);
            Assert.IsTrue(result);
        }


        [Test]
      
        public void AddDriver()
        {
            var driver = new Driver(0, "Test Driver", "DL133", "9876543210", "Available");
            bool result = _service.AddDriver(driver);
            Assert.IsTrue(result);
        }


        [Test]
        public void AddVehicle()
        {
            var vehicle = new Vehicle(0, "TestBus", 25, "Bus", "Available");
            bool result = _service.AddVehicle(vehicle);
            Assert.IsTrue(result);
        }



        [Test]
        public void BookTrip()
        {
            var booking = new Booking(0, 1, 1, DateTime.Now, "Confirmed");
            bool result = _service.BookTrip(booking);
            Assert.IsTrue(result);
        }
        [Test]
        public void DeleteVehicle()
        {
            int nonExistentVehicleId = -999; 

            Assert.Throws<VehicleNotFoundException>(() =>
            {
                _service.DeleteVehicle(nonExistentVehicleId);
            });
        }

    }
}


       