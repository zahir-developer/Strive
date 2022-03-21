--[StriveCarSalon].[uspMonthlyCustomerDetail]  1,07,2021
-- =============================================
-- Author:		Arunkumae S
-- Create date: 02-11-2020
-- Description:	Monthly Customer Detail Report
-- =============================================
-- =============================================
-- History
-- =============================================
-- 30-Apr-2021 - Zahir - Changed JobPaymentId mapping with job table.
-- O5-May-2021 - Zahir - tblVehicleMake and tblVehicleModel table used instead of Codevalue table.
-- O5-May-2021 - Zahir - Removed Get Client/GetMembership name function.
-- 07-May-2021 - Vetriselvi - Enabled MembershipId and Membership price
-- 29-Jun-2021 - Vetriselvi - Added vehicle id in filter condition to get unique values.
-- 30-Jun-2021 - Vetriselvi - Removed payments details for PaymentType Membership.
-- 06-Jul-2021 - Vetriselvi - Added Difference Amount.
-- 07-Jul-2021 - Vetriselvi - Ticket Amount was not showing if the payment was made through Membership.
-- 13-Dec-2021 - Zahir - Membership Total Price taken from ClientVehicleMembershipDetails table instead of Membership table.
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspMonthlyCustomerDetail] 
(@LocationId int = null ,@Month int = null,@Year int = null)
AS
BEGIN
 
 
 DROP TABLE IF EXISTS #tblJob
 SELECT JobId, JobPaymentId, ClientId, TicketNumber, JobDate,VehicleId,Color,Model
 INTO #tblJob 
 FROM tblJob  J
 WHERE J.LocationId = @LocationId and J.IsActive = 1 and J.IsDeleted = 0
 and DATEPART(month,jobdate) = @Month and DATEPART(YEAR,jobdate) = @Year 

 DROP TABLE IF EXISTS #DifferenceAmount
SELECT J.JobId,
 isnull(JPD.Amount,0) as Amount
 INTO #DifferenceAmount
from #tblJob (NoLock) J
inner join [tblJobPayment] (NoLock) JP on JP.JobPaymentId = J.JobPaymentId and JP.IsActive = 1 and JP.IsDeleted = 0 and JP.IsProcessed = 1
inner join [tblJobPaymentDetail] (NoLock) JPD on JP.JobPaymentId = JPD.JobPaymentId 
left join GetTable('PaymentType') GT on GT.valueid = JPD.PaymentType
where GT.valuedesc = 'Membership' 

DROP TABLE IF EXISTS #TicketAmount
SELECT 
J.JobId,
SUM(isnull(JI.Price,0)) as TicketAmount
INTO #TicketAmount
from #tblJob (NoLock) J
inner join [tblJobItem] (NoLock) JI on JI.JobId = J.JobId and JI.IsActive = 1 and JI.IsDeleted = 0
inner join [tblJobPayment] (NoLock) JP on JP.JobPaymentId = J.JobPaymentId and JP.IsActive = 1 and JP.IsDeleted = 0 and JP.IsProcessed = 1
group by J.JobId

SELECT 
J.ClientId,
CONCAT(ISNULL(C.FirstName,''), ' ', ISNULL(C.LastName,''))  as ClientName,
J.JobId,
TicketNumber,
ISNULL(VM.ModelValue,'Unk') as Model,	
ISNULL(VC.valuedesc,'Unk') as Color,
JobDate,
ISNULL(tblcvmd.MembershipId,0) as MemberShipId,
ISNULL(tblcvmd.TotalPrice,0) as MembershipPrice,
--ISNULL(PaymentType,0) As PaymentType,
--GT.valuedesc As PaymentDescription,
ISNULL(BM.MembershipName,'Unk') as MembershipName,
--SUM(isnull(JPD.Amount,0)) as TicketAmount
 ta.TicketAmount,
 da.Amount AS DifferenceAmount
from #tblJob (NoLock) J
--inner join [tblJobItem] (NoLock) JI on JI.JobId = J.JobId and JI.IsActive = 1 and JI.IsDeleted = 0
inner join [tblJobPayment] (NoLock) JP on JP.JobPaymentId = J.JobPaymentId and JP.IsActive = 1 and JP.IsDeleted = 0 and JP.IsProcessed = 1
--inner join [tblJobPaymentDetail] (NoLock) JPD on JP.JobPaymentId = JPD.JobPaymentId 
left join [tblClientVehicle] (NoLock) V on J.ClientId = V.ClientId and V.IsActive = 1 and V.IsDeleted = 0 and J.VehicleId = V.VehicleId
left join [tblClient] (NoLock) C on J.ClientId = C.ClientId and C.IsActive = 1 and C.IsDeleted = 0

LEFT JOIN
    tblClientVehicleMembershipDetails tblcvmd  WITH(NOLOCK)
    ON V.VehicleId = tblcvmd.ClientVehicleId AND  ISNULL(tblcvmd.IsActive,1) = 1 AND ISNULL(tblcvmd.IsDeleted,0) = 0
LEFT JOIN
	tblMembership BM  WITH(NOLOCK) ON(BM.MembershipId = tblcvmd.MembershipId) AND ISNULL(BM.IsActive, 1) = 1 AND ISNULL(BM.IsDeleted,0)=0  and BM.LocationId = @LocationId
--left join [tblMembership] (NoLock) BM on BM.MembershipId = JP.MembershipId and BM.IsActive = 1 and BM.IsDeleted = 0
--left join GetTable('PaymentType') GT on GT.valueid = JPD.PaymentType
left join GetTable('VehicleColor') VC on VC.valueid = J.Color
left join tblVehicleModel VM on VM.ModelId = J.Model
LEFT JOIN #DifferenceAmount da ON da.JobId = J.JobId
LEFT JOIN #TicketAmount ta ON ta.JobId = J.JobId
--group by J.ClientId,C.FirstName, C.LastName, J.JobId,TicketNumber,BM.MembershipName,VM.ModelValue,VC.valuedesc,JobDate,tblcvmd.MembershipId,BM.Price--,PaymentType
--, da.Amount
order by J.ClientId


END
