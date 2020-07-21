CREATE PROC uspResetPassword
(@Email varchar(64), @OTP varchar(10), @NewPassword varchar(100))
AS
BEGIN
DECLARE @ResetAuthId int
DECLARE @tempTable TABLE ( AuthId INT );

BEGIN TRAN
Update tblAuthMaster set PasswordHash = @NewPassword 

OUTPUT INSERTED.AuthId
INTO @tempTable

from tblAuthMaster am inner join tblAuthOTP aotp on
am.AuthId = aotp.AuthId where am.EmailId = @Email and aotp.OTP = @OTP and aotp.IsVerified=0

if Not exists(select 1 from @tempTable) 
begin	
	ROLLBACK TRANSACTION
	RAISERROR(N'Invalid Request',1,1)

end
else
begin
	update tblAuthOTP set IsVerified=1 where AuthId in(select authid from @tempTable)

	insert into tblResetAuth
	SELECT AuthId,3,@NewPassword,0,GETDATE() from @tempTable

	set @ResetAuthId=SCOPE_IDENTITY()

	insert into tblResetAuthAction
	SELECT @ResetAuthId,AuthId,1,GETDATE() from @tempTable
COMMIT TRAN
end
END