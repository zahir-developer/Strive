/*
-----------------------------------------------------------------------------------------
Author              : Zahir
Create date         : 2020-06-13
Description         : Get new job Ticket ID
FRS					: Admin
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2021-05-13   | Zahir		| Job Id used as Ticket number to avoid duplicate ticket number.

-----------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [StriveCarSalon].[uspGetTicketNumber] 
@LocationId INT
AS 
BEGIN

DECLARE @TicketNumer BIGINT,@JobId int;

--SET @TicketNumer = (Select isNull(Max(TicketNumber)+1, 000001) as TicketNumber from tblJob(NOLOCK) where locationId = @LocationId )

INSERT INTO tblJob (TicketNumber,LocationId,JobDate,IsActive, IsDeleted) values(0,1,GETDATE(), 0, 0)

Set @JobId=(Select SCOPE_IDENTITY() as JobId)

Update tblJob set TicketNumber = @JobId Where JobId = @JobId

Select @JobId as TicketNumber ,@JobId as JobId

END