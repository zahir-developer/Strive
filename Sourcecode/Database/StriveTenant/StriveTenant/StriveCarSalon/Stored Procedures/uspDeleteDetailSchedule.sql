




-- =============================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To Inactive a Detail schedule
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspDeleteDetailSchedule] 
(@JobId int)
AS
BEGIN

UPDATE tblJob SET IsDeleted=1 WHERE JobId=@JobId;
UPDATE tblJobDetail SET IsDeleted=1 WHERE JobId=@JobId;
UPDATE tblJobItem SET IsDeleted=1 WHERE JobId=@JobId;
UPDATE tblBaySchedule SET IsDeleted=1 WHERE JobId=@JobId;
UPDATE tblJobServiceEmployee SET IsDeleted=1 WHERE JobItemId IN(SELECT JobItemId FROM tblJobItem WHERE JobId = @JobId);
END
