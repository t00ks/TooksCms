CREATE PROCEDURE [dbo].[ArticleInfoSelect]
(
	@ArticleId INT = NULL
)
AS

SET NOCOUNT ON

SELECT
	a.[ArticleId],
	a.[ArticleUid],
	a.[Status],
	a.[CategoryId],
	c.[CategoryName],
	c.[ImageName] AS [CategoryImage],
	a.[Date],
	at.[Name] As [TypeName],
	at.[ArticleTypeId],
	ac.[Version],
	con.col.value('(//*/Title/text())[1]','NVARCHAR(MAX)') AS Title,
	CAST(CASE WHEN (SELECT COUNT(*) FROM [dbo].[ArticleImage] WHERE ArticleId = a.ArticleId) > 0 THEN 1 ELSE 0 END AS BIT) AS HasImages,
	(SELECT TOP 1 Thumbnail FROM [dbo].[ArticleImage] WHERE ArticleId = a.ArticleId ORDER BY NEWID()) AS ImageThumbnail
FROM 
	[dbo].[Article] a
	INNER JOIN config.ArticleType at on at.ArticleTypeId = a.ArticleTypeId
	INNER JOIN lookup.Category c on a.CategoryId = c.CategoryId
	INNER JOIN
		(
			SELECT ArticleId, MAX([Version]) AS [Version] FROM dbo.ArticleContent GROUP BY ArticleId
		) g on a.ArticleId = g.ArticleId
	INNER JOIN dbo.ArticleContent ac on ac.ArticleId = g.ArticleId AND ac.[Version] = g.[Version]
	CROSS APPLY ac.Content.nodes('.') as con(col)
WHERE 
	(@ArticleId IS NULL OR a.ArticleId = @ArticleId)
