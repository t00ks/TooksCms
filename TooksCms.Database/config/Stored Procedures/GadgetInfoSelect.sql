CREATE PROCEDURE [config].[GadgetInfoSelect]
(
	@roleId		INT = NULL,
	@gadgetId	INT = NULL,
	@areaTypeId	INT = NULL
)
AS

SELECT
	g.[GadgetId],
	g.[Name],
	g.[Description],
	g.[View],
	g.[DefaultColumn],
	r.[RoleName],
	a.[AreaType]
FROM
	[config].[Gadget] g
	INNER JOIN [security].[Gadget2Role2AreaType] g2r ON g2r.[GadgetId] = g.[GadgetId]
	INNER JOIN [security].[Role] r ON r.[RoleId] = g2r.[RoleId]
	INNER JOIN [security].[AreaType] a ON a.[AreaTypeId] = g2r.[AreaTypeId]
WHERE
	@roleId IS NULL OR r.RoleId = @roleId
AND	@gadgetId IS NULL OR g.GadgetId =  @gadgetId
AND	@areaTypeId IS NULL OR a.AreaTypeId = @areaTypeId

ORDER BY g.[GadgetId]
