CREATE TABLE [lookup].[Country] (
    [CountryId]  INT              IDENTITY (1, 1) NOT NULL,
    [CountryUid] UNIQUEIDENTIFIER CONSTRAINT [DF_Country_CountryUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Name]       NVARCHAR (50)    NOT NULL,
    [ISO3166]    NVARCHAR (2)     NOT NULL,
    [ImageName]  NVARCHAR (60)    NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

