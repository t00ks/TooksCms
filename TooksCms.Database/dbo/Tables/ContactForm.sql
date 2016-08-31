CREATE TABLE [dbo].[ContactForm] (
    [ContactFormId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContactFormUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [SiteId]         INT              NOT NULL,
    [Title]          NVARCHAR (256)   NOT NULL,
    [Name]           NVARCHAR (256)   NOT NULL,
    [Email]          NVARCHAR (512)   NOT NULL,
    [Content]        NVARCHAR (MAX)   NOT NULL,
    [Date]           DATETIME         NOT NULL,
    [Read]           BIT              CONSTRAINT [DF_ContactForm_Read] DEFAULT ((0)) NOT NULL,
    [Public]         BIT              CONSTRAINT [DF_ContactForm_Public] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ContactForm] PRIMARY KEY CLUSTERED ([ContactFormId] ASC),
    CONSTRAINT [FK_ContactForm_Site] FOREIGN KEY ([SiteId]) REFERENCES [security].[Site] ([SiteId])
);

