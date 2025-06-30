using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using model;
using myexceptions;
using util;
using System.Data.Entity;


namespace dao
{
    public class TransportManagementServiceImpl : ITransportManagementService

    {
        public bool AddVehicle(Vehicle vehicle)
        {
            if (vehicle.Capacity <= 0)
                throw new InvalidCapacityException("Capacity must be greater than 0.");
            if (string.IsNullOrWhiteSpace(vehicle.Type))
                throw new InvalidVehicleTypeException("Vehicle type is required.");
            using (SqlConnection con = DBConnUtil.GetConnection())
            { string query = "Insert into Vehicles (Model, Capacity, Type, Status) VALUES (@model, @capacity, @type, @status)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                 cmd.Parameters.AddWithValue("@model", vehicle.Model);
                 cmd.Parameters.AddWithValue("@capacity", vehicle.Capacity);
                 cmd.Parameters.AddWithValue("@type", vehicle.Type);
                 cmd.Parameters.AddWithValue("@status", vehicle.Status);
                    try
                    {
                      con.Open();
                      int rows = cmd.ExecuteNonQuery();
                      return rows > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public bool DeleteVehicle(int vehicleId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "Delete From Vehicles Where vehicleID = @vehicleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                { cmd.Parameters.AddWithValue("@vehicleId", vehicleId);

                  try
                    {
                      con.Open();
                     int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            throw new VehicleNotFoundException($"Vehicle ID {vehicleId} not found.");
                        }
                        return true;
                    }
                    catch (SqlException ex)
                    {
                     Console.WriteLine(" Error: " + ex.Message);
                     return false;
                    }
                }
            }

        }
        public bool AddRoute(Route route)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "insert into Routes (StartDestination, EndDestination, Distance) VALUES (@start, @end, @distance)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@start", route.StartDestination);
                    cmd.Parameters.AddWithValue("@end", route.EndDestination);
                    cmd.Parameters.AddWithValue("@distance", route.Distance);
                    try
                    {
                     con.Open();
                     int rows = cmd.ExecuteNonQuery();
                     return rows > 0;
                    }
                    catch (Exception ex)
                    {
                      Console.WriteLine(" Error: " + ex.Message);
                      return false;
                    }
                }
            }
        }
        public bool AddPassenger(Passenger passenger)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
              string query = "insert into Passengers (FirstName, Gender, Age, Email, PhoneNumber) VALUES (@first, @gender, @age, @email, @phone)";
              using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@first", passenger.FirstName);
                    cmd.Parameters.AddWithValue("@gender", passenger.Gender);
                    cmd.Parameters.AddWithValue("@age", passenger.Age);
                    cmd.Parameters.AddWithValue("@email", passenger.Email);
                    cmd.Parameters.AddWithValue("@phone", passenger.PhoneNumber);
                    try
                    {
                      con.Open();
                      int rows = cmd.ExecuteNonQuery();
                      return rows > 0;
                    }
                    catch (SqlException ex)
                    {
                     Console.WriteLine("SQL Error: " + ex.Message);
                     return false;
                    }
                }

            }
        }
        public bool ScheduleTrip(Trip trip)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"insert into Trips 
            (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, TripType, MaxPassengers) VALUES (@vehicleId, @routeId, @departure, @arrival, @status, @type, @max)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@vehicleId", trip.VehicleID);
                    cmd.Parameters.AddWithValue("@routeId", trip.RouteID);
                    cmd.Parameters.AddWithValue("@departure", trip.DepartureDate);
                    cmd.Parameters.AddWithValue("@arrival", trip.ArrivalDate);
                    cmd.Parameters.AddWithValue("@status", trip.Status);
                    cmd.Parameters.AddWithValue("@type", trip.TripType);
                    cmd.Parameters.AddWithValue("@max", trip.MaxPassengers);
                    try
                    {
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }

        }
        public bool ScheduleTrip(int vehicleId, int routeId, DateTime departure, DateTime arrival)
        {
            Trip trip = new Trip(0, vehicleId, routeId, departure, arrival, "Scheduled", "Passenger", 40);
            return ScheduleTrip(trip);
        }
        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "update Vehicles  SET Model = @model, Capacity = @capacity, Type = @type, Status = @status  WHERE VehicleID = @vehicleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@vehicleId", vehicle.VehicleID);
                    cmd.Parameters.AddWithValue("@model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@capacity", vehicle.Capacity);
                    cmd.Parameters.AddWithValue("@type", vehicle.Type);
                    cmd.Parameters.AddWithValue("@status", vehicle.Status);

                    try
                    {
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            throw new VehicleNotFoundException($"Vehicle ID {vehicle.VehicleID} not found.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: " + ex.Message);
                        return false;
                    }
                }


            }
        }
        public bool BookTrip(Booking booking)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {

                string statusQuery = "SELECT Status FROM Trips WHERE TripID = @tripId";
                using (SqlCommand statusCmd = new SqlCommand(statusQuery, con))
                {
                    statusCmd.Parameters.AddWithValue("@tripId", booking.TripID);
                    con.Open();
                    object result = statusCmd.ExecuteScalar();
                    if (result == null)
                    {
                        Console.WriteLine("Trip does not exist.");
                        return false;
                    }
                    string tripStatus = result.ToString();
                    if (tripStatus == "Cancelled")
                    {
                        Console.WriteLine("Cannot book. Trip is cancelled.");
                        return false;
                    }
                }

                string query = "insert into Bookings (TripID, PassengerID, BookingDate, Status) VALUES (@tripId, @passengerId, @bookingDate, @status)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tripId", booking.TripID);
                    cmd.Parameters.AddWithValue("@passengerId", booking.PassengerID);
                    cmd.Parameters.AddWithValue("@bookingDate", booking.BookingDate);
                    cmd.Parameters.AddWithValue("@status", booking.Status);
                    try
                    {
                       
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool BookTrip(int tripId, int passengerId, DateTime bookingDate)
        {
            Booking booking = new Booking(0, tripId, passengerId, bookingDate, "Confirmed");
            return BookTrip(booking);
        }

        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = DBConnUtil.GetConnection())
                {
                    string query = "SELECT VehicleID, Model, Capacity, Type, Status FROM Vehicles";
                    da = new SqlDataAdapter(query, con);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        Vehicle v = new Vehicle()
                        {
                            VehicleID = int.Parse(row["VehicleID"].ToString()),
                            Model = row["Model"].ToString(),
                            Capacity = decimal.TryParse(row["Capacity"].ToString(), out decimal cap) ? cap : 0,
                            Type = row["Type"].ToString(),
                            Status = row["Status"].ToString()
                        };
                        vehicleList.Add(v);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            return vehicleList;
        }
        public bool AddDriver(Driver driver)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"insert into drivers (DriverName, LicenseNumber, PhoneNumber, Status)
                         VALUES (@name, @license, @phone, @status)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", driver.DriverName);
                    cmd.Parameters.AddWithValue("@license", driver.LicenseNumber);
                    cmd.Parameters.AddWithValue("@phone", driver.PhoneNumber);
                    cmd.Parameters.AddWithValue("@status", driver.Status);

                    try
                    {
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public bool AllocateDriver(int tripId, int driverId)
        {
          using (SqlConnection con = DBConnUtil.GetConnection())
            {
             string query = @"update Trips SET DriverID = @driverId WHERE TripID = @tripId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tripId", tripId);
                    cmd.Parameters.AddWithValue("@driverId", driverId);

                    try
                    {
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public bool DeallocateDriver(int tripId)
        {
          using (SqlConnection con = DBConnUtil.GetConnection())
            {
             string query = "UPDATE Trips SET DriverID = NULL WHERE TripID = @tripId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                { cmd.Parameters.AddWithValue("@tripId", tripId);
                    try
                    {
                     con.Open();
                     int rows = cmd.ExecuteNonQuery();
                     return rows > 0;
                    }
                    catch (Exception ex)
                    {
                      Console.WriteLine(" Error: " + ex.Message);
                      return false;
                    }
                }
            }
        }
        public bool CancelTrip(int tripId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "Update Trips SET Status = 'Cancelled' WHERE TripID = @tripId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tripId", tripId);

                    try
                    {
                      con.Open();
                     int rows = cmd.ExecuteNonQuery();
                     return rows > 0;
                    }
                    catch (Exception ex)
                    {
                      Console.WriteLine(" Error: " + ex.Message);
                      return false;
                    }
                }
            }
        }
        public bool CancelBooking(int bookingId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "Update Bookings SET Status = 'Cancelled' WHERE BookingID = @bookingId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                  cmd.Parameters.AddWithValue("@bookingId", bookingId);

                    try
                    {
                      con.Open();
                      int rows = cmd.ExecuteNonQuery();
                      return rows > 0;
                    }
                    catch (Exception ex)
                    {
                      Console.WriteLine(" Error: " + ex.Message);
                      return false;
                    }
                }
            }
        }
        public List<Booking> GetBookingsByPassenger(int passengerId)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"SELECT B.BookingID, B.TripID, B.PassengerID, B.BookingDate, B.Status, P.FirstName
                         FROM Bookings B
                         INNER JOIN Passengers P ON B.PassengerID = P.PassengerID
                         WHERE B.PassengerID = @passengerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@passengerId", passengerId);
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Booking booking = new Booking();
                            booking.BookingID = Convert.ToInt32(reader["BookingID"]);
                            booking.TripID = Convert.ToInt32(reader["TripID"]);
                            booking.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                            booking.BookingDate = Convert.ToDateTime(reader["BookingDate"]);
                            booking.Status = reader["Status"].ToString();

                            bookings.Add(booking);
                        }

                        if (bookings.Count == 0)
                        {
                            throw new BookingNotFoundException($"No bookings found for Passenger ID: {passengerId}");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                }
            }
            return bookings;
        }
    
        
        public List<Booking> GetBookingsByTrip(int tripId)
        {
         List<Booking> bookings = new List<Booking>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
              string query = "SELECT BookingID, TripID, PassengerID, BookingDate, Status FROM Bookings WHERE TripID = @tripId";
              using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tripId", tripId);
                    try
                    {
                      con.Open();
                      SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Booking booking = new Booking();

                            booking.BookingID = Convert.ToInt32(reader["BookingID"]);
                            booking.TripID = Convert.ToInt32(reader["TripID"]);
                            booking.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                            booking.BookingDate = Convert.ToDateTime(reader["BookingDate"]);
                            booking.Status = reader["Status"].ToString();

                            bookings.Add(booking);
                        }
                    
                    }
                    catch (SqlException ex)
                    {
                     Console.WriteLine("SQL Error: " + ex.Message);
                    }
                }
            }
            return bookings;
        }
        public List<Driver> GetAvailableDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"SELECT DriverID, DriverName, LicenseNumber, PhoneNumber, Status 
                 FROM Drivers 
                 WHERE Status = 'Available' 
                 AND DriverID NOT IN (SELECT DISTINCT DriverID FROM Trips WHERE DriverID IS NOT NULL)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Driver driver = new Driver();

                            driver.DriverID = Convert.ToInt32(reader["DriverID"]);
                            driver.DriverName = reader["DriverName"].ToString();
                            driver.LicenseNumber = reader["LicenseNumber"].ToString();
                            driver.PhoneNumber = reader["PhoneNumber"].ToString();
                            driver.Status = reader["Status"].ToString();

                            drivers.Add(driver);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: " + ex.Message);
                    }
                }
            }
            return drivers;
        }
        public string AddPayment(Payment payment)
        {
            using (TmsDbContext context = new TmsDbContext())
            {
                try
                {
                    context.Payments.Add(payment);
                    context.SaveChanges();
                    return "Payment added successfully.";
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }

        }
        public List<Payment> GetAllPayments()
        {
            using (TmsDbContext context = new TmsDbContext())
            {
                List<Payment> paymentList = context.Payments.ToList();
                return paymentList;
            }
        }


    }

}















