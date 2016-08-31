CREATE TABLE [wed].[Guest]
(
	[GuestId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(MAX) NOT NULL, 
    [GuestType] TINYINT NOT NULL, 
    [GuestSide] TINYINT NOT NULL, 
	[InvitationSent] BIT NOT NULL DEFAULT 0,
    [Code] NVARCHAR(30) NOT NULL, 
    [Attending] BIT NULL,
	[GuestGroupId] INT NULL,
	[DietaryRequirements] NVARCHAR(MAX) NULL,
	[RSVPDate] DATETIME NULL,
	[RSVPIpAddress] NVARCHAR(15) NULL
    CONSTRAINT [FK_Guest_GuestGroup] FOREIGN KEY ([GuestGroupId]) REFERENCES [wed].[GuestGroup]([GuestGroupId])
)
