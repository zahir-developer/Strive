



CREATE PROCEDURE [CON].[uspDeleteWashes]
(
@JobId int
)
AS 
BEGIN

UPDATE [CON].[tblJob] set
IsDeleted =1
WHERE JobId = @JobId

UPDATE [CON].[tblJobDetail] set
IsDeleted =1
WHERE JobId = @JobId

END
