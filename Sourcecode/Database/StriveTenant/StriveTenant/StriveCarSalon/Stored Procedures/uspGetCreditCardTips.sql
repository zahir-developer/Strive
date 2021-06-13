-- =============================================
-- Author:		Shalini
-- Create date: 03-june-2021
-- Description:	To get Creditcard amount
-- ==============================================
-- EXEC [StriveCarSalon].[uspGetCreditCardTips] 
-- =====================================================
CREATE PROCEDURE [StriveCarSalon].[uspGetCreditCardTips]
@LocationId INT = NULL
AS
BEGIN
SELECT	
	SUM(Credit) AS Credit
FROM 
(SELECT  
	 SUM(ISNULL(tbljpd.Amount,0)) AS Credit
FROM	tblJob tbljob 
LEFT JOIN 	tblJobPayment tbljp ON		tbljob.JobId = tbljp.JobId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 	tblJobPaymentDetail tbljpd ON		tbljp.JobPaymentId = tbljpd.JobPaymentId 
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN GetTable('Paymenttype') gt	on		tbljpd.PaymentType = gt.valueid 

WHERE tbljob.jobdate ='2021-06-03' and gt.Valuedesc='Card'
and ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
Group by tbljob.TicketNumber
) sub
END