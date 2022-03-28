CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocationName]
AS 
BEGIN

	select 
	l.LocationId,
	l.LocationName,
	l.IsActive,
	l.IsDeleted
	 from tblLocation l
	 where l.IsActive = 1 
	 and ISNULL(l.IsDeleted,0) = 0 


	
END