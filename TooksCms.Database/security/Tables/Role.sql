CREATE TABLE [security].[Role] (
    [RoleId]      INT              IDENTITY (1, 1) NOT NULL,
    [RoleUid]     UNIQUEIDENTIFIER CONSTRAINT [DF_Role_RoleUid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [RoleName]    NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

