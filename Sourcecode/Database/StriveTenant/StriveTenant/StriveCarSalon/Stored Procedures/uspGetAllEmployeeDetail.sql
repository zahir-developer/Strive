-- =============================================
-- Author:		Naveen
-- Create date: 15-07-2020
-- Description:	Retrieves All Employee details
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>
-- 29-07-2020, Zahir - Fixed duplicate issue & Modified logic for getting count of Document/Liability/Schedules
-- 21-10-2020, Zahir - Added employee chat communicationId.
-- 02-02-2021, Zahir - Added Offset, pagination and sorting
-- 24-02-2021, Zahir - Removed STUFF for employee phone number
-- 07-jun-2021, shalini - pagenumber and count for nullquery changes					 
------------------------------------------------
-- [StriveCarSalon].[uspGetAllEmployeeDetail] null,1,100,'ASC',firstname
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllEmployeeDetail]

@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL,
@PageSize INT = NULL,	
@SortOrder VARCHAR(5) = 'ASC',
@SortBy VARCHAR(100) = NULL
AS
BEGIN
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from tblEmployee);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END	

DROP Table IF EXISTS #Employee

SELECT 
emp.EmployeeId, 
empdet.EmployeeDetailId,
empdet.EmployeeCode,
emp.FirstName,
emp.LastName,
chatComm.CommunicationId,
--stuff((SELECT '/'+ phoneNumber FROM strivecarsalon.tblemployeeaddress 
--WHERE EmployeeId = emp.EmployeeId
--FOR XML PATH('')),1,1,'') as MobileNo,
empad.phoneNumber as MobileNo,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END FROM tblEmployeeLiability WHERE employeeid=emp.employeeid AND ISNULL(IsDeleted,0)=0) as Collisions,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM tblEmployeeDocument WHERE employeeid=emp.employeeid AND ISNULL(IsDeleted,0)=0) as Documents,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM tblSchedule WHERE employeeid=emp.employeeid AND ISNULL(IsDeleted,0)=0) as Schedules,

isnull(emp.IsActive,1) as Status
INTO #Employee
FROM tblEmployee emp 
LEFT JOIN tblEmployeeAddress empAd on emp.employeeId = empAd.employeeId and empAd.PhoneNumber != ''
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
LEFT JOIN tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId 
WHERE --empdet.EmployeeDetailId is NOT NULL AND 
ISNULL(emp.IsDeleted,0) = 0 
AND ((emp.FirstName like '%'+@Query+'%')
OR (emp.LastName like '%'+@Query+'%')
OR @Query is null OR @Query = ' ')

order by emp.IsActive DESC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN emp.FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN emp.LastName END ASC,
CASE WHEN @SortBy = 'MobileNo' AND @SortOrder='ASC' THEN empAd.PhoneNumber END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN emp.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN emp.LastName END DESC,
CASE WHEN @SortBy = 'MobileNo' AND @SortOrder='DESC' THEN empAd.PhoneNumber END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN emp.FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN emp.FirstName END ASC

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

Select * from #Employee

IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count 
FROM tblEmployee emp 
LEFT JOIN tblEmployeeAddress empAd on emp.employeeId = empAd.employeeId and empAd.PhoneNumber != ''
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
LEFT JOIN tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId 
WHERE  
ISNULL(emp.IsDeleted,0) = 0 
END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count 
FROM tblEmployee emp 
LEFT JOIN tblEmployeeAddress empAd on emp.employeeId = empAd.employeeId and empAd.PhoneNumber != ''
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
LEFT JOIN tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId 
WHERE --empdet.EmployeeDetailId is NOT NULL AND 
ISNULL(emp.IsDeleted,0) = 0 
AND ((emp.FirstName like '%'+@Query+'%')
OR (emp.LastName like '%'+@Query+'%')
OR @Query is null OR @Query = ' ')
END

END
