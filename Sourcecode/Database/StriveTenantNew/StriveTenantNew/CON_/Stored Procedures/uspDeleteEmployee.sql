


CREATE PROCEDURE [CON].[uspDeleteEmployee]
    (
     @EmployeeId int)
AS 
BEGIN
    UPDATE [CON].[tblEmployee] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [CON].[tblEmployeeDetail] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
	UPDATE [CON].[tblEmployeeAddress] 
    SET IsDeleted=1 WHERE EmployeeId = @EmployeeId
END