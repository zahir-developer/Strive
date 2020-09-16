CREATE PROCEDURE [StriveCarSalon].[uspDeleteCollision]
    (
     @tvpEmployeeLiabilityId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblEmployeeLiability] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
	UPDATE [StriveCarSalon].[tblEmployeeLiabilityDetail] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
END
