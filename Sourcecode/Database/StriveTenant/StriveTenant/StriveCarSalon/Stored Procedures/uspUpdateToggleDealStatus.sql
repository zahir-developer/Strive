CREATE PROCEDURE [StriveCarSalon].[uspUpdateToggleDealStatus]
(
@Status bit
)
AS 
BEGIN

UPDATE tblDeal  SET
Deals =@Status

END