-- =============================================
-- Author:		Vineeth B
-- Create date: 24-11-2020
-- Description:	To delete Bonus Setup
-- =============================================
create procedure [StriveCarSalon].uspDeleteBonusSetup
(@BonusId int)
AS
BEGIN
UPDATE tblBonus SET IsDeleted=1 WHERE BonusId=@BonusId;
UPDATE tblBonusRange SET IsDeleted=1 WHERE BonusId=@BonusId;
END