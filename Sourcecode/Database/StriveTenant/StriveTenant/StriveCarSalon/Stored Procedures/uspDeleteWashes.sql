


CREATE PROCEDURE [StriveCarSalon].[uspDeleteWashes]
(
@JobId int
)
AS 
BEGIN

UPDATE [StriveCarSalon].[tblJob] set
IsDeleted =1
WHERE JobId = @JobId

UPDATE [StriveCarSalon].[tblJobDetail] set
IsDeleted =1
WHERE JobId = @JobId

END
