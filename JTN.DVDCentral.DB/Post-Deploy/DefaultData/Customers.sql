BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, Address, City, State, Zip, Phone, UserId)
	VALUES
	(1, 'Eddie', 'Edwards', '123 Main St.', 'Oshkosh', 'WI', '54904', '123-456-7890', 1),
	(2, 'Jack', 'Evans', '456 Orange Ave.', 'Phoenix', 'AZ', '85001', '987-654-3210', 2),
	(3, 'Adam', 'Cole', '789 Apple Blvd.', 'Green Bay', 'WI', '54229', '456-123-0555', 4)
END
