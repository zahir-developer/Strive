
CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployee]
    (
     @tblEmployeeId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblEmployee] 
    SET IsActive=0 WHERE EmployeeId = @tblEmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeDetail] 
    SET IsActive=0 WHERE EmployeeId = @tblEmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeAddress] 
    SET IsActive=0 WHERE RelationshipId = @tblEmployeeId
END