CREATE Procedure [StriveCarSalon].[uspDeleteJobItems]   
@TicketNumber varchar(10),
@LocationId int
as 
begin 

DECLARE @JobType NVARCHAR(15);

Select @JobType = cv.codevalue from tblJob j
JOIN tblCodeValue cv on cv.id = j.JobType
where TicketNumber = @TicketNumber and (j.locationId = @locationId OR @locationId is NUlL)


IF @JobType = 'Wash'

BEGIN
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;
set @JobId=(select top 1 JobId from tblJob where TicketNumber=@TicketNumber and (locationId = @locationId OR @locationId is NUlL) )
update tblJob set IsDeleted=1 where JobId = @JobId
update tblJobItem set IsDeleted=1 where JobId=@JobId
END

IF @JobType = 'Detail'
BEGIN

EXEC uspDeleteDetailSchedule @TicketNumber

END

END