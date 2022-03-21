
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 08 Dec 2021
-- Description:	Update Membership Discount for the Client Vehicles
-- [StriveCarSalon].[uspUpdateMembershipVehicleDiscount] 57409, 21446
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspUpdateMembershipVehicleDiscount]
--DECLARE @ClientId INT = 57409, @VehicleId INT = 214446, @Action NVARCHAR = 'Delete'
@ClientId INT, @VehicleId INT, @Action NVARCHAR = 'Delete' AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @MembershipCount INT;
	
	DECLARE @VehicleCount INT;

	Select @MembershipCount = COUNT(md.ClientMembershipId) from tblClientVehicle v
	INNER JOIN tblClientVehicleMembershipDetails md on v.VehicleId = md.ClientVehicleId 
	and md.IsDeleted = 0 and v.IsDeleted = 0
	where (v.ClientId = @ClientId);

	IF @MembershipCount = 0 OR @MembershipCount <= 0
	BEGIN
	--Do nothing, If Given Vehicle doesn't have a membership
	Print @MembershipCount
	--Print 'Do Nothing'
	--Return;
	END
	ELSE 
	BEGIN

	IF @MembershipCount >= 1
	BEGIN

	Select @MembershipCount = COUNT(md.ClientMembershipId) from tblClientVehicle v
	INNER JOIN tblClientVehicleMembershipDetails md on v.VehicleId = md.ClientVehicleId and md.IsDeleted = 0
	where (VehicleId != @VehicleId OR @VehicleId is NULL);

	Select @VehicleCount = COUNT(v.VehicleId) from tblClientVehicle v
	where v.IsDeleted = 0 and (VehicleId != @VehicleId OR @VehicleId is NULL OR @Action = 'Delete');
	
	Select v.VehicleId from tblClientVehicle v
	INNER JOIN tblClientVehicleMembershipDetails md on v.VehicleId = md.ClientVehicleId and md.IsDeleted = 0
	where VehicleId != @VehicleId and v.ClientId = @ClientId;

	DECLARE @C_VehicleId INT;
	
	--Cursor
	DECLARE C_Vehicle CURSOR FOR 
	
	Select v.VehicleId from tblClientVehicle v
	INNER JOIN tblClientVehicleMembershipDetails md on v.VehicleId = md.ClientVehicleId and md.IsDeleted = 0
	where VehicleId != @VehicleId and v.ClientId = @ClientId;

	DECLARE @Loop INT = 1;

	Print 'Loop:'
	Print @Loop 
	Print ''

	OPEN C_Vehicle
	FETCH NEXT FROM C_Vehicle INTO @C_VehicleId

	
	WHILE @@FETCH_STATUS = 0  
BEGIN  

DECLARE @MembershipDiscount DECIMAL(18,2) = 0;
DECLARE @MembershipPrice DECIMAL(18,2) = 0;
DECLARE @MembershipDiscountedPrice DECIMAL(18,2) = 0;

DECLARE @ClientVehicleMembershipId INT = 0;
DECLARE @MembershipId INT = 0;
DECLARE @VehicelAdditionalServicePrice Decimal(18,2) = 0;


	Print @C_VehicleId

	Select top 1 @MembershipDiscount = (m.Price - m.DiscountedPrice), @MembershipDiscountedPrice = m.DiscountedPrice ,@MembershipPrice = m.Price , @ClientVehicleMembershipId = md.ClientMembershipId, @MembershipId = md.MembershipId  from tblClientVehicleMembershipDetails md 
	JOIN tblMembership m on m.membershipId = md.membershipId 
	where md.IsDeleted = 0 and md.ClientVehicleId = @C_VehicleId
	Print '@MembershipDiscountedPrice'
	Print @MembershipDiscountedPrice

	Print '@MembershipPrice'
	Print @MembershipPrice


	Select @VehicelAdditionalServicePrice = SUM(s.price) from tblClientVehicleMembershipService ms
	JOIN tblService s on s.serviceId = ms.ServiceId
	Where ms.ClientMembershipId = @ClientVehicleMembershipId and
	ms.ServiceId NOT IN (Select ServiceId from tblMembershipService where MembershipId = @MembershipId and IsDeleted = 0) and ms.IsDeleted = 0

	Print '@VehicelAdditionalServicePrice'

	Print @VehicelAdditionalServicePrice

	IF @Loop = 1 OR @Loop = 4 OR @Loop = 7 OR @Loop = 10 
	BEGIN
	
	Print 'No Discount for ' + CONVERT(VARCHAR(10), @C_VehicleId)
	Update tblClientVehicleMembershipDetails set TotalPrice = (ISNULL(@VehicelAdditionalServicePrice,0) + @MembershipPrice), IsDiscount = 0 where ClientMembershipId = @ClientVehicleMembershipId;
	END
	ELSE
	BEGIN

	Print 'Discount for ' + CONVERT(VARCHAR(10), @C_VehicleId)
	Update tblClientVehicleMembershipDetails set TotalPrice = (ISNULL(@VehicelAdditionalServicePrice,0) + @MembershipDiscountedPrice), IsDiscount = 1 where ClientMembershipId = @ClientVehicleMembershipId;
	
	END


	SET @Loop = @Loop + 1

	FETCH next FROM C_Vehicle INTO @C_VehicleId

END

	--END CURSOR

	CLOSE C_Vehicle


	DEALLOCATE C_Vehicle
	END
	END
END