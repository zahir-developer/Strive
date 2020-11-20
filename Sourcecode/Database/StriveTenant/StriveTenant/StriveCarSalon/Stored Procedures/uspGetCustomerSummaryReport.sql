


-- =================================================
-- Author:		Vineeth B
-- Create date: 03-10-2020
-- Description:	To get CustomerSummaryReport Details
-- =================================================
create   PROCEDURE  [StriveCarSalon].[uspGetCustomerSummaryReport] 
--1,'2020'
@LocationId INT, 
@Date VARCHAR(4)

AS

BEGIN

DROP TABLE  IF EXISTS #NumberOfMembershipAccounts
SELECT 
    CAST(DATEPART(MONTH,StartDate)AS INT) Month,
	CAST(COUNT(*) AS DECIMAL) NumberOfMembershipAccounts
INTO 
	#NumberOfMembershipAccounts
FROM 
	tblClientVehicleMembershipDetails 
WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0
AND LocationId=@LocationId
AND SUBSTRING(CAST(StartDate AS VARCHAR(10)),1,4)=@Date
GROUP BY DATEPART(MONTH,StartDate)


DROP TABLE  IF EXISTS #Customer
SELECT 
	DATEPART(MONTH,JobDate) Month,
	CAST(COUNT(DISTINCT ClientId) AS DECIMAL) Customer 
INTO 
	#Customer
FROM 
	tblJob 
WHERE LocationId=@LocationId
AND SUBSTRING(CAST(JobDate AS VARCHAR(10)),1,4)=@Date
AND IsActive=1 AND ISNULL(IsDeleted,0)=0
GROUP BY DATEPART(MONTH,JobDate)

DROP TABLE  IF EXISTS #WashesCompletedCount
SELECT 
	DATEPART(MONTH,JobDate) Month,
	CAST(COUNT(*) AS DECIMAL)WashesCompletedCount 
INTO 
	#WashesCompletedCount
FROM 
	tblJob 
WHERE LocationId=@LocationId
AND SUBSTRING(CAST(JobDate AS varchar(10)),1,4)=@Date
AND IsActive=1 AND ISNULL(IsDeleted,0)=0
AND JobType =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
AND JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
GROUP BY DATEPART(MONTH,JobDate)


SELECT  
	Cu.Month,
	ISNULL(Noma.NumberOfMembershipAccounts,0) NumberOfMembershipAccounts,
	ISNULL(Cu.Customer,0) CustomerCount,
	ISNULL(Wcc.WashesCompletedCount,0) WashesCompletedCount,
	CASE	WHEN ISNULL(cu.Customer,0)=0 THEN ISNULL(wcc.WashesCompletedCount,0) 
			ELSE CAST((ISNULL(wcc.WashesCompletedCount,0)/ISNULL(cu.Customer,0))AS decimal(9,2)) END AS AverageNumberOfWashesPerCustomer,
	CASE	WHEN ISNULL(noma.NumberOfMembershipAccounts,0)=0 THEN ISNULL(wcc.WashesCompletedCount,0)
			ELSE CAST((ISNULL(wcc.WashesCompletedCount,0)/ISNULL(noma.NumberOfMembershipAccounts,0)) AS decimal(9,2)) END AS TotalNumberOfWashesPerCustomer,
	CASE	WHEN ISNULL(noma.NumberOfMembershipAccounts,0)=0 THEN ISNULL(cu.Customer,0) * 100
			ELSE CAST(((ISNULL(cu.Customer,0)/ISNULL(noma.NumberOfMembershipAccounts,0)) * 100)AS decimal(9,2)) END AS PercentageOfCustomersThatTurnedUp
FROM 
	#Customer Cu
LEFT JOIN
	#NumberOfMembershipAccounts Noma
ON	Noma.Month=Cu.Month
LEFT JOIN
	#WashesCompletedCount Wcc
ON	Wcc.Month=cu.Month
DROP TABLE  IF EXISTS #NumberOfMembershipAccounts
DROP TABLE  IF EXISTS #Customer
DROP TABLE  IF EXISTS #WashesCompletedCount
END