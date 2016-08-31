CREATE PROCEDURE [stat].[SelectBrowserStats]
AS

SET NOCOUNT ON
  
SELECT DISTINCT
	[UserAgent],
	[BrowserName],
	[BrowserVersion],
	COUNT(*) As [Count]
FROM 
	[stat].[PageVisit]
GROUP BY
	[UserAgent],
	[BrowserName],
	[BrowserVersion]
