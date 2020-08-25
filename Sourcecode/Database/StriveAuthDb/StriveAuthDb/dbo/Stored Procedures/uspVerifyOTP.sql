
CREATE PROC [dbo].[uspVerifyOTP]
(@Email varchar(64), @OTP varchar(10))
AS
BEGIN

/* 
------------------------------------------------------------------------------
-------------------Created by Zahir on 23-07-2020 ----------------------------
---------To Verfiy OTP using emailId/OTP and update IsVerfied flag as 1.------
------------------------------------------------------------------------------

*/

DECLARE @AuthOTPID INT = 0;

Select top 1 @AuthOTPID = o.AuthOTPId from tblAuthMaster m
JOIN tblAuthOTP o on o.AuthId = m.AuthId where m.EmailId=@Email and o.OTP = @OTP and IsVerified = 0 order by o.CreatedDate desc

IF (@AuthOTPID > 0)
BEGIN
Update tblAuthOTP SET IsVerified = 1 WHERE AuthOTPId = @AuthOTPID
SELECT 1
END
ELSE
SELECT 0

END