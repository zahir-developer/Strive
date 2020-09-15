CREATE PROCEDURE [StriveCarSalon].[uspSampleStoreProcedure]
@EmployeeId INT,
@RoleId INT,
@CreatedUser Varchar(100)

/*
-----------------------------------------------------------------------------------------
Author              : Lenin
Create date         : 28-08-2020
Description         : Sample Procedure to show steps for procedure creation 
FRS					: Sample Procedure
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2020-08-01   | Lenin		| Added RollBack for errored transaction 

-----------------------------------------------------------------------------------------
*/
AS

BEGIN TRY

BEGIN TRANSACTION

	INSERT INTO tblEmployeeRole 
	(EmployeeId,RoleId,IsDefault,IsActive,IsDeleted,CreatedBy,CreatedDate)
	VALUES
	(@EmployeeId,@RoleId,0,1,0,@CreatedUser,GETDATE())

END TRY

BEGIN CATCH
IF @@ERROR<>0
BEGIN
	ROLLBACK TRANSACTION
END
ELSE
COMMIT TRANSACTION
END CATCH
