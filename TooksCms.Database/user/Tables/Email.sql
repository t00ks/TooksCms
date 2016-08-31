CREATE TABLE [user].[Email] (
    [EmailId]       INT              IDENTITY (1, 1) NOT NULL,
    [EmailUid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Email_EmailUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ContactInfoId] INT              NOT NULL,
    [Address]       NVARCHAR (256)   NOT NULL,
    [IsPrimary]     BIT              NOT NULL,
    CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_Email_ContactInfo] FOREIGN KEY ([ContactInfoId]) REFERENCES [user].[ContactInfo] ([ContactInfoId])
);

