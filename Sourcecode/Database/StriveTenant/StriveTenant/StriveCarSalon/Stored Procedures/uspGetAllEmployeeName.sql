-- =============================================
-- Author:		Zahir
-- Create date: 12-04-2020
-- Description:	Retrieves All Employee Names
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllEmployeeName]

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
emp.FirstName,
emp.LastName,
chatComm.CommunicationId
INTO #Employee
FROM tblEmployee emp 
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
WHERE --empdet.EmployeeDetailId is NOT NULL AND 
ISNULL(emp.IsDeleted,0) = 0 
AND ((emp.FirstName like '%'+@Query+'%')
OR (emp.LastName like '%'+@Query+'%')
OR @Query is null OR @Query = ' ')

order by emp.IsActive DESC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN emp.FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN emp.LastName END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN emp.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN emp.LastName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN emp.FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN emp.FirstName END ASC

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

Select DISTINCT * from #Employee order by firstname


END