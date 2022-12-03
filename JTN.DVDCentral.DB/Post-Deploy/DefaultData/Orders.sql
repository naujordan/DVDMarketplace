BEGIN
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, UserId, ShipDate)
	VALUES
	(1, 1, '2022-09-18', 1, '2022-09-22'),
	(2, 1,'2018-10-31', 1, '2018-11-01'),
	(3, 2, '2020-10-10', 2, '2020-10-12')
END
