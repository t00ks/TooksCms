CREATE TABLE [user].[PhoneNumber] (
    [PhoneNumberId]  INT              IDENTITY (1, 1) NOT NULL,
    [PhoneNumberUid] UNIQUEIDENTIFIER CONSTRAINT [DF_PhoneNumber_PhoneNumberUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ContactInfoId]  INT              NOT NULL,
    [Number]         NVARCHAR (100)   NOT NULL,
    [Type]           INT              NOT NULL,
    CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED ([PhoneNumberId] ASC),
    CONSTRAINT [FK_PhoneNumber_ContactInfo] FOREIGN KEY ([ContactInfoId]) REFERENCES [user].[ContactInfo] ([ContactInfoId])
);

