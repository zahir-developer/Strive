-- =============================================
-- Author:		Juki B
-- Create date: 2022-02-10
-- Description:	Procedure to update loginid for employee in auth table
-- =============================================

CREATE PROCEDURE [dbo].[uspUpdateLoginId]
(@AuthId int, 
 @LoginId varchar(32)
)
AS
BEGIN
 IF(@AuthId > 0)
 BEGIN
 update tblAuthMaster set LoginId = @LoginId where AuthId = @AuthId
 END
END