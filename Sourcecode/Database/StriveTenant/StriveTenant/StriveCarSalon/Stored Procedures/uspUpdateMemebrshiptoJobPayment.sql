CREATE PROCEDURE [StriveCarSalon].[uspUpdateMemebrshiptoJobPayment]
AS
DECLARE @MembershippaymentId INT,@AccountpaymentId INT

select @MembershippaymentId=cv.id from tblcodevalue CV
Join tblCodeCategory cc
ON cc.id=cv.CategoryId
Where CC.Category = 'PaymentType' and cv.CodeValue='Membership'

select @AccountpaymentId=cv.id from tblcodevalue CV
Join tblCodeCategory cc
ON cc.id=cv.CategoryId
Where CC.Category = 'PaymentType' and cv.CodeValue='Account'


--Select @MembershippaymentId
DROP TABLE IF EXISTS #MembershipJob
Select Jp.JobPaymentId,Mpd.Monthlycharge,MPd.Locationid,job.JobId,Cast(1 as INT) MembershipId
INTO #MembershipJob
from StgMembershipPaymentDetail MPD
Join tblJob job
on job.TicketNumber=Mpd.Recid
AND job.LocationId=Mpd.Locationid
Join tblJobPayment Jp
ON Jp.JobId=Job.JobId

Update MSj SET MSj.MembershipId=Mem.MembershipId from #MembershipJob Msj
Join tblMembership Mem
ON Mem.MembershipName=LTrim(Rtrim(Msj.Monthlycharge))
AND mem.LocationId=Msj.locationid


SelecT Mj.*
,cvmd.ClientMembershipId 
INTO #ClientVehicleMembership
from #MembershipJob MJ
join tblJobPaymentDetail jpd
on MJ.JobPaymentId=jpd.JobPaymentId
AND Jpd.PaymentType=@AccountpaymentId
Join tblJob Job
on job.JobId=mj.JobId
Join tblClientVehicleMembershipDetails CVMD
on CVMD.ClientVehicleId=job.VehicleId
ANd Cvmd.MembershipId=mj.MembershipId

Update Jp SET Jp.MembershipId=Cvm.ClientMembershipId from tblJobPayment jp
join #ClientVehicleMembership CVM
on CVM.JobPaymentId=Jp.JobPaymentId

Update jpd SET Jpd.PAymentType= @MembershippaymentId 
from tblJobPaymentDetail jpd
join #ClientVehicleMembership CVM
on CVM.JobPaymentId=jpd.JobPaymentId
ANd Jpd.PaymentType=@AccountpaymentId