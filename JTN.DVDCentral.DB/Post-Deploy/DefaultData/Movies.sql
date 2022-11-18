	BEGIN
		INSERT INTO tblMovie (Id, Title, Description, Cost, RatingId, FormatId, DirectorId, Quantity, ImagePath)
		VALUES
		(1, 'Pulp Fiction', 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.',
		9.99, 1, 1, 1, 10, 'pulpFiction.jpg'),
		(2, 'The Prestige', 'A mysterious story of two magicians, whose intense rivalry leads them on a life-long battle for supremacy, full of obsession, deceit, and jealousy, with dangerous and deadly consequences.',
		14.99, 2, 2, 2, 1, 'thePrestige.jpg'),
		(3, 'Pineapple Express', 'The plot centers on a process server and his marijuana dealer as they are forced to flee from hitmen and a corrupt police officer after witnessing them commit a murder.',
		2.99, 1, 3, 3, 99, 'pineappleExpress.jpg')
	END
