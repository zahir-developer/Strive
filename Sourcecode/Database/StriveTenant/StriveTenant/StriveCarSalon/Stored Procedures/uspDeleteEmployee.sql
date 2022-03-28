

CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployee]
    (
     @EmployeeId int)
AS 
BEGIN

--DECLARE @EmployeeId INT;
DECLARE @IsDeleted INT = 1;

    UPDATE [tblEmployee] 
    SET IsDeleted=@IsDeleted WHERE EmployeeId = @EmployeeId
	UPDATE [tblEmployeeDetail] 
    SET IsDeleted=@IsDeleted WHERE EmployeeId = @EmployeeId
	UPDATE [tblEmployeeAddress] 
    SET IsDeleted=@IsDeleted WHERE EmployeeId = @EmployeeId
END
