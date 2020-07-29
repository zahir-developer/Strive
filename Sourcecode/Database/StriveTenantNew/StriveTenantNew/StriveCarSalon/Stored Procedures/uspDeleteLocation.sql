
CREATE PROCEDURE [StriveCarSalon].[uspDeleteLocation]
(
@LocationId int,@UserId int, @Date datetimeoffset
)
AS 
BEGIN

UPDATE [StriveCarSalon].[tblLocationAddress] set
IsActive = 0,
IsDeleted =0,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblLocation] set
IsActive = 0,
IsDeleted =0,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

END