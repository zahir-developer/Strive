CREATE TRIGGER [DropRestriction]   
ON DATABASE   
FOR DROP_TABLE, DROP_PROCEDURE, DROP_FUNCTION, DROP_VIEW, DROP_TYPE
AS   

IF original_Login()<>'sa'
BEGIN
   PRINT 'DROPING DB OBJECT IS RESTRICTED'   

   ROLLBACK;
 END