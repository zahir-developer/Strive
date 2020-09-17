CREATE Procedure [StriveCarSalon].[uspGetItemListByTicketNumber] --'9944'
(@TicketNumber varchar(10))
as begin
    Select tbljb.JobId,
	       tbljb.TicketNumber,
		   tblsr.ServiceName,
		   tbljbI.Price,
		   tbljbI.Quantity,
		   tbler.valuedesc as ServiceType
		   From StriveCarSalon.tblJob tbljb 
		   LEFT JOIN StriveCarSalon.tblJobItem tbljbI ON tbljb.JobId = tbljbI.JobId  
		   LEFT JOIN StriveCarSalon.tblService tblsr ON tbljbI.ServiceId = tblsr.ServiceId
		   LEFT JOIN [StriveCarSalon].[GetTable]('ServiceType') tbler ON  (tblsr.ServiceType = tbler.valueid)
		   where ISNULL(tbljb.IsDeleted,0)=0 AND ISNULL(tbljb.IsActive,1)=1 and 
		   (tbljb.TicketNumber =  @TicketNumber  )

		  Select   tbljp.TaxAmount AS Tax,
		           tbljp.Cashback,
				   tbljbIe.Price,
		           sum(tbljbIe.Quantity) as Quantity,
				   SUM(tbljbIe.Price * tbljbIe.Quantity) as Total,
				   Sum(((tbljbIe.Price * tbljbIe.Quantity) + tbljp.TaxAmount -tbljp.Cashback)) as GrandTotal
				   FROM StriveCarSalon.tblJobItem tbljbIe
				   INNER JOIN StriveCarSalon.tblJob tbljob ON tbljbIe.JobId = tbljob.JobId
				   LEFT JOIN StriveCarSalon.tblJobPayment tbljp ON tbljob.JobId = tbljp.JobId
				   where ISNULL(tbljbIe.IsDeleted,0)=0 AND ISNULL(tbljbIe.IsActive,1)=1 and 
		           (tbljob.TicketNumber = @TicketNumber )
				   Group by tbljbIe.Quantity,tbljp.TaxAmount,tbljp.Cashback,tbljbIe.Price
END
