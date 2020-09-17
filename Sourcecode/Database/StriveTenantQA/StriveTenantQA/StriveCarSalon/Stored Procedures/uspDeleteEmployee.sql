


CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployee]
    (
     @EmployeeId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblEmployee] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeDetail] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [StriveCarSalon].[tblEmployeeAddress] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
END
