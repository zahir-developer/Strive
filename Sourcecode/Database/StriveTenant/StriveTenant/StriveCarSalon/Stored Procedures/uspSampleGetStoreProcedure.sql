CREATE PROCEDURE [StriveCarSalon].[uspSampleGetStoreProcedure]
@EmployeeId INT,
@RoleId INT,
@CreatedUser Varchar(100)

/*
-----------------------------------------------------------------------------------------
Author              : Lenin
Create date         : 28-Aug-2020
Description         : Sample Procedure to show steps for Get procedure creation 
FRS					: Sample Procedure
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2020-Sep-01   | Lenin		| Added RollBack for errored transaction 

-----------------------------------------------------------------------------------------
*/
AS

BEGIN

SELECT  
	(tble.FirstName+tble.MiddleName+tble.LastName) AS EmployeeName,
	tble.BirthDate,
	tble.Gender,
	tble.AlienNo,
	tble.MaritalStatus,
	tblrm.RoleName
FROM
	tblemployee tble WITH (NOLOCK)
INNER JOIN
	tblEmployeeRole tbler WITH (NOLOCK)
ON		tbler.EmployeeId=tble.EmployeeId 
LEFT JOIN
	tblRoleMaster tblrm WITH (NOLOCK)
ON		tblrm.RoleMasterId=tbler.RoleId
WHERE tble.EmployeeId=@EmployeeId AND RoleId=@RoleId
	
END

