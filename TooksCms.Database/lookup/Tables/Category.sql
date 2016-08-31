CREATE TABLE [lookup].[Category] (
    [CategoryId]          INT              IDENTITY (1, 1) NOT NULL,
    [CategoryUid]         UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [CategoryName]        NVARCHAR (150)   NOT NULL,
    [CategoryDescription] NVARCHAR (MAX)   NOT NULL,
    [ParentCategoryId]    INT              NULL,
	[ImageName]			  NVARCHAR (150)   NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

