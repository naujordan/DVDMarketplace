BEGIN
	INSERT INTO tblOrderItem (Id, OrderId, MovieId, Quantity, Cost)
	VALUES
	(1, 1, 1, 1, 9.99),
	(2, 2, 2, 1, 14.99),
	(3, 3, 1, 2, 19.98)
END
