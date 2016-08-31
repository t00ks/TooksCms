CREATE PROCEDURE [dbo].[SnapshotLiteSelect] AS
BEGIN
	SELECT 
		 [SnapshotId]
		,[Url]
		,[Date]
	FROM
		[Snapshot]
END