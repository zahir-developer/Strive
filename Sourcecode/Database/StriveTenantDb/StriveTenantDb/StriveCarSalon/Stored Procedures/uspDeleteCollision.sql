CREATE PROCEDURE [StriveCarSalon].[uspDeleteCollision]
    (
     @tvpEmployeeLiabilityId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblEmployeeLiability] 
    SET IsActive=0 WHERE LiabilityId = @tvpEmployeeLiabilityId
	UPDATE [StriveCarSalon].[tblEmployeeLiabilityDetail] 
    SET IsActive=0 WHERE LiabilityId = @tvpEmployeeLiabilityId
END