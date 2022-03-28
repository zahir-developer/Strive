
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
CREATE PROCEDURE [StriveCarSalon].[uspGetAllBayListById]
(@LocationId int)
AS
BEGIN
Select BayId,BayName FROM [tblBay] WHERE IsActive=1 AND LocationId = @LocationId AND IsDeleted = 0
END
