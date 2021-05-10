CREATE PROCEDURE [StriveCarSalon].[uspDeleteCollision]
    (
     @tvpEmployeeLiabilityId int)
AS 
BEGIN
    UPDATE [tblEmployeeLiability] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
	UPDATE [tblEmployeeLiabilityDetail] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
END
