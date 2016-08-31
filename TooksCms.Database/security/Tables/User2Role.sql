CREATE TABLE [security].[User2Role] (
    [RoleId] INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [FK_User2Role_Role] FOREIGN KEY ([RoleId]) REFERENCES [security].[Role] ([RoleId]),
    CONSTRAINT [FK_User2Role_User] FOREIGN KEY ([UserId]) REFERENCES [user].[User] ([UserId]),
    CONSTRAINT [PK_User2Role] UNIQUE NONCLUSTERED ([RoleId] ASC, [UserId] ASC)
);

