
--EXEC [StriveCarSalon].[uspGetTicketNumber] 20


CREATE PROCEDURE [StriveCarSalon].[uspGetTicketNumber] 
@LocationId INT
AS 
BEGIN
 
 DECLARE @TicketNumer BIGINT ;

SET @TicketNumer = (Select isNull(Max(TicketNumber)+1, 000001) as TicketNumber from tblJob(NOLOCK) where locationId = @LocationId )

Select @TicketNumer as TicketNumber

INSERT INTO StriveCarSalon.tblJob (TicketNumber,LocationId,JobDate,IsActive) values(@TicketNumer,1,GETDATE(), 0)

END