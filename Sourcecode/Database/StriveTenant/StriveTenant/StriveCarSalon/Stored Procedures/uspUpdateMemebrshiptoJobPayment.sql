CREATE PROCEDURE [StriveCarSalon].[uspUpdateMemebrshiptoJobPayment]
AS
DECLARE @MembershippaymentId INT,@AccountpaymentId INT

select @MembershippaymentId=cv.id from StriveCarSalon.tblcodevalue CV
Join StriveCarSalon.tblCodeCategory cc
ON cc.id=cv.CategoryId
Where CC.Category = 'PaymentType' and cv.CodeValue='Membership'

select @AccountpaymentId=cv.id from StriveCarSalon.tblcodevalue CV
Join StriveCarSalon.tblCodeCategory cc
ON cc.id=cv.CategoryId
Where CC.Category = 'PaymentType' and cv.CodeValue='Account'


--Select @MembershippaymentId
DROP TABLE IF EXISTS #MembershipJob
Select Jp.JobPaymentId,Mpd.Monthlycharge,MPd.Locationid,job.JobId,Cast(1 as INT) MembershipId
INTO #MembershipJob
from StriveCarSalon.StgMembershipPaymentDetail MPD
Join StriveCarSalon.tblJob job
on job.TicketNumber=Mpd.Recid
AND job.LocationId=Mpd.Locationid
Join StriveCarSalon.tblJobPayment Jp
ON Jp.JobId=Job.JobId

Update MSj SET MSj.MembershipId=Mem.MembershipId from #MembershipJob Msj
Join StriveCarSalon.tblMembership Mem
ON Mem.MembershipName=LTrim(Rtrim(Msj.Monthlycharge))
AND mem.LocationId=Msj.locationid


SelecT Mj.*
,cvmd.ClientMembershipId 
INTO #ClientVehicleMembership
from #MembershipJob MJ
join StriveCarSalon.tblJobPaymentDetail jpd
on MJ.JobPaymentId=jpd.JobPaymentId
AND Jpd.PaymentType=@AccountpaymentId
Join StriveCarSalon.tblJob Job
on job.JobId=mj.JobId
Join StriveCarSalon.tblClientVehicleMembershipDetails CVMD
on CVMD.ClientVehicleId=job.VehicleId
ANd Cvmd.MembershipId=mj.MembershipId

Update Jp SET Jp.MembershipId=Cvm.ClientMembershipId from StriveCarSalon.tblJobPayment jp
join #ClientVehicleMembership CVM
on CVM.JobPaymentId=Jp.JobPaymentId

Update jpd SET Jpd.PAymentType= @MembershippaymentId 
from StriveCarSalon.tblJobPaymentDetail jpd
join #ClientVehicleMembership CVM
on CVM.JobPaymentId=jpd.JobPaymentId
ANd Jpd.PaymentType=@AccountpaymentId