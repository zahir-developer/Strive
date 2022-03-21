--==============================================================
---------------------------History------------------------------
--==============================================================
-- 2022-02-15, Zahir Hussain - Added condition for Wash tickets
--==============================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAccountDetails] 
@TicketNumber varchar(10),
@LocationId INT = NULL
AS
begin

--EXEC [StriveCarSalon].uspGetAccountDetails 'MSM594250',1

--DECLARE @TicketNumber varchar(10) = 'MSM594114', @LocationId INT = 1
Declare @CurrentDate DateTime 



Set @CurrentDate=(SELECT CAST(GETDATE() as Date))

DROP TABLE IF EXISTS #ClientAccountDetail

declare @rolledBackAmount DECIMAL(18,2), @CreditAmount DECIMAL(18,2)

Declare @WashJobType VARCHAR(10) = 'Wash'

SET @WashJobType = 
(Select top 1 CodeValue FROM tblJob tblj 
JOIN tblCodeValue cv on cv.id = tblj.JobType 
WHERE tblj.TicketNumber = @TicketNumber and tblj.LocationId = @LocationId and cv.CodeValue = @WashJobType)

IF @WashJobType IS NOT NULL
BEGIN


select @CreditAmount = SUM(isnull(CAH.Amount,0))
FROM tblJob tbljb 
Join tblClient tblcli ON tbljb.ClientId = tblcli.ClientId
JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tbljb.ClientId AND CAH.IsDeleted = 0
WHERE tbljb.TicketNumber=@TicketNumber and (tbljb.LocationId = @LocationId OR @LocationId = 0 OR @LocationId is NULL)
and(CAH.IsDeleted = 0 and tbljb.IsDeleted = 0)


Select tblcli.Clientid, tbljb.vehicleId, tbljb.TicketNumber,
(isnull(tblcli.Amount,0) + ISNULL(@CreditAmount,0)) Amount,
Tblcli.IsCreditAccount into #ClientAccountDetail From tblJob tbljb 
LEFT Join tblClient tblcli ON tbljb.ClientId = tblcli.ClientId
where tbljb.TicketNumber=@TicketNumber and (tbljb.LocationId = @LocationId OR @LocationId = 0 OR @LocationId is NULL) and tbljb.IsActive = 1
--GROUP BY tblcli.Clientid, tbljb.vehicleId, tbljb.TicketNumber,
--tblcli.Amount ,Tblcli.IsCreditAccount

Select * from #ClientAccountDetail

    Select tbljb.TicketNumber,
		   tblcv.VehicleId,
	       --IsAccount,
		   tblcvm.MembershipId,
		   tblcvm.LocationId,
		   tblcvm.ClientMembershipId,
		   tblcvm.ClientVehicleId
		   --tblcv.IsMembershipAvalable

		   From #ClientAccountDetail tbljb
		   INNER Join tblClientVehicle tblcv ON tblcv.VehicleId = tbljb.VehicleId
		   LEFT Join tblClientVehicleMembershipDetails tblcvm ON tblcv.VehicleId = tblcvm.ClientVehicleId and tblcvm.isDeleted =0
		   --Left Join  tblCodeCategory tblcc ON tblcva.CategoryId = tblcc.id
		   --Where 
		   --tblcc.Category ='ClientType' and 
		   --tbljb.TicketNumber in (@TicketNumber) 

		   --group by
		   --TicketNumber,tblcli.ClientId,tblcv.VehicleId,tblcli.ClientType,tblcva.CodeValue,tblcvm.MembershipId

		   
		    --and tblcvm.StartDate<= @CurrentDate and tblcvm.EndDate>=@CurrentDate 

		END
End