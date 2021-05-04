
CREATE PROCEDURE [StriveCarSalon].[uspGetDashboardDetail] --[StriveCarSalon].[uspGetDashboardDetail] 2033,'2020-12-15'
(@LocationId INT, @Date Date)
-- =============================================
-- Author:		Vineeth B
-- Create date: 14-12-2020
-- Description:	To get In Time Details
-- =============================================
AS
BEGIN

SELECT 
tblb.BayName,
CONVERT(VARCHAR(5),tblj.TimeIn,108) TimeIn 
FROM tblJob tblj 
INNER JOIN tblJobDetail tbljd 
ON tblj.JobId=tbljd.JobId AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0 AND tbljd.IsActive=1 AND ISNULL(tbljd.IsDeleted,0)=0
RIGHT JOIN tblBay tblb 
ON tbljd.BayId = tblb.BayId AND tblb.IsActive=1 AND ISNULL(tblb.IsDeleted,0)=0
WHERE tblj.LocationId=@LocationId AND tblj.JobDate=@Date 
END