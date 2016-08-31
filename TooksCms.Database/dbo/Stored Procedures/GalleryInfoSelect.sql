CREATE PROCEDURE [dbo].[GalleryInfoSelect]
(
	@GalleryId INT = NULL
)
AS

SET NOCOUNT ON

SELECT
	g.[GalleryId],
	g.[GalleryUid],
	g.[Title],
	g.[CategoryId],
	c.[CategoryName],
	g.[CreatedDate],
	(SELECT TOP 1 Thumbnail FROM [dbo].[GalleryImage] WHERE GalleryId = g.GalleryId ORDER BY NEWID()) AS ImageThumbnail
FROM 
	dbo.Gallery g
	INNER JOIN lookup.Category c on g.CategoryId = c.CategoryId
WHERE 
	(@GalleryId IS NULL OR g.[GalleryId] = @GalleryId)
