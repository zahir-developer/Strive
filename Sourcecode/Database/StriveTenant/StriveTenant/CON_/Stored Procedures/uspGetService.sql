



CREATE PROCEDURE [CON].[uspGetService]
as
begin
select ServiceId,ServiceName,ServiceType,LocationId,Cost,Commision,CommissionCost,CommisionType,Upcharges,
       ParentServiceId,IsActive from [CON].tblService
	   where isDeleted=0
end
