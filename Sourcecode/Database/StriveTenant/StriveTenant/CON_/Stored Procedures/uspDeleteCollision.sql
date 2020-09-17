
CREATE PROCEDURE [CON].[uspDeleteCollision]
    (
     @tvpEmployeeLiabilityId int)
AS 
BEGIN
    UPDATE [CON].[tblEmployeeLiability] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
	UPDATE [CON].[tblEmployeeLiabilityDetail] 
    SET IsDeleted=1 WHERE LiabilityId = @tvpEmployeeLiabilityId
END
