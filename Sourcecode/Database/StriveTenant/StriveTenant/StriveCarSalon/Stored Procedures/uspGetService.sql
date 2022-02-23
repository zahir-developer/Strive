CREATE PROCEDURE [StriveCarSalon].[uspGetService]
as
begin
select ServiceId,ServiceName,ServiceType,LocationId,Cost,Commision,CommissionCost,CommisionType,Upcharges,[Description]
       ParentServiceId,IsActive from tblService
	   where isDeleted=0
end
