-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-10-2020
-- Description:	Update the communicationId based on EmployeeId
-- =============================================
CREATE PROCEDURE [StriveCarSalon].UpdateChatCommunicationDetail
	@EmployeeId INT,
	@CommunicationId NVARCHAR(50)
AS
BEGIN

DECLARE @Exist INT = 0;

SET @Exist = (Select count(1) from tblChatCommunication where EmployeeId = @employeeId)

if @Exist > 0
BEGIN
	Update tblChatCommunication SET CommunicationId= @CommunicationId WHERE EmployeeId = @EmployeeId
END
ELSE
BEGIN
	Insert into tblChatCommunication(CommunicationId,EmployeeId) values(@CommunicationId,@EmployeeId)
END
END