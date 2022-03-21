CREATE PROCEDURE [StriveCarSalon].[uspGetAllAdSetup]
as
begin
select tblad.AdSetupId
      ,tblad.DocumentId
      ,tblad.Name
      ,tblad.Description
      ,tblad.IsActive Status
      ,tblad.IsDeleted
	  ,tbld.FileName as Image
	  ,tblad.LaunchDate
	   FROM [tblAdSetup] tblad 
	   inner join tbldocument tbld on tblad.documentid= tbld.documentid 
where tblad.IsDeleted =0 
end