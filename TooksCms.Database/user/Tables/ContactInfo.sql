CREATE TABLE [user].[ContactInfo] (
    [ContactInfoId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContactInfoUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [UserId]         INT              NULL,
    [Title]          NVARCHAR (10)    NOT NULL,
    [FirstName]      NVARCHAR (128)   NOT NULL,
    [LastName]       NVARCHAR (128)   NOT NULL,
    [CountryId]      INT              NOT NULL,
    [City]           NVARCHAR (128)   NOT NULL,
    [DateCreated]    DATETIME         CONSTRAINT [DF_ContactInfo_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ContactInfo] PRIMARY KEY CLUSTERED ([ContactInfoId] ASC),
    CONSTRAINT [FK_ContactInfo_User] FOREIGN KEY ([UserId]) REFERENCES [user].[User] ([UserId])
);

