create PROCEDURE [StriveCarSalon].[uspGetServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    SELECT ServiceId,ServiceName,ServiceType,LocationId,Cost,Commision,CommisionType,
	       Upcharges,ParentServiceId,IsActive,DateEntered,CommissionCost
		   FROM  [StriveCarSalon].[tblService] 
           WHERE ServiceId = @tblServiceId
END