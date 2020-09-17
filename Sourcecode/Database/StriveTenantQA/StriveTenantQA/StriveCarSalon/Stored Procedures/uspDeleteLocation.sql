


CREATE PROCEDURE [StriveCarSalon].[uspDeleteLocation]
(
@LocationId int,@UserId int, @Date datetimeoffset
)
AS 
BEGIN

UPDATE [StriveCarSalon].[tblLocationAddress] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [StriveCarSalon].[tblLocation] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

END
