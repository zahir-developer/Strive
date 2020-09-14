
-- =============================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To Inactive a Detail schedule
-- =============================================
CREATE proc [StriveCarSalon].[uspDeleteDetailSchedule]
(@JobId int)
AS
BEGIN
UPDATE [StriveCarSalon].[tblJob] SET IsDeleted=0 WHERE JobId =@JobId
END