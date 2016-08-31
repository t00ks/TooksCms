CREATE TABLE [user].[Guest] (
    [GuestId]    INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GuestUid]   UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Name]       NVARCHAR (256)   NOT NULL,
    [Email]      NVARCHAR (512)   NOT NULL,
    [Website]    NVARCHAR (512)   NULL,
    [IpAddress]  NVARCHAR (15)    NOT NULL,
    [Date]       DATETIME         NOT NULL,
    [IsArchived] BIT              NOT NULL,
    CONSTRAINT [PK_Guest] PRIMARY KEY CLUSTERED ([GuestId] ASC)
);

