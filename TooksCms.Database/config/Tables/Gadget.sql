CREATE TABLE [config].[Gadget] (
    [GadgetId]      INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GadgetUid]     UNIQUEIDENTIFIER CONSTRAINT [DF_Gadget_GadgetUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Name]          NVARCHAR (128)   NOT NULL,
    [Description]   NVARCHAR (512)   NOT NULL,
    [View]          NVARCHAR (MAX)   NOT NULL,
    [DefaultColumn] INT              NOT NULL,
    [SiteId]        INT              NOT NULL,
    CONSTRAINT [PK_Gadget] PRIMARY KEY CLUSTERED ([GadgetId] ASC),
    CONSTRAINT [FK_Gadget_Site] FOREIGN KEY ([SiteId]) REFERENCES [security].[Site] ([SiteId])
);

