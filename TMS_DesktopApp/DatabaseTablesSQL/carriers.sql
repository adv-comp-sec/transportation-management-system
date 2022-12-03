# CREATE DATABASE TMS_DB;
# USE TMS_DB;
# SHOW DATABASES;
# SELECT DATABASE();
DROP TABLE IF EXISTS `carriers`;
CREATE TABLE carriers(
  cName      VARCHAR(17),
  dCity      VARCHAR(10) NOT NULL,
  FTLA       INT  NOT NULL,
  LTLA       INT  NOT NULL,
  FTLRate    DEC(6,4) NOT NULL,
  LTLRate    DEC(6,4) NOT NULL,
  reefCharge DEC(6,4) NOT NULL,
  primary key(cName,dCity)
);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Planet Express','Windsor',50,640,5.21,0.3621,0.08);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Planet Express','Hamilton',50,640,5.21,0.3621,0.08);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Planet Express','Oshawa',50,640,5.21,0.3621,0.08);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Planet Express','Belleville',50,640,5.21,0.3621,0.08);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Planet Express','Ottawa',50,640,5.21,0.3621,0.08);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Schooner''s','London',18,98,5.05,0.3434,0.07);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Schooner''s','Toronto',18,98,5.05,0.3434,0.07);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Schooner''s','Kingston',18,98,5.05,0.3434,0.07);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Tillman Transport','Windsor',24,35,5.11,0.3012,0.09);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Tillman Transport','London',18,45,5.11,0.3012,0.09);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('Tillman Transport','Hamilton',18,45,5.11,0.3012,0.09);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('We Haul','Ottawa',11,0,5.2,0,0.065);
INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) VALUES ('We Haul','Toronto',11,0,5.2,0,0.065);

SELECT * FROM carriers;