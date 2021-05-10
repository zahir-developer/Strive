
CREATE PROCEDURE [StriveCarSalon].[USPDELETECHATUSERGROUP] (@ChatGroupUserId int)
AS
BEGIN

	update tblChatUserGroup set IsDeleted= 1 where ChatGroupUserId = @ChatGroupUserId

END