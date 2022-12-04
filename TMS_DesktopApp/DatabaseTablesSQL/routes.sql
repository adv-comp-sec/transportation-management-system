USE TMS_DB;

DROP TABLE IF EXISTS `routes`;
CREATE TABLE routes(
  routeId 				SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  location  			VARCHAR(20) NOT NULL,
  locationReference 	VARCHAR(20) NOT NULL,
  distance		      	INT  NOT NULL,
  timeInHours			DOUBLE  NOT NULL,
  primary key(routeId)
);

INSERT INTO routes(routeId,location,locationReference,distance,timeInHours) VALUES (5001,'Windsor','London',191,2.5);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('London','Hamilton',128,1.75);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('Hamilton','Toronto',68,1.25);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('Toronto','Oshawa',60,1.3);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('Oshawa','Belleville',134,1.65);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('Belleville','Kingston',82,1.2);
INSERT INTO routes(location,locationReference,distance,timeInHours) VALUES ('Kingston','Ottawa',196,2.5);


SELECT * FROM routes;