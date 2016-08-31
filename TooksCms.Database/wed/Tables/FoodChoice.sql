CREATE TABLE [wed].[FoodChoice]
(
	[FoodChoiceId]  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL PRIMARY KEY,
	[GuestId] INT NOT NULL,
	[Starter] NVARCHAR(15) NULL,
	[Main] NVARCHAR(15) NULL,
	[Dessert] NVARCHAR(15) NULL,
	[IsVeggie] BIT NULL,
    CONSTRAINT [FK_Guest_FoodChoice] FOREIGN KEY ([GuestId]) REFERENCES [wed].[Guest] ([GuestId])
)
