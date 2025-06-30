using System;
using System.Collections.Generic;
using System.Linq;
using dao;
using model;
using util;
using myexceptions;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            TransportManagementServiceImpl service = new TransportManagementServiceImpl();
            bool running = true;
            while (running)
            {



                Console.WriteLine("\nTransport Management System");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Delete Vehicle");
                Console.WriteLine("3. Add Route");
                Console.WriteLine("4. Add Passenger");
                Console.WriteLine("5. Schedule Trip");
                Console.WriteLine("6. Update Vehicle");
                Console.WriteLine("7. Book Trip");
                Console.WriteLine("8. Add Driver");
                Console.WriteLine("9. Allocate Driver");
                Console.WriteLine("10. Deallocate Driver");
                Console.WriteLine("11. Cancel Trip");
                Console.WriteLine("12. Cancel Booking");
                Console.WriteLine("13. Get Bookings By Passenger");
                Console.WriteLine("14. Get Bookings By Trip");
                Console.WriteLine("15. Get Available Drivers");
                Console.WriteLine("16. Vehicle Search & Filter");
                Console.WriteLine("17. Add Payment");
                Console.WriteLine("18. View All Payments");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Model: ");
                            string model = Console.ReadLine();
                            Console.Write("Enter Capacity: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal cap))
                            {
                                Console.WriteLine("Invalid capacity.");
                                break;
                            }
                            Console.Write("Enter Type: ");
                            string type = Console.ReadLine();
                            Console.Write("Enter Status: ");
                            string status = Console.ReadLine();
                            Vehicle v = new Vehicle(0, model, cap, type, status);
                            bool result = service.AddVehicle(v);
                            Console.WriteLine(result ? "Vehicle Added" : "Failed to add vehicle.");
                            break;
                        case 2:
                            Console.Write("Enter Vehicle ID to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                            {
                                bool delresult = service.DeleteVehicle(deleteId);
                                Console.WriteLine(delresult ? "Vehicle Deleted" : "Failed to delete vehicle.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Vehicle ID.");
                            }
                            break;

                        case 3:
                            Console.Write("Enter Start Destination: ");
                            string start = Console.ReadLine();
                            Console.Write("Enter End Destination: ");
                            string end = Console.ReadLine();
                            Console.Write("Enter Distance (km): ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal dist))
                            {
                                Console.WriteLine("Invalid distance.");
                                break;
                            }
                            Route r = new Route(0, start, end, dist);
                            bool routeResult = service.AddRoute(r);
                            Console.WriteLine(routeResult ? "Route Added" : "Failed to add route.");
                            break;

                        case 4:
                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Gender: ");
                            string gender = Console.ReadLine();
                            Console.Write("Enter Age: ");
                            if (!int.TryParse(Console.ReadLine(), out int age))
                            {
                                Console.WriteLine("Invalid age.");
                                break;
                            }
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone Number: ");
                            string phone = Console.ReadLine();
                            Passenger p = new Passenger(0, name, gender, age, email, phone);
                            bool passResult = service.AddPassenger(p);
                            Console.WriteLine(passResult ? "Passenger Added" : "Failed to add passenger.");
                            break;

                        case 5:
                            Console.Write("Enter Vehicle ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int vehicleId))
                            {
                                Console.WriteLine("Invalid Vehicle ID.");
                                break;
                            }
                            Console.Write("Enter Route ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int routeId))
                            {
                                Console.WriteLine("Invalid Route ID.");
                                break;
                            }
                            Console.Write("Enter Departure Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime departure))
                            {
                                Console.WriteLine("Invalid departure date.");
                                break;
                            }
                            Console.Write("Enter Arrival Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime arrival))
                            {
                                Console.WriteLine("Invalid arrival date.");
                                break;
                            }
                            bool tripResult = service.ScheduleTrip(vehicleId, routeId, departure, arrival);
                            Console.WriteLine(tripResult ? "Trip Scheduled" : "Failed to schedule trip.");
                            break;

                        case 6:
                            Console.Write("Enter Vehicle ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int updateId))
                            {
                                Console.WriteLine("Invalid Vehicle ID.");
                                break;
                            }
                            Console.Write("Enter  Model: ");
                            string newModel = Console.ReadLine();
                            Console.Write("Enter  Capacity: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal newCap))
                            {
                                Console.WriteLine("Invalid capacity.");
                                break;
                            }
                            Console.Write("Enter  Type: ");
                            string newType = Console.ReadLine();
                            Console.Write("Enter  Status: ");
                            string newStatus = Console.ReadLine();
                            Vehicle updatedVehicle = new Vehicle(updateId, newModel, newCap, newType, newStatus);
                            bool updateResult = service.UpdateVehicle(updatedVehicle);
                            Console.WriteLine(updateResult ? "Vehicle Updated" : "Update failed.");
                            break;

                        case 7:
                            Console.Write("Enter Trip ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int tripId))
                            {
                                Console.WriteLine("Invalid Trip ID.");
                                break;
                            }
                            Console.Write("Enter Passenger ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int passengerId))
                            {
                                Console.WriteLine("Invalid Passenger ID.");
                                break;
                            }
                            Console.Write("Enter Booking Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime bookingDate))
                            {
                                Console.WriteLine("Invalid Booking Date.");
                                break;
                            }
                            bool bookingResult = service.BookTrip(tripId, passengerId, bookingDate);
                            Console.WriteLine(bookingResult ? "Booking Confirmed" : "Booking Failed.");

                            break;
                        case 8:
                            Console.Write("Enter Driver Name: ");
                            string driverName = Console.ReadLine();
                            Console.Write("Enter License No: ");
                            string license = Console.ReadLine();
                            Console.Write("Enter Phone No: ");
                            string driverPhone = Console.ReadLine();
                            Console.Write("Enter Status: ");
                            string driverStatus = Console.ReadLine();
                            Driver details = new Driver(0, driverName, license, driverPhone, driverStatus);
                            bool driverAdded = service.AddDriver(details);
                            Console.WriteLine(driverAdded ? "Driver added!" : "Driver not added.");
                            break;
                        case 9:
                            Console.Write("Enter Trip ID: ");
                            int allocTripId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Driver ID: ");
                            int allocDriverId = int.Parse(Console.ReadLine());
                            bool allocate = service.AllocateDriver(allocTripId, allocDriverId);
                            Console.WriteLine(allocate ? "Driver Allocated!" : "Allocation Failed.");
                            break;
                        case 10:
                            Console.Write("Enter Trip ID to deallocate driver: ");
                            int deallocTripId = int.Parse(Console.ReadLine());
                            bool deallocate = service.DeallocateDriver(deallocTripId);
                            Console.WriteLine(deallocate ? "Driver Deallocated!" : "Failed to deallocate driver.");
                            break;
                        case 11:
                            Console.Write("Enter Trip ID to cancel: ");
                            int cancelTripId = int.Parse(Console.ReadLine());
                            bool cancelTrip = service.CancelTrip(cancelTripId);
                            Console.WriteLine(cancelTrip ? "Trip Cancelled!" : "Failed to cancel trip.");
                            break;
                        case 12:
                            Console.Write("Enter Booking ID to cancel: ");
                            int cancelBookingId = int.Parse(Console.ReadLine());
                            bool cancelBooking = service.CancelBooking(cancelBookingId);
                            Console.WriteLine(cancelBooking ? "Booking Cancelled!" : "Failed to cancel booking.");
                            break;
                        case 13:
                            Console.Write("Enter Passenger ID: ");
                            if (int.TryParse(Console.ReadLine(), out int bookPassengerId))
                            {
                                List<Booking> bookingsByPassenger = service.GetBookingsByPassenger(bookPassengerId);
                                foreach (Booking b in bookingsByPassenger)
                                {
                                    Console.WriteLine($"BookingID: {b.BookingID}, TripID: {b.TripID}, Status: {b.Status}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Passenger ID.");
                            }
                            break;
                        case 14:
                            Console.Write("Enter Trip ID: ");
                            if (int.TryParse(Console.ReadLine(), out int bookTripId))
                            {
                                List<Booking> bookingsByTrip = service.GetBookingsByTrip(bookTripId);
                                foreach (Booking b in bookingsByTrip)
                                {
                                    Console.WriteLine($"BookingID: {b.BookingID}, PassengerID: {b.PassengerID}, Status: {b.Status}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Trip ID.");
                            }
                            break;
                        case 15:
                            List<Driver> availableDrivers = service.GetAvailableDrivers();
                            foreach (Driver d in availableDrivers)
                            {
                                Console.WriteLine($"DriverID: {d.DriverID}, Name: {d.DriverName}, Status: {d.Status}");
                            }
                            break;
                        case 16:
                            List<Vehicle> vehicles = service.GetVehicles();
                            Console.WriteLine("1. All Vehicles");
                            Console.WriteLine("2. Active Vehicles");
                            Console.WriteLine("3. Capacity > 40");
                            Console.WriteLine("4. Search Vehicle by ID");
                            Console.Write("Enter filter choice: ");
                            string filterInput = Console.ReadLine();
                            List<Vehicle> filteredVehicles = vehicles;
                            switch (filterInput)
                            {
                                case "1":
                                    break;
                                case "2":
                                    filteredVehicles = vehicles.Where(veh => veh.Status.ToLower() == "available").ToList();
                                    break;
                                case "3":
                                    filteredVehicles = vehicles.Where(veh => veh.Capacity > 40).ToList();
                                    break;
                                case "4":
                                    Console.Write("Enter Vehicle ID: ");
                                    if (int.TryParse(Console.ReadLine(), out int id))
                                    {
                                        Vehicle vehicle = vehicles.FirstOrDefault(veh => veh.VehicleID == id);
                                        if (vehicle != null)
                                        {
                                            Console.WriteLine($"Vehicle Found: ID: {vehicle.VehicleID}, Model: {vehicle.Model}, Type: {vehicle.Type}, Capacity: {vehicle.Capacity}, Status: {vehicle.Status}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("vehicle not found.");
                                        }
                                    }
                                    return;
                                default:
                                    Console.WriteLine("invalid filter choice.");
                                    break;
                            }
                            foreach (Vehicle vItem in filteredVehicles)
                            {
                                Console.WriteLine($"ID: {vItem.VehicleID} | Model: {vItem.Model} | Type: {vItem.Type} | Capacity: {vItem.Capacity} | Status: {vItem.Status}");
                            }
                            break;
                        case 17:
                            Console.Write("Enter Booking ID: ");
                            int payBookingId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Amount: ");
                            decimal payAmount = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter Payment Method: ");
                            string payMethod = Console.ReadLine();
                            Payment pay = new Payment
                            {
                                BookingID = payBookingId,
                                Amount = payAmount,
                                PaymentDate = DateTime.Now,
                                PaymentMethod = payMethod
                            };
                            string payResult = service.AddPayment(pay);
                            Console.WriteLine(payResult);
                            break;

                        case 18:
                            List<Payment> paymentList = service.GetAllPayments();
                            if (paymentList.Count > 0)
                            {
                                foreach (Payment payment in paymentList)
                                {
                                    Console.WriteLine($"PaymentID: {payment.PaymentID}, BookingID: {payment.BookingID}, Amount: {payment.Amount}, Date: {payment.PaymentDate}, Method: {payment.PaymentMethod}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No payments found.");
                            }
                            break;
                        case 0:
                            Console.WriteLine("Exiting...");
                            running = false;
                            break;


                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (VehicleNotFoundException ex)
                {
                    Console.WriteLine("Vehicle Error: " + ex.Message);
                }
                catch (InvalidCapacityException ex)
                {
                    Console.WriteLine("Capacity Error: " + ex.Message);
                }
                catch (InvalidVehicleTypeException ex)
                {
                    Console.WriteLine("Type Error: " + ex.Message);

                }
                catch (BookingNotFoundException ex)
                {
                    Console.WriteLine("Booking Error: " + ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }


}
