





-- =============================================
-- Author:		Arunkumae S
-- Create date: 02-11-2020
-- Description:	Monthly Customer Detail Report
-- =============================================




CREATE PROC [StriveCarSalon].[uspMonthlyCustomerDetail] --1,9,2020
(@LocationId int = null ,@Month int = null,@Year int = null)
AS
BEGIN
 

SELECT J.ClientId,
StriveCarSalon.GetClientName(J.ClientId) as ClientName,
J.JobId,
 TicketNumber,
 Model,
 VM.valuedesc as ModelDescription,
 Color,
 VC.valuedesc as ModelColor,
 JobDate,
 ISNULL(JP.MembershipId,0) as MemberShipId,
 ISNULL(BM.Price,0) as MembershipPrice,
 ISNULL(PaymentType,0) As PaymentType,
 GT.valuedesc As PaymentDescription,
StriveCarSalon.GetMemberShipName(JP.MembershipId) As MemberShipName,
SUM(JP.Amount) as TicketAmount
from [StriveCarSalon].[tblJob] (NoLock) J
inner join [StriveCarSalon].[tblJobItem] (NoLock) JI on JI.JobId = J.JobId and JI.IsActive = 1 and JI.IsDeleted = 0
inner join [StriveCarSalon].[tblJobPayment] (NoLock) JP on JP.JobId = J.JobId and JP.IsActive = 1 and JP.IsDeleted = 0 and JP.IsProcessed = 1
left join [StriveCarSalon].[tblClientVehicle] (NoLock) V on J.ClientId = V.ClientId and V.IsActive = 1 and V.IsDeleted = 0
left join [StriveCarSalon].[tblClient] (NoLock) C on J.ClientId = C.ClientId and C.IsActive = 1 and C.IsDeleted = 0
left join [StriveCarSalon].[tblMembership] (NoLock) BM on BM.MembershipId = JP.MembershipId and BM.IsActive = 1 and BM.IsDeleted = 0
left join [StriveCarSalon].GetTable('PaymentType') GT on GT.valueid = JP.PaymentType 
left join [StriveCarSalon].GetTable('VehicleColor') VC on VC.valueid = J.Color
left join [StriveCarSalon].GetTable('VehicleModel') VM on VM.valueid = J.Model
where DATEPART(month,jobdate) = @Month and DATEPART(YEAR,jobdate) = @Year and J.LocationId = @LocationId and J.IsActive = 1 and J.IsDeleted = 0
group by J.ClientId,J.JobId,TicketNumber,Model,VM.valuedesc,Color,VC.valuedesc,JobDate,JP.MembershipId,BM.Price,PaymentType,GT.valuedesc
order by J.ClientId


END