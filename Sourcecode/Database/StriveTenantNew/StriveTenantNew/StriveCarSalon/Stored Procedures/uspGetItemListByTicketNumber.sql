CREATE Procedure [StriveCarSalon].[uspGetItemListByTicketNumber]  --'1245'
(@TicketNumber varchar(10))
as begin
    Select tbljb.JobId,
	       tbljb.TicketNumber,
		   tblsr.ServiceName,
		   tbler.valuedesc as ServiceType
		   From StriveCarSalon.tblJob tbljb 
		   INNER JOIN StriveCarSalon.tblJobItem tbljbI ON tbljb.JobId = tbljbI.JobId  
		   INNER JOIN StriveCarSalon.tblService tblsr ON tbljbI.ServiceId = tblsr.ServiceId
		   LEFT JOIN [StriveCarSalon].[GetTable]('ServiceType') tbler ON  (tblsr.ServiceType = tbler.valueid)
		   where ISNULL(tbljb.IsDeleted,0)=0 AND ISNULL(tbljb.IsActive,1)=1 and 
		   (tbljb.TicketNumber = @TicketNumber )

		  Select   tbljp.TaxAmount AS Tax,
		           tbljp.Cashback,
		           tbljbI.Price,
		           tbljbI.Quantity,
		           tblgc.TotalAmount as GiftCard,
		           tbljp.Amount as Total
				   FROM StriveCarSalon.tblJobItem tbljbI
				   INNER JOIN StriveCarSalon.tblJob tbljob ON tbljbI.JobId = tbljob.JobId
				   INNER JOIN StriveCarSalon.tblJobPayment tbljp ON tbljob.JobId = tbljp.JobId
		           INNER JOIN StriveCarSalon.tblGiftCardHistory tblgch ON tbljp.JobPaymentId = tblgch.JobPaymentId
		           INNER JOIN StriveCarSalon.tblGiftCard tblgc ON tblgch.GiftCardId = tblgc.GiftCardId
				   where ISNULL(tbljbI.IsDeleted,0)=0 AND ISNULL(tbljbI.IsActive,1)=1 and 
		           (tbljob.TicketNumber = @TicketNumber )
END