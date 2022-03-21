CREATE PROCEDURE [StriveCarSalon].[uspGetAllMake] 
as 
-- =============================================================
-- Author:         Shalini
-- Created date:   2021-04-18
-- Description:    Returns all Vehicle Make
-- =============================================================
begin
	select 
	 MakeId,
	 MakeValue	
	from tblVehicleMake make  
	where ISNULL(IsDeleted,0) =0
	ORDER BY MakeValue
	
end