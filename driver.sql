CREATE TABLE Drivers (
    DriverID INT IDENTITY(1,1) PRIMARY KEY,
    DriverName VARCHAR(100) NOT NULL,
    LicenseNumber VARCHAR(50) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15),
    Status VARCHAR(50) 
);
INSERT INTO Drivers (DriverName, LicenseNumber, PhoneNumber, Status)
VALUES 
('Raj Kumar', 'TN22X4567', '9876543210', 'Available'),
('Priya Sharma', 'TN10B2323', '9843217890', 'On Trip'),
('Anil Reddy', 'KA09C7890', '7894561230', 'Available'),
('Sundar Iyer', 'TN11D4561', '9977886655', 'On Leave');
