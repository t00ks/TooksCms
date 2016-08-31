CREATE PROCEDURE [dbo].[ArticleSearch]
(
	@searchText NVARCHAR(MAX)
)
AS

SET NOCOUNT ON

SELECT DISTINCT
	a.[ArticleId]
   ,a.[ArticleUid]
   ,a.[ArticleTypeId]
   ,a.[SiteId]
   ,a.[Status]
   ,a.[CategoryId]
   ,a.[Date]
FROM 
	[dbo].[Article] a WITH (NOLOCK)
	INNER JOIN [dbo].ArticleContent (NOLOCK) ac ON a.ArticleId = ac.ArticleId
WHERE
	ac.Content.exist('(//*/text())[contains(fn:lower-case(.), sql:variable("@searchText"))]') = 1

UNION

SELECT DISTINCT
	a.[ArticleId]
   ,a.[ArticleUid]
   ,a.[ArticleTypeId]
   ,a.[SiteId]
   ,a.[Status]
   ,a.[CategoryId]
   ,a.[Date]
FROM 
	[dbo].[Article] a WITH (NOLOCK)
	INNER JOIN [dbo].Article2Tag (NOLOCK) a2t ON a2t.ArticleId = a.ArticleId
	INNER JOIN [lookup].Tag (NOLOCK) t ON a2t.TagId = t.TagId
WHERE
	t.Name = @searchText
 OR REPLACE(t.Name, ' ', '') = @searchText
