  
CREATE PROCEDURE [StriveCarSalon].[uspGetItemProductListByTicketNumber]  
@TicketNumber varchar(10)  
AS   
  
BEGIN  
      
SELECT   
 tbljb.JobId,  
 tbljb.TicketNumber,  
 tblsr.ServiceId,  
 tblsr.ServiceName,  
 ISNULL(tbljbI.Price,0) Price,  
 ISNULL(tbljbI.Quantity,0) Quantity,  
 tbljbI.JobItemId,  
 tblcv.codevalue as ServiceType  
FROM   
 tblJob tbljb   
LEFT JOIN   
 tblJobItem tbljbI   
ON  tbljb.JobId = tbljbI.JobId    
LEFT JOIN   
 tblService tblsr   
ON  tbljbI.ServiceId = tblsr.ServiceId  
LEFT JOIN   
 tblCodeValue tblcv  
ON  tblcv.id=tblsr.ServiceType  
INNER JOIN [StriveCarSalon].GetTable('SalesItemType') gt
ON(gt.valueid = tbljbi.ItemTypeId)
-- [StriveCarSalon].[GetTable]('ServiceType') tbler   
--ON  tblsr.ServiceType = tbler.valueid  

WHERE  
gt.valuedesc='Service' 
AND tbljb.TicketNumber =  @TicketNumber    
AND ISNULL(tbljbI.IsDeleted,0)=0   
AND ISNULL(tbljbI.IsActive,1)=1   
  
;WITH Payment_Summary  
AS  
(  
SELECT     
 tbljob.TicketNumber,  
 tblcv.CodeValue PaymentType,  
 SUM(ISNULL(tbljp.Amount,0)) AS Amount,  
 SUM(ISNULL(tbljp.TaxAmount,0)) AS Tax,  
 SUM(ISNULL(tbljp.Cashback,0)) AS CashBack,  
 SUM(ISNULL(tbljpd.Amount,0)) AS Discount   
FROM   
 tblJob tbljob   
LEFT JOIN   
 tblJobPayment tbljp   
ON  tbljob.JobId = tbljp.JobId  
LEFT JOIN   
 tblJobPaymentDiscount tbljpd   
ON  tbljp.JobPaymentId = tbljpd.JobPaymentId  
LEFT JOIN  
 tblCodeValue tblCV  
ON  tblCV.id=tbljp.PaymentType  
WHERE   
 tbljob.TicketNumber = @TicketNumber  
AND ISNULL(tbljob.IsDeleted,0)=0   
AND ISNULL(tbljob.IsActive,1)=1   
GROUP BY tbljob.TicketNumber,tblcv.CodeValue  
)  
  
SELECT   
   TicketNumber  
 , CASE WHEN PaymentType='Cash' THEN SUM(Amount) ELSE 0 END AS Cash   
 , CASE WHEN PaymentType='Credit' THEN SUM(Amount) ELSE 0 END AS Credit  
 , CASE WHEN PaymentType='GiftCard' THEN SUM(Amount) ELSE 0 END AS GiftCard  
 , SUM(Tax) AS Tax  
 , SUM(CashBack) AS [CashBack]  
 , SUM(Amount) AS [Total]  
 , SUM(Discount) AS [Discount]   
 , (SUM(Amount)+SUM(Tax)+SUM(CashBack)+SUM(Discount)) AS [GrandTotal]  
FROM   
 Payment_Summary  
GROUP BY TicketNumber,PaymentType  
  

--Product list from JobItem  
SELECT   
 tbljb.JobId,  
 tbljb.TicketNumber,  
 tblprd.ProductId,  
 tblprd.ProductName,  
 ISNULL(tbljbI.Price,0) Price,  
 ISNULL(tbljbI.Quantity,0) Quantity,  
 tbljbI.JobItemId,  
 tblcv.codevalue as ProductType  
FROM   
 tblJob tbljb   
LEFT JOIN   
 tblJobItem tbljbI   
ON  tbljb.JobId = tbljbI.JobId    
LEFT JOIN   
 tblProduct tblprd  
ON  tbljbI.ServiceId = tblprd.ProductId  
LEFT JOIN   
 tblCodeValue tblcv  
ON  tblcv.id=tblprd.ProductType  
INNER JOIN [StriveCarSalon].GetTable('SalesItemType') gt
ON(gt.valueid = tbljbi.ItemTypeId)
-- [StriveCarSalon].[GetTable]('ServiceType') tbler   
--ON  tblsr.ServiceType = tbler.valueid  
WHERE   
 gt.valuedesc='Product'
AND tbljb.TicketNumber =  @TicketNumber    
AND ISNULL(tbljbI.IsDeleted,0)=0   
AND ISNULL(tbljbI.IsActive,1)=1   
  
END