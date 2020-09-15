
-- =============================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To get BayList By Location Id
-- =============================================

---------------------History--------------------
-- =====================================================
-- 09-09-2020, Vineeth - Added IsActive condition
--------------------------------------------------------
-- =====================================================
CREATE PROC [StriveCarSalon].[uspGetAllBayListById]
(@LocationId int)
AS
BEGIN
Select BayId,BayName FROM [StriveCarSalon].[tblBay] WHERE IsActive=1 AND LocationId = @LocationId 
END