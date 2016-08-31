CREATE TABLE [user].[Address] (
    [AddressId]     INT              IDENTITY (1, 1) NOT NULL,
    [AddressUid]    UNIQUEIDENTIFIER CONSTRAINT [DF_Address_AddressUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ContactInfoId] INT              NOT NULL,
    [HouseNumber]   INT              NULL,
    [HouseName]     NVARCHAR (200)   NULL,
    [AddressLine1]  NVARCHAR (1000)  NOT NULL,
    [AddressLine2]  NVARCHAR (1000)  NULL,
    [AddressLine3]  NVARCHAR (1000)  NULL,
    [City]          NVARCHAR (150)   NULL,
    [County]        NVARCHAR (100)   NULL,
    [CountryId]     INT              NOT NULL,
    [PostCode]      NVARCHAR (8)     NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_Address_ContactInfo] FOREIGN KEY ([ContactInfoId]) REFERENCES [user].[ContactInfo] ([ContactInfoId]),
    CONSTRAINT [FK_Address_Country] FOREIGN KEY ([CountryId]) REFERENCES [lookup].[Country] ([CountryId])
);

