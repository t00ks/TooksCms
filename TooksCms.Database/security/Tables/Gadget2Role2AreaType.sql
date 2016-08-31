CREATE TABLE [security].[Gadget2Role2AreaType] (
    [GadgetId]   INT NOT NULL,
    [RoleId]     INT NOT NULL,
    [AreaTypeId] INT NOT NULL,
    CONSTRAINT [PK_Gadget2Role2AreaType] PRIMARY KEY CLUSTERED ([GadgetId] ASC, [RoleId] ASC, [AreaTypeId] ASC),
    CONSTRAINT [FK_Gadget2Role2AreaType_AreaType] FOREIGN KEY ([AreaTypeId]) REFERENCES [security].[AreaType] ([AreaTypeId]),
    CONSTRAINT [FK_Gadget2Role2AreaType_Gadget] FOREIGN KEY ([GadgetId]) REFERENCES [config].[Gadget] ([GadgetId]),
    CONSTRAINT [FK_Gadget2Role2AreaType_Role] FOREIGN KEY ([RoleId]) REFERENCES [security].[Role] ([RoleId])
);

