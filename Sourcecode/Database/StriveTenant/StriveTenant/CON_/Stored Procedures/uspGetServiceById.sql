

CREATE PROCEDURE [CON].[uspGetServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    SELECT ServiceId,ServiceName,ServiceType,LocationId,Cost,Commision,CommisionType,
	       Upcharges,ParentServiceId,IsActive,CommissionCost
		   FROM  [CON].[tblService] 
           WHERE ServiceId = @tblServiceId AND IsDeleted=0
END
