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

CREATE PROCEDURE [StriveCarSalon].[uspDeleteLocation] --1, 1, '2021-03-10 17:05:03.3166667 +00:00', 0
(
@LocationId int, @UserId int, @Date datetimeoffset, @IsDeleted BIT = 1
)
AS 
BEGIN


UPDATE [StriveCarSalon].[tblLocation] set
IsDeleted = @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblLocationAddress] set
IsDeleted = @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblDrawer] set
IsDeleted= @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblBay] set
IsDeleted= @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblLocationOffset] SET
IsDeleted = @IsDeleted
WHERE LocationId = @LocationId	

END
