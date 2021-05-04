CREATE PROCEDURE [StriveCarSalon].[uspGetTicketNumber] 
@LocationId INT
AS 
BEGIN

DECLARE @TicketNumer BIGINT,@JobId int;

SET @TicketNumer = (Select isNull(Max(TicketNumber)+1, 000001) as TicketNumber from tblJob(NOLOCK) where locationId = @LocationId )

INSERT INTO StriveCarSalon.tblJob (TicketNumber,LocationId,JobDate,IsActive, IsDeleted) values(@TicketNumer,1,GETDATE(), 0, 0)

Set @JobId=(Select SCOPE_IDENTITY() as JobId)

Select @TicketNumer as TicketNumber ,@JobId as JobId

END