-- ========================================================
-- Author:         Vineeth.B
-- Created date:   2020-07-01
-- Description:    Delete Location Details by LocationId
-- ========================================================

---------------------------History-------------------------
-- ========================================================
-- 16-09-2020,     Vineeth - Add tblDrawer and tblBay table
-----------------------------------------------------------
-- ========================================================

CREATE PROCEDURE [StriveCarSalon].[uspDeleteLocation]
(
@LocationId int,@UserId int, @Date datetimeoffset
)
AS 
BEGIN

UPDATE [StriveCarSalon].[tblLocation] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblLocationAddress] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblDrawer] set
IsDeleted=1,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblBay] set
IsDeleted=1,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

END
