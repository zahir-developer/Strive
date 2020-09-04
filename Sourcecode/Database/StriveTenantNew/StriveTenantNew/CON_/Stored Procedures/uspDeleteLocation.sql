


CREATE PROCEDURE [CON].[uspDeleteLocation]
(
@LocationId int,@UserId int, @Date datetimeoffset
)
AS 
BEGIN

UPDATE [CON].[tblLocationAddress] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

UPDATE [CON].[tblLocation] set
IsDeleted =1,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE LocationId = @LocationId

END