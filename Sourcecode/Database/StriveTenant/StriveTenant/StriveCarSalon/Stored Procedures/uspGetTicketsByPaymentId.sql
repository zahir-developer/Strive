-- =============================================
-- Author:		Shalini
-- Create date: 03-june-2021
-- Description:	To get all tickets for particular jobpayment id
-- =============================================

-- EXEC [StriveCarSalon].[uspGetTicketsByPaymentId] 411330
-- =====================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetTicketsByPaymentId] 
(@JobPaymentId int)
AS
BEGIN
Select 
tbj.ticketnumber 
from tblJob tbj with(nolock)
inner JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tbj.JobPaymentId = tbljp.JobPaymentId
 --AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0
  AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
WHERE  isnull(tbj.IsDeleted,0)=0 
AND tbljp.JobPaymentId=@JobPaymentId
END