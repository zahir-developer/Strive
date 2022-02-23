-- ================================================
-- Author:		Shalini
-- Create date: 08-June-2021
-- Description:	Retrieve membership discount for vehicle
-- ================================================

---------------------History--------------------
-- =============================================
------------------------------------------------
--[StriveCarSalon].[uspGetMemberShipDiscount] 17956, 12601
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipDiscount] 
(@ClientId int, @VehicleId INT = NULL)

AS
BEGIN

DECLARE @VehicleCount INT
DECLARE @NonDiscountedCount int
DECLARE @MembershipCount int
DECLARE @DiscountedCount int
DECLARE @IsMembership bit
DECLARE @IsDiscount bit

DROP TABLE IF EXISTS #Vehicles

select VehicleId into #Vehicles from tblClientVehicle where ClientId=@ClientId and IsActive=1 and ISNULL(IsDeleted,0)=0				

select @VehicleCount = count(VehicleId) from #Vehicles 

set @MembershipCount  = (select Count(1) from tblClientVehicleMembershipDetails 
                where IsActive=1 and (ClientVehicleId != @VehicleId OR @VehicleId is NULL) AND
				ISNULL(IsDeleted,0)=0
				and ClientVehicleId in (select VehicleId from #Vehicles))

Select CASE 

WHEN @MembershipCount = 0 THEN 0 -- No discount for 1st Membership
WHEN @MembershipCount = 1 THEN 1 -- Discount for 2nd Vehicle on 2nd Membership 
WHEN @MembershipCount = 2 THEN 1 -- Discount for 3rd Vehicle on 3rd Membership 

WHEN @MembershipCount = 3 THEN 0 -- No discount for 4th Membership
WHEN @MembershipCount = 4 THEN 1 -- Discount for 5th Vehicle on 5nd Membership 
WHEN @MembershipCount = 5 THEN 1 -- Discount for 6th Vehicle on 6th Membership 

WHEN @MembershipCount = 6 THEN 0 -- No discount for 7th Membership
WHEN @MembershipCount = 7 THEN 1 -- Discount for 8th Vehicle on 5nd Membership 
WHEN @MembershipCount = 8 THEN 1 -- Discount for 9th Vehicle on 6th Membership 

WHEN @MembershipCount = 9 THEN 0 -- No discount for 10th Membership
WHEN @MembershipCount = 10 THEN 1 -- Discount for 11th Vehicle on 5nd Membership 
WHEN @MembershipCount = 11 THEN 1 -- Discount for 12th Vehicle on 6th Membership 

--WHEN (@MembershipCount * 2 >= (@VehicleCount)) and @MembershipCount ! = 0 THEN 1 
END as IsDiscount

/*
set @DiscountedCount  = (select Count(1) from tblClientVehicleMembershipDetails 
                where ISNULL(IsDiscount,1) = 1 and IsActive=1 and ISNULL(IsDeleted,0)=0
				and ClientVehicleId in (select VehicleId from #Vehicles))

IF((@DiscountedCount <= (@NonDiscountedCount * 2)) and @NonDiscountedCount ! = 0)
SET @IsDiscount = 1
ELSE
SET @IsDiscount = 0

select @IsDiscount as IsDiscount
*/
END