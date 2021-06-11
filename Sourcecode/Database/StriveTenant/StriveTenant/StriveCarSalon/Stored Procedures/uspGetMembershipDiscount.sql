-- ================================================
-- Author:		Shalini
-- Create date: 08-June-2021
-- Description:	Retrieve memnership discount for vehicle
-- ================================================

---------------------History--------------------
-- =============================================
------------------------------------------------
--[StriveCarSalon].[uspGetMemberShipDiscount] 55543
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipDiscount] 
(@ClientId int)

AS
BEGIN

DECLARE @NonDiscountedCount int
DECLARE @DiscountedCount int
DECLARE @IsMembership bit
DECLARE @IsDiscount bit

select VehicleId into #Vehicles from tblClientVehicle where ClientId=@ClientId and IsActive=1 and ISNULL(IsDeleted,0)=0				

set @NonDiscountedCount  = (select Count(1) from tblClientVehicleMembershipDetails 
                where IsDiscount = 0 and IsActive=1 and ISNULL(IsDeleted,0)=0
				and ClientVehicleId in (select VehicleId from #Vehicles))

set @DiscountedCount  = (select Count(1) from tblClientVehicleMembershipDetails 
                where IsDiscount = 1 and IsActive=1 and ISNULL(IsDeleted,0)=0
				and ClientVehicleId in (select VehicleId from #Vehicles))

IF((@NonDiscountedCount * 2) <= @DiscountedCount)
SET @IsDiscount = 1
ELSE
SET @IsDiscount = 0


select @IsDiscount as IsDiscount

END