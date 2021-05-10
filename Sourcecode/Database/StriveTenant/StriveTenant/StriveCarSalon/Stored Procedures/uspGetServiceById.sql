
CREATE PROCEDURE [StriveCarSalon].[uspGetServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    SELECT ServiceId,ServiceName,ServiceType,LocationId,Cost,Price,Commision,CommisionType,
	       Upcharges,ParentServiceId,IsActive,CommissionCost
		   FROM  [tblService] 
           WHERE ServiceId = @tblServiceId AND IsDeleted=0
END
