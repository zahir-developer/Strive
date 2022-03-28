CREATE PROCEDURE [StriveCarSalon].[uspGetAllDeals] 
@DealId INT = NULL
as 
begin
	select 
	tbld.DealId,
	tbld.DealName,
	tbld.TimePeriod,
	tbld.StartDate,
	tbld.EndDate,
	tbld.Deals
	 from tblDeal tbld
	 where tbld.IsActive = 1 
	 and ISNULL(tbld.IsDeleted,0) = 0 


	
end