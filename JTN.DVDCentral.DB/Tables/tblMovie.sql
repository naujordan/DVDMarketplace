CREATE TABLE [dbo].[tblMovie]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Title] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[Cost] FLOAT NOT NULL,
	[RatingId] INT NOT NULL,
	[FormatId] INT NOT NULL,
	[DirectorId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[ImagePath] VARCHAR(255) NOT NULL
)
