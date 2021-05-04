

CREATE procedure [StriveCarSalon].[uspGetBonusSetup] --[StriveCarSalon].[uspGetBonusSetup] 3,2021,1
(@BonusMonth INT,@BonusYear INT,@LocationId INT)
AS
BEGIN
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')

SELECT 
BonusId
,LocationId
,BonusStatus
,BonusMonth
,BonusYear
,NoOfBadReviews
,BadReviewDeductionAmount
,NoOfCollisions
,CollisionDeductionAmount
,TotalBonusAmount
,IsActive
,IsDeleted
FROM tblBonus 
WHERE BonusMonth =@BonusMonth
AND BonusYear =@BonusYear
AND LocationId = @LocationId
AND IsActive = 1 AND ISNULL(IsDeleted,0)=0

DECLARE @BonusId INT =(SELECT BonusId FROM tblBonus WHERE BonusMonth=@BonusMonth AND
BonusYear=@BonusYear AND LocationId=@LocationId AND IsActive=1 AND ISNULL(IsDeleted,0)=0)

SELECT 
BonusRangeId
,BonusId
,Min
,Max
,BonusAmount
,Total
,IsActive
,IsDeleted
FROM tblBonusRange
WHERE BonusId =@BonusId
AND IsActive = 1 AND ISNULL(IsDeleted,0)=0


SELECT 
	tbll.LocationId,tbll.LocationName,COUNT(*) WashCount
	FROM tbljob tblj 
	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId)
	INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@WashId
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	--AND convert(varchar(7), tblj.JobDate)=CONCAT(@BonusYear,'-',@BonusMonth)
	and DATEPART(YEAR,tblj.JobDate )=@BonusYear 
	and DATEPART(MONTH,tblj.JobDate )=@BonusMonth 
	AND tblj.LocationId=@LocationId 
	AND tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tbll.IsActive=1
	AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0
	AND ISNULL(tbll.IsDeleted,0)=0
	GROUP BY tbll.LocationId,tbll.LocationName
END