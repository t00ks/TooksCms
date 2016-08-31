CREATE PROCEDURE [lookup].[CategoryInfoSelect]
(
	@categoryId INT = NULL
)
AS

SELECT 
	c.CategoryName,
	p.CategoryName + ' - ' + c.CategoryName AS FullCategoryName,
	c.CategoryDescription,
	p.CategoryDescription AS ParentDescription,
	c.CategoryId,
	c.ParentCategoryId,
	c.ImageName
FROM
	[lookup].[Category] AS c
INNER JOIN [lookup].[Category] p ON c.ParentCategoryId = p.CategoryId
WHERE
	(@categoryId IS NULL) OR c.CategoryId = @categoryId