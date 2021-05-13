
CREATE PROCEDURE [StriveCarSalon].[uspGetTicketNumber] 
@LocationId INT
AS 
BEGIN

DECLARE @TicketNumer BIGINT,@JobId int;

--SET @TicketNumer = (Select isNull(Max(TicketNumber)+1, 000001) as TicketNumber from tblJob(NOLOCK) where locationId = @LocationId )

INSERT INTO tblJob (TicketNumber,LocationId,JobDate,IsActive, IsDeleted) values(0,1,GETDATE(), 0, 0)

Set @JobId=(Select SCOPE_IDENTITY() as JobId)

Select @JobId as TicketNumber ,@JobId as JobId

END