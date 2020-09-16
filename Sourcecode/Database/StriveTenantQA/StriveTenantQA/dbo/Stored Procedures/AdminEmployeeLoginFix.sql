CREATE PROCEDURE [dbo].[AdminEmployeeLoginFix]
AS
-- =============================================
-- Author:		Zahir Hussain 
-- Create date: <Create Date,,>
-- Description:	To fix login Issue, When Admin Employee Deleted by mistake.
-- =============================================

BEGIN
	
Update StriveCarSalon.tblEmployee Set IsDeleted = 0 where EmployeeId = 1

Update StriveCarSalon.tblEmployeeLocation Set LocationId = 1, IsDeleted = 0, EmployeeId =1 WHERE EmployeeLocationId = 1

Update StriveCarSalon.tblEmployeeRole Set RoleId=1, IsDeleted = 0, EmployeeId = 1 WHERE EmployeeRoleId = 1 

EXEC [StriveCarSalon].[uspGetUserByAuthId] 5

END
