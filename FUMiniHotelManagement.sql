Create database FUMiniHotelManagement
use FUMiniHotelManagement
-- Create Table RoomType
CREATE TABLE RoomType (
    RoomTypeID INT  PRIMARY KEY,
    RoomTypeName NVARCHAR(50) NOT NULL,
    TypeDescription NVARCHAR(250),
    TypeNote NVARCHAR(250)
);

-- Create Table RoomInformation
CREATE TABLE RoomInformation (
    RoomID INT  PRIMARY KEY,
    RoomNumber NVARCHAR(50) NOT NULL,
    RoomDescription NVARCHAR(220),
    RoomMaxCapacity INT NOT NULL,
    RoomStatus INT CHECK (RoomStatus IN (1, 2)) NOT NULL, -- 1 Active, 2 Deleted
    RoomPricePerDate DECIMAL(18, 2) NOT NULL,
    RoomTypeID INT NOT NULL,
    CONSTRAINT FK_RoomType FOREIGN KEY (RoomTypeID) REFERENCES RoomType(RoomTypeID)
);


-- Create Table BookingDetail
CREATE TABLE BookingDetail (
    BookingReservationID INT NOT NULL,
    RoomID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    ActualPrice DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_BookingReservation FOREIGN KEY (BookingReservationID) REFERENCES BookingReservation(BookingReservationID),
    CONSTRAINT FK_RoomInformation FOREIGN KEY (RoomID) REFERENCES RoomInformation(RoomID),
    PRIMARY KEY (BookingReservationID, RoomID)
);

-- Create Table BookingReservation
CREATE TABLE BookingReservation (
    BookingReservationID INT  PRIMARY KEY,
    BookingDate DATE NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    CustomerID INT NOT NULL,
    BookingStatus NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- Create Table Customer
CREATE TABLE Customer (
    CustomerID INT  PRIMARY KEY,
    CustomerFullName NVARCHAR(50) NOT NULL,
    Telephone NVARCHAR(12) NOT NULL,
    EmailAddress NVARCHAR(50) NOT NULL,
    CustomerBirthday DATE NOT NULL,
    CustomerStatus INT CHECK (CustomerStatus IN (1, 2)) NOT NULL, -- 1 Active, 2 Deleted
    Password NVARCHAR(50) NOT NULL
);
-- Insert values into RoomType table
INSERT INTO RoomType (RoomTypeID,RoomTypeName, TypeDescription, TypeNote) VALUES 
(1,N'Standard', N'Phòng tiêu chuẩn với tiện nghi cơ bản', N'Không có ghi chú thêm'),
(2,N'Deluxe', N'Phòng cao cấp với không gian rộng rãi và tiện nghi cao cấp', N'Bao gồm bữa sáng miễn phí'),
(3,N'Suite', N'Phòng Suite với khu vực sinh hoạt riêng và tiện nghi cao cấp', N'Bao gồm quyền truy cập vào phòng chờ cao cấp');

-- Insert values into RoomInformation table
INSERT INTO RoomInformation (RoomID ,RoomNumber, RoomDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDate) VALUES 
(1,N'101', N'Phòng tiêu chuẩn với giường đôi', 2, 1, 1, 100.00),
(2,N'202', N'Phòng cao cấp với giường king-size và ban công', 3, 2, 1, 200.00),
(3,N'303', N'Phòng Suite với khu vực sinh hoạt riêng và view biển', 4, 3, 1, 300.00);

-- Insert values into Customer table
INSERT INTO Customer (CustomerID,CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) VALUES 
(1,N'Nguyễn Văn A', N'0123456789', N'nguyenvana@example.com', '1980-05-15', 1, N'password123'),
(2,N'Trần Thị B', N'0987654321', N'tranthib@example.com', '1990-08-25', 1, N'password456'),
(3,N'Lê Văn C', N'0912345678', N'levanc@example.com', '1985-12-10', 1, N'password789');

INSERT INTO BookingReservation (BookingReservationID,BookingDate, TotalPrice, CustomerID, BookingStatus) VALUES 
(1,'2024-07-01', 300.00, 1, 1),
(2,'2024-07-05', 400.00, 2, 1),
(3,'2024-07-10', 500.00, 3, 1);
INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) VALUES 
(1, 1, '2024-07-01', '2024-07-03', 200.00),
(2, 2, '2024-07-05', '2024-07-07', 300.00),
(3, 3, '2024-07-10', '2024-07-12', 400.00);


