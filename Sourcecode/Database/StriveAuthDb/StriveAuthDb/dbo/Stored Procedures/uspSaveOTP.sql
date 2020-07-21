CREATE PROC uspSaveOTP
(@Email varchar(64), @OTP varchar(10), @DateEntered datetime)
AS
BEGIN
insert into tblAuthOTP
select am.authid,am.mobilenumber,@Email,0,@DateEntered,@OTP from tblAuthMaster am where am.EmailId=@Email
END