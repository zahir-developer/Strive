

CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployee]
    (
     @tblEmployeeId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblEmployee] 
    SET IsDeleted=1 WHERE EmployeeId = @tblEmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeDetail] 
    SET IsDeleted=1 WHERE EmployeeId = @tblEmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeAddress] 
    SET IsDeleted=1 WHERE EmployeeId = @tblEmployeeId
END