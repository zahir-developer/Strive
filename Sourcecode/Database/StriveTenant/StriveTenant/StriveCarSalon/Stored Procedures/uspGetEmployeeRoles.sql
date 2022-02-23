CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeRoles]
as
-- =============================================
-- Author:		Vineeth B
-- Create date: 28-07-2020
-- Description:	To get Employee Roles
-- =============================================

-----------------------------------------------------------------------------------------------------------------------------------------
-- Rev | Date Modified  | Developer	| Change Summary
-----------------------------------------------------------------------------------------------------------------------------------------
--  1  |  2020-Oct-14   | Vineeth	| Added NULL Condition in CodeCategory and CodeValue table
--  2  |  2020-Oct-15   | Vineeth   | Removed tblcodevalue and tblcodecategory table and include RoleMaster table
-----------------------------------------------------------------------------------------------------------------------------------------
begin
select 
RoleMasterId,
RoleName
from [tblRoleMaster] 
WHERE ISNULL(IsDeleted,0)=0 AND ISNULL(IsActive,1)=1
end
