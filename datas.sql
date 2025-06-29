CREATE TABLE Vehicles (
    VehicleID INT IDENTITY(1,1) PRIMARY KEY,
    Model VARCHAR(255),
    Capacity DECIMAL(10, 2),
    Type VARCHAR(50),         
    Status VARCHAR(50)        
);

INSERT INTO Vehicles (Model, Capacity, Type, Status) VALUES
('Tata Ace', 850.00, 'Truck', 'Available'),
('Volvo 9600', 50.00, 'Bus', 'On Trip'),
('Eicher 2075', 1000.00, 'Truck', 'Maintenance'),
('Force Traveller', 15.00, 'Van', 'Available');

CREATE TABLE Routes (
    RouteID INT IDENTITY(1,1) PRIMARY KEY,
    StartDestination VARCHAR(255),
    EndDestination VARCHAR(255),
    Distance DECIMAL(10, 2)
);

INSERT INTO Routes (StartDestination, EndDestination, Distance) VALUES
('Chennai', 'Coimbatore', 510.00),
('Madurai', 'Trichy', 138.00),
('Bangalore', 'Hyderabad', 570.00),
('Pune', 'Mumbai', 150.00);

CREATE TABLE Trips (
    TripID INT IDENTITY(1,1) PRIMARY KEY,
    VehicleID INT FOREIGN KEY REFERENCES Vehicles(VehicleID),
    RouteID INT FOREIGN KEY REFERENCES Routes(RouteID),
    DepartureDate DATETIME,
    ArrivalDate DATETIME,
    Status VARCHAR(50),
    TripType VARCHAR(50) DEFAULT 'Freight',
    MaxPassengers INT
);

INSERT INTO Trips (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, TripType, MaxPassengers) VALUES
(1, 1, '2025-06-20 08:00:00', '2025-06-20 15:00:00', 'Scheduled', 'Freight', 0),
(2, 2, '2025-06-22 09:00:00', '2025-06-22 12:30:00', 'In Progress', 'Passenger', 45),
(4, 4, '2025-06-23 06:30:00', '2025-06-23 08:30:00', 'Completed', 'Passenger', 12);

CREATE TABLE Passengers (
    PassengerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(255),
    Gender VARCHAR(255),
    Age INT,
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(50)
);
INSERT INTO Passengers (FirstName, Gender, Age, Email, PhoneNumber) VALUES
('Lakshna', 'Female', 21, 'lakshna@email.com', '9876543210'),
('Ajay', 'Male', 29, 'ajay@email.com', '9784561230'),
('Priya', 'Female', 34, 'priya@email.com', '9871111111');

CREATE TABLE Bookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES Trips(TripID),
    PassengerID INT FOREIGN KEY REFERENCES Passengers(PassengerID),
    BookingDate DATETIME,
    Status VARCHAR(50)
);

INSERT INTO Bookings (TripID, PassengerID, BookingDate, Status) VALUES
(2, 1, '2025-06-16 10:00:00', 'Confirmed'),
(2, 2, '2025-06-16 10:05:00', 'Confirmed'),
(3, 3, '2025-06-15 09:30:00', 'Completed');









