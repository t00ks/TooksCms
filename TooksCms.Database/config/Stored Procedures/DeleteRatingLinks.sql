CREATE PROCEDURE [config].[DeleteRatingLinks]
(
	@articleTypeid INT,
	@categoryId INT
)
AS

DELETE FROM 
	Rating2ArticleType2Category
WHERE
	ArticleTypeId = @articleTypeid
AND CategoryId = @categoryId
