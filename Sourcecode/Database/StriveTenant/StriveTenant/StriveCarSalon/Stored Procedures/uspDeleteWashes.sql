


CREATE PROCEDURE [StriveCarSalon].[uspDeleteWashes]
(
@JobId int
)
AS 
BEGIN

UPDATE [tblJob] set
IsDeleted =1
WHERE JobId = @JobId

UPDATE [tblJobDetail] set
IsDeleted =1
WHERE JobId = @JobId

END
