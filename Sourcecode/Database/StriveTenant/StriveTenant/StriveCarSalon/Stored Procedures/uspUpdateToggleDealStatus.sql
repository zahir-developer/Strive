Create PROCEDURE [StriveCarSalon].[uspUpdateToggleDealStatus]
(
@Status bit
)
AS 
BEGIN

UPDATE [StriveCarSalon].tblDeal  SET
Deals =@Status

END