CREATE TABLE [config].[BulletinType] (
    [BulletinTypeId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BulletinTypeUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Name]            NVARCHAR (200)   NOT NULL,
    [Description]     NVARCHAR (MAX)   NOT NULL,
    [Class]           NVARCHAR (MAX)   NOT NULL,
    [Assembly]        NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_BulletinType] PRIMARY KEY CLUSTERED ([BulletinTypeId] ASC)
);

