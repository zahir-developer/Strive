SELECT 'ALTER TABLE '+T.NAME +' DROP COLUMN '+ c.name 
FROM sys.columns c
    JOIN sys.tables t ON c.object_id = t.object_id
WHERE c.name LIKE '%DateEntered%';


