# CREATE DATABASE TMS_DB;
USE tms_db;
# SHOW DATABASES;
SELECT DATABASE();
DROP TABLE IF EXISTS tms_db.orders;
CREATE TABLE orders(
  OrderID      SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  CustomerID       SMALLINT UNSIGNED NOT NULL,
  TripID   SMALLINT UNSIGNED,
  RelevantCitiesId SMALLINT UNSIGNED,
  OrderStatus     VARCHAR(50) NOT NULL,
  OrderDate       DATETIME,
  Origin VARCHAR(50) NOT NULL,
  Destination VARCHAR(50) NOT NULL,
  VanType    BIT NOT NULL,
  JobType   BIT NOT NULL,
  Quantity SMALLINT,
  Cost DECIMAL(10,2),
  primary key(OrderID)
);

INSERT INTO orders(CustomerID,TripID ,RelevantCitiesId,OrderStatus,OrderDate,Origin,Destination,VanType,JobType,Quantity) 
VALUES (1,2,1,'Recieved','2022-01-19 03:14:07','Hamilton','Toronto',0,0,0);