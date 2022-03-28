
/*
-----------------------------------------------------------------------------------------
Author              : Zahir
Create date         : 2020-06-13
Description         : Get new job Ticket ID
FRS					: Admin
Example				: EXEC uspGetJobTicketNumber 1

-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer		| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2021-05-13   | Zahir Hussain	| Job Id used as Ticket number to avoid duplicate ticket number.
  2  |  2022-02-08   | Zahir Hussain	| Ticket number prefix added.
-----------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [StriveCarSalon].[uspGetJobTicketNumber] 
@LocationId INT
AS 
BEGIN

DECLARE @Prefix NVARCHAR(3) = (Select StriveCarSalon.GetFirstLetters(LocationName) from tblLocation where locationId = @locationId)

BEGIN TRY

BEGIN TRANSACTION


DECLARE @TicketNumber BIGINT, @JobId int;
	
SET @TicketNumber = (Select isNull(MAX(CONVERT(INT,STUFF(TicketNumber, 1, PATINDEX('%[0-9]%', TicketNumber)-1, ''))) + 1, 000001) as TicketNumber from tblJobTicket where locationId = @LocationId)

IF @TicketNumber = 1
BEGIN

DECLARE @JobTicketNumber INT = NULL

SET @JobTicketNumber = (Select isNull(MAX(CONVERT(INT,STUFF(TicketNumber, 1, PATINDEX('%[0-9]%', TicketNumber)-1, ''))) + 1, 000001) from tblJob where LocationId = @LocationId);

INSERT INTO tblJobTicket (LocationId, TicketNumber) values (@LocationId, CONCAT(@Prefix,@TicketNumber))

SET @TicketNumber = @JobTicketNumber

END


INSERT INTO tblJob (TicketNumber,LocationId,JobDate,IsActive, IsDeleted) values(CONCAT(@Prefix,@TicketNumber),@LocationId,GETDATE(), 0, 0)

Set @JobId=(Select SCOPE_IDENTITY() as JobId)

UPDATE tblJobTicket set TicketNumber = CONCAT(@Prefix,@TicketNumber) Where LocationId = @LocationId

COMMIT TRANSACTION

Select CONCAT(@Prefix,@TicketNumber) as TicketNumber ,@JobId as JobId

END TRY


BEGIN CATCH
IF @@ERROR<>0
BEGIN

    SELECT ERROR_MESSAGE()

	ROLLBACK TRANSACTION
END
ELSE
COMMIT TRANSACTION
END CATCH





END