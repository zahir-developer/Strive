
CREATE PROCEDURE [StriveCarSalon].[uspDeleteChatUserGroup] (@ChatGroupUserId int)
AS
BEGIN

Update StriveCarSalon.tblChatUserGroup set IsDeleted= 1 where ChatGroupUserId = @ChatGroupUserId

END