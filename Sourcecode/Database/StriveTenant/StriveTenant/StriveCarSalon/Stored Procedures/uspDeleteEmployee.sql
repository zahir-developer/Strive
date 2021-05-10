

CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployee]
    (
     @EmployeeId int)
AS 
BEGIN
    UPDATE [tblEmployee] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [tblEmployeeDetail] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [tblEmployeeAddress] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
END
