
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 05-11-2020
-- Description:	Changes the status of unread messages.
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspUpdateChatUnreadMessageState]
	@SenderId INT,
	@RecipientId INT,
	@GroupId INT = NULL
AS
BEGIN

IF(@SenderId !=0 AND @RecipientId != 0 )
BEGIN
	Update tblChatMessageRecipient SET IsRead = 1 WHERE SenderId=@SenderId and RecipientId = @RecipientId and IsRead = 0
END

IF(@GroupId IS NOT NULL OR @GroupId != 0 )
BEGIN
	DELETE FROM tblChatGroupRecipient WHERE RecipientId=@RecipientId and ChatGroupId = @GroupId and IsRead = 0
END

END