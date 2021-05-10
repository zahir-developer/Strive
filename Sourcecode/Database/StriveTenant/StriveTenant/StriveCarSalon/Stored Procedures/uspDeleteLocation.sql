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
@LocationId int, @UserId int, @Date datetimeoffset, @IsDeleted BIT = 1
)
AS 
BEGIN


UPDATE [tblLocation] set
IsDeleted = @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [tblLocationAddress] set
IsDeleted = @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [tblDrawer] set
IsDeleted= @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

UPDATE [tblBay] set
IsDeleted= @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate=@Date
WHERE LocationId = @LocationId

UPDATE [tblLocationOffset] SET
IsDeleted = @IsDeleted
WHERE LocationId = @LocationId	

END
