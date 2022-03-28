--====================================================
---------------------History---------------------------
--====================================================
-- 2022-02-17 - Zahir H - Membership payment jobs added
-- 2022-02-17 - Zahir H - Query optimization.
-- 2022-02-18 - Zahir H - Membership payment job changes reverted
-- 2022-02-18 - Zahir H - Membership payment job changes reverted
-- 2022-02-18 - Zahir H - All the CreditAccount History shown(Job payment) irrespective of TransactionType
--====================================================	
--EXEC [StriveCarSalon].[uspGetCreditAccountBalanceHistory] 57450

CREATE PROCEDURE [StriveCarSalon].[uspGetCreditAccountBalanceHistory]     
(@ClientId varchar(10))    
as    
BEGIN    

--DECLARE @ClientId INT = 57450 

declare @CreditAccountID int    
  
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')  
DECLARE @DetailServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Detail Package')  

Drop TABLE IF EXISTS #CreditAccountHistory    
      
Drop TABLE IF EXISTS #CreditPaymentHistory    
  
 SELECT  CreditAccountHistoryId
,TransactionType              
,Amount          
,isnull(IsActive,1) as Status,  
CAST(ISNULL(UpdatedDate,CreatedDate) AS DATETIME) AS CreatedDate,
CAST(UpdatedDate AS DATETIME) AS UpdatedDate 
,Comments 
,ClientId
,isnull(IsActive,0) IsActive,
isnull(IsDeleted,0) IsDeleted,
JobPaymentId
INTO #CreditPaymentHistory
from [tblCreditAccountHistory]   
where ClientId = @ClientId    
AND isnull(IsDeleted,0) = 0 
--AND TransactionType IS NULL 
Order by ISNULL(UpdatedDate, CreatedDate) DESC

Select * from #CreditPaymentHistory

DROP TABLE IF EXISTS #ServiceType  
Select valueid, valuedesc into #ServiceType from GetTable('ServiceType')  

DROP TABLE IF EXISTS #tblJobAndPayment

Select tj.jobId, tbljp.jobpaymentId, tj.TicketNumber, tj.CreatedDate, tbljp.Amount into #tblJobAndPayment 
from tblJob tj
JOIN tblJobPayment tbljp on tbljp.JobPaymentId = tj.JobPaymentId
where ClientId = @ClientId and tj.IsDeleted = 0
AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0   
AND tj.JobType = @WashId  


  
DROP TABLE IF EXISTS #JobPaymentIds

CREATE TABLE #JobPaymentIds (JobPaymentId INT)

INSERT INTO #JobPaymentIds (JobPaymentId)(
Select jp.JobPaymentId from tblJob j
JOIN #tblJobAndPayment jp on j.JobPaymentId = jp.JobPaymentId
join #CreditPaymentHistory tblcah ON tblcah.JobPaymentId = j.JobPaymentId
WHERE j.ClientId = @ClientId)

DROP TABLE IF EXISTS #JobServiceEmployee  
  
Select tblj.JobId,  
tbljse.JobServiceEmployeeId,  
tbljse.JobItemId,  
tbljse.ServiceId,  
tbls.ServiceName,  
--tbls.Cost,  
tblji.price as Cost,  
tbljse.EmployeeId,  
ISNULL(tbljse.CommissionAmount,'0.00')CommissionAmount,  
CONCAT(tble.FirstName,' ',tble.LastName) AS EmployeeName  
INTO #JobServiceEmployee  
from tblJobServiceEmployee tbljse with(nolock)   
INNER JOIN tblJobItem tblji ON tbljse.JobItemId = tblji.JobItemId  
INNER JOIN tblService tbls ON(tbljse.ServiceId = tbls.ServiceId)  
INNER JOIN tblEmployee tble ON(tbljse.EmployeeId = tble.EmployeeId)  
INNER JOIN #tblJobAndPayment tblj ON tblj.JobId = tblji.JobId  
join #JobPaymentIds tblcah ON tblcah.JobPaymentId = tblj.JobPaymentId 

WHERE  isnull(tblji.IsDeleted,0)=0  
AND tblji.IsActive=1  
AND isnull(tbljse.IsDeleted,0)=0  
AND tbljse.IsActive=1  
  
drop table if exists #checkout  
SELECT tblj.jobid,  
CASE WHEN st.valuedesc !='Additional Services' THEN TRIM(tbls.ServiceName) END AS [Services]  
INTO   
 #Checkout  
  
FROM [tblCreditAccountHistory] tblcah     
JOIN tblJobPayment tbljp  ON tblcah.JobPaymentId = tbljp.JobPaymentId
join #tblJobAndPayment tblj WITH(NOLOCK) on tbljp.JobPaymentId = tblj.JobPaymentId   
INNER JOIN tblJobItem tblji (NOLOCK) on tblj.JobId = tblji.JobId  
INNER JOIN tblService tbls  WITH(NOLOCK) ON(tblji.ServiceId = tbls.ServiceId)  
INNER JOIN #ServiceType st ON(tbls.ServiceType = st.valueid)  
--INNER JOIN #JobType jt ON(tblj.JobType = jt.valueid)  
--LEFT JOIN #JobStatus js ON(tblj.JobStatus = js.valueid)  
 --LEFT JOIN #PaymentStatus ps ON(tbljp.PaymentStatus = ps.valueid)  
where tblcah.ClientId = @ClientId    
  
 --Statement  
DROP TABLE IF EXISTS #CreditAccountStatement  
Select DISTINCT tj.TicketNumber ,  tj.CreatedDate,
tblcah.Amount,  
STUFF((SELECT DISTINCT', ' + [Services]  
    FROM #Checkout C1  
 WHERE C1.JobId= c.JobID  
    FOR XML PATH('')  
 ), 1, 2, '') as ServiceCompleted ,  
CAST(jpm.CreatedDate AS DATETIME) AS TransactionDate  
INTO #CreditAccountStatement  
from [tblCreditAccountHistory] tblcah     
join #tblJobAndPayment jpm on tblcah.JobPaymentId =jpm.JobPaymentId    
join #tblJobAndPayment tj on jpm.JobPaymentId = tj.JobPaymentId    
join #Checkout c on c.JobId = tj.JobId    
where tblcah.ClientId = @ClientId    
AND tblcah.IsDeleted=0     
 
--declare @Balance decimal(18,2);    
--Select @Balance=SUM(Amount) from #CreditAccountHistory     
    
--Select @CreditAccountID as CreditAccountId , @Balance as BalanceAmount    
    
Select * from #CreditAccountStatement Order by CreatedDate DESC

  --History  
DROP TABLE IF EXISTS #CreditAccountHistory  
  
select     DISTINCT    tj.TicketNumber , tj.CreatedDate,   
tblcah.Amount,  
STUFF(  
   (SELECT DISTINCT', ' + [Services]  
    FROM #Checkout C1  
 WHERE C1.JobId= c.JobID  
    FOR XML PATH('')  
 ), 1, 2, '') as ServiceCompleted ,  
CAST(ISNULL(tblcah.UpdatedDate, tblcah.CreatedDate) AS DATETIME) AS TransactionDate  
,jpm.Amount as  Price  
,se.EmployeeName AS Detailer  
,se.CommissionAmount
INTO #CreditAccountHistory  
from [tblCreditAccountHistory] tblcah     
join #tblJobAndPayment jpm on tblcah.JobPaymentId =jpm.JobPaymentId    
join #tblJobAndPayment tj on jpm.JobPaymentId = tj.JobPaymentId    
join #Checkout c on c.JobId = tj.JobId    
join #JobServiceEmployee se on se.JobId  = tj.JobId   
where tblcah.ClientId = @ClientId    
AND tblcah.IsDeleted=0 

select * from #CreditAccountHistory order by TransactionDate DESC

declare @rolledBackAmount DECIMAL(18,2), @ActivityAmount DECIMAL(18,2)


select @ActivityAmount = SUM(isnull(CAH.Amount,0))
FROM tblClient tblcli 
JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
WHERE tblcli.ClientId = @ClientId AND CAH.IsDeleted = 0

Select (ISNULL(tblcli.Amount,0) + ISNULL(@ActivityAmount,0)) CreditAmount
--,ISNULL(tblcli.Amount,0) ,-1*@rolledBackAmount , @ActivityAmount
From  tblClient tblcli 
where  tblcli.ClientId = @ClientId 

end