CREATE TABLE [config].[StaticRoute] (
    [StaticRoute] NVARCHAR (256) NOT NULL,
    [Area]        NVARCHAR (56)  NOT NULL,
    [Action]      NVARCHAR (56)  NOT NULL,
    [Id]          INT            NOT NULL,
    CONSTRAINT [PK_StaticRoute] PRIMARY KEY CLUSTERED ([StaticRoute] ASC)
);

