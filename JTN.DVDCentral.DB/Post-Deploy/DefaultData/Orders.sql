BEGIN
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, UserId, ShipDate, SubTotal, Tax, Total)
	VALUES
	(1, 1, '2022-09-18', 1, '2022-09-22', 9.99, 0.52, 10.51),
	(2, 1,'2018-10-31', 1, '2018-11-01', 9.99, 0.52, 10.51),
	(3, 2, '2020-10-10', 2, '2020-10-12', 9.99, 0.52, 10.51)
END
