

CREATE PROCEDURE [StriveCarSalon].[uspGetTicketNumber]

AS 
BEGIN
DECLARE @TicketNumber varchar(50) 
SELECT @Ticketnumber=(select floor(rand()*1000000-1))

WHILE EXISTS(
SELECT * FROM [StriveCarSalon].[tblJob]
WHERE TicketNumber =@TicketNumber
)
Begin  
select @Ticketnumber=(select floor(rand()*1000000-1))
end
SELECT @TicketNumber

END