CREATE TABLE [dbo].[Snapshot]
(
	[SnapshotId] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](150) NOT NULL,
	[Html] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	CONSTRAINT [PK_Snapshot] PRIMARY KEY CLUSTERED 
	(
		[SnapshotId] ASC
	)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]