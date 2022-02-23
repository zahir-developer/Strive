CREATE Procedure [StriveCarSalon].[uspGetItemList] 
(@TicketNumber varchar(10),@Quantity int)
as begin
    Select tbljb.TicketNumber,
	       tbljp.TaxAmount AS Tax,
		   tbljp.Cashback,
		   tbljbI.Price,
		   tblsr.ServiceName,
		   --tblgc.TotalAmount as GiftCard,
		   0 as GiftCard,
		   tbler.valuedesc as ServiceType,
	       sum(tbljbI.Quantity) as Quantity,
	       SUM(tbljbI.Price * @Quantity) as Total,
		   Sum(((tbljbI.Price * @Quantity) + tbljp.TaxAmount +tbljp.Cashback) 
		   -- - tblgc.TotalAmount 
		   ) as GrandTotal 
		   From tblJob tbljb
		   INNER JOIN tblJobItem tbljbI ON tbljb.JobId = tbljbI.JobId
		   INNER JOIN tblService tblsr ON tbljbI.ServiceId = tblsr.ServiceId
		   INNER JOIN tblJobPayment tbljp ON tbljb.JobPaymentId = tbljp.JobPaymentId
		   --INNER JOIN tblGiftCard tblgc ON tbljp.GiftCardId = tblgc.GiftCardId
		   LEFT JOIN [CON].[GetTable]('ServiceType') tbler ON  (tblsr.ServiceType = tbler.valueid)
		    
		   where tbljb.TicketNumber = @TicketNumber 
		   Group by tbljb.TicketNumber,tbljp.TaxAmount,tbljp.Cashback,
		   --tblgc.TotalAmount,
		   tblsr.ServiceName,tbljbI.Price,tbler.valuedesc
		   --select SUM(Quantity * (Select Price from tblJobItem where JobId = 12)) as Total from tblJobItem

		   --select SUM(quantity) AS quantity,SUM(Price * quantity) as Total from tblJobItem where JobId = 12

END
