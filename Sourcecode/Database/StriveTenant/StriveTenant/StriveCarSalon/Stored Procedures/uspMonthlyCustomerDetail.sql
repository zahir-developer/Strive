--[StriveCarSalon].[uspMonthlyCustomerDetail]  1,06,2021
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
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspMonthlyCustomerDetail] 
(@LocationId int = null ,@Month int = null,@Year int = null)
AS
BEGIN
 

SELECT 
J.ClientId,
CONCAT(ISNULL(C.FirstName,''), ' ', ISNULL(C.LastName,''))  as ClientName,
J.JobId,
TicketNumber,
ISNULL(VM.ModelValue,'Unk') as Model,
ISNULL(VC.valuedesc,'Unk') as Color,
JobDate,
ISNULL(tblcvmd.MembershipId,0) as MemberShipId,
ISNULL(BM.Price,0) as MembershipPrice,
--ISNULL(PaymentType,0) As PaymentType,
--GT.valuedesc As PaymentDescription,
ISNULL(BM.MembershipName,'Unk') as MembershipName,
SUM(isnull(JI.Price,0)) as TicketAmount
--SUM(isnull(JP.Amount,0)) as Amount
from [tblJob] (NoLock) J
inner join [tblJobItem] (NoLock) JI on JI.JobId = J.JobId and JI.IsActive = 1 and JI.IsDeleted = 0
inner join [tblJobPayment] (NoLock) JP on JP.JobPaymentId = J.JobPaymentId and JP.IsActive = 1 and JP.IsDeleted = 0 and JP.IsProcessed = 1
inner join [tblJobPaymentDetail] (NoLock) JPD on JP.JobPaymentId = JPD.JobPaymentId 
left join [tblClientVehicle] (NoLock) V on J.ClientId = V.ClientId and V.IsActive = 1 and V.IsDeleted = 0
left join [tblClient] (NoLock) C on J.ClientId = C.ClientId and C.IsActive = 1 and C.IsDeleted = 0

LEFT JOIN
    tblClientVehicleMembershipDetails tblcvmd  WITH(NOLOCK)
    ON V.VehicleId = tblcvmd.ClientVehicleId AND  ISNULL(tblcvmd.IsActive,1) = 1 AND ISNULL(tblcvmd.IsDeleted,0) = 0
LEFT JOIN
	tblMembership BM  WITH(NOLOCK) ON(BM.MembershipId = tblcvmd.MembershipId) AND ISNULL(BM.IsActive, 1) = 1 AND ISNULL(BM.IsDeleted,0)=0
--left join [tblMembership] (NoLock) BM on BM.MembershipId = JP.MembershipId and BM.IsActive = 1 and BM.IsDeleted = 0
left join GetTable('PaymentType') GT on GT.valueid = JPD.PaymentType 
left join GetTable('VehicleColor') VC on VC.valueid = J.Color
left join tblVehicleModel VM on VM.ModelId = J.Model
where DATEPART(month,jobdate) = @Month and DATEPART(YEAR,jobdate) = @Year and J.LocationId = @LocationId and J.IsActive = 1 and J.IsDeleted = 0
group by J.ClientId,C.FirstName, C.LastName, J.JobId,TicketNumber,BM.MembershipName,VM.ModelValue,VC.valuedesc,JobDate,tblcvmd.MembershipId,BM.Price--,PaymentType
--,GT.valuedesc
order by J.ClientId


END