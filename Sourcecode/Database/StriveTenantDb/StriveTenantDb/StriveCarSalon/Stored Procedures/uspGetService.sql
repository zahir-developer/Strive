
CREATE proc [StriveCarSalon].[uspGetService]
as
begin
select ServiceId,ServiceName,ServiceType,LocationId,Cost,Commision,CommisionType,Upcharges,
       ParentServiceId,IsActive,DateEntered from [StriveCarSalon].tblService
end