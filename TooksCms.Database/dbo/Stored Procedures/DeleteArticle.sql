CREATE PROCEDURE [dbo].[DeleteArticle]
(
	@articleId INT
)
AS

SET NOCOUNT ON;

DELETE FROM [dbo].[Article2Tag]
	WHERE ArticleId = @articleId

DELETE FROM [dbo].[ArticleComment]
	WHERE ArticleId = @articleId

DELETE FROM [dbo].[ArticleContent]
	WHERE ArticleId = @articleId

DELETE FROM [dbo].[ArticleImage]
	WHERE ArticleId = @articleId


DECLARE @bulletinId INT

SELECT @bulletinId = BulletinId FROM [dbo].[Bulletin] WHERE ArticleId = @articleId

DELETE FROM [dbo].[BulletinContent]
	WHERE BulletinId = @bulletinId

DELETE FROM [dbo].[Bulletin]
	WHERE BulletinId = @bulletinId

GO