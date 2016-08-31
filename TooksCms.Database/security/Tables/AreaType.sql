CREATE TABLE [security].[AreaType] (
    [AreaTypeId]  INT              NOT NULL,
    [AreaTypeUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [AreaType]    NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_AreaType] PRIMARY KEY CLUSTERED ([AreaTypeId] ASC)
);

