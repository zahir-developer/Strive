CREATE PROC uspGetWeatherSubscriptionTenant
AS
BEGIN
SELECT DBSchemaName,DBUserName,DBPassword FROM tblSchemaMaster sm 
INNER JOIN tbltenantconfig tc	ON sm.TenantId = tc.TenantId
INNER JOIN tbltenantfeature tf	ON tc.featureid=tf.featureid
WHERE tf.featurename='Weather' and sm.IsDeleted=0
END