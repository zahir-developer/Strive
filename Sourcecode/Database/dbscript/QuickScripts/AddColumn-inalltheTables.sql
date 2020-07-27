SELECT 'ALTER TABLE ' + QUOTENAME(ss.name) + '.' + QUOTENAME(st.name) + ' ADD [CreatedBy] int NULL;'
FROM sys.tables st
INNER JOIN sys.schemas ss on st.[schema_id] = ss.[schema_id]
WHERE st.is_ms_shipped = 0
AND NOT EXISTS (
    SELECT 1
    FROM sys.columns sc
    WHERE sc.[object_id] = st.[object_id]
    AND sc.name = 'CreatedBy'
);