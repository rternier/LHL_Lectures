--insert data into User table
Insert into [User] (Name, password, Salt, EmailAddress, isAdmin) values ('Sarah Connor', HASHBYTES('SHA',CONVERT(VARCHAR(50),12345) + CONVERT(VARCHAR(50),6789) + CONVERT(VARCHAR(50),101112)), rand(200000) *1000000 , 'Shewillbeback@example.com', 0)
Insert into [User] (Name, password, Salt, EmailAddress, isAdmin) values ('Uncle Roger', HASHBYTES('SHA',CONVERT(VARCHAR(50),23623) + CONVERT(VARCHAR(50),3426) + CONVERT(VARCHAR(50),34573457)), rand(700000)*1000000 , 'Hiyaa@example.com', 0)
Insert into [User] (Name, password, Salt, EmailAddress, isAdmin) values ('Ron Rugundy', HASHBYTES('SHA',CONVERT(VARCHAR(50),34578) + CONVERT(VARCHAR(50),344) + CONVERT(VARCHAR(50),3457457)),   rand(800000)*1000000 , 'Flutist@example.com', 0)
Insert into [User] (Name, password, Salt, EmailAddress, isAdmin) values ('Ellen Ripley', HASHBYTES('SHA',CONVERT(VARCHAR(50),12345) + CONVERT(VARCHAR(50),77) + CONVERT(VARCHAR(50),34573457)),  rand(400000)*1000000 , 'AlienMobster@example.com', 0)
Insert into [User] (Name, password, Salt, EmailAddress, isAdmin) values ('John Wick', 12345, rand(500000)*10000000 ,'BabaYaga@example.com', 0)


--insert data into Product Table
INSERT INTO Projects.dbo.Products(Name) values('Jacket')

--Queries that test SQL Injection.
/*

Add the following to the UserName login to drop products
' or 1=1; DROP TABLE Products; --

Add the following to UserName to bypass password check.
' or 1=1 or 'test' = '


*/


SELECT * FROM Projects.dbo.[User] 
-- WHERE EmailAddress = '' or 1=1; DROP TABLE Products; 
--AND Password = '' or 1=1 or 'test' =''

--SELECT * FROM Projects.dbo.[User] WHERE name = @Name
--   ' UNION SELECT * from Projects.dbo.[user]--'
SELECT * FROM Projects.dbo.[User] WHERE name = '' UNION SELECT * from Projects.dbo.[user]--'
