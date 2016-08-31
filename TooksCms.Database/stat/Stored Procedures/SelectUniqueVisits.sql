CREATE PROCEDURE [stat].[SelectUniqueVisits]
(
	@fromDate	DATETIME2 = NULL,
	@toDate		DATETIME2 = NULL
)
AS

SET NOCOUNT ON

SELECT DISTINCT
	[IpAddress],
	DATEADD(dd, 0, DATEDIFF(dd, 0, [DateTime])) AS [Date],
	COUNT(*) AS [Count]
FROM
	[stat].[PageVisit]
WHERE
	(@fromDate IS NULL OR [DateTime] >= @fromDate)
AND	(@toDate IS NULL OR DATEADD(dd, 0, DATEDIFF(dd, 0, [DateTime])) <= @toDate)
GROUP BY
	[IpAddress],
	DATEADD(dd, 0, DATEDIFF(dd, 0, [DateTime]))
ORDER BY
	[Date]
