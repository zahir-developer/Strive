CREATE proc [StriveCarSalon].[uspGetAllAdSetup]
as
begin
select tblad.AdSetupId
      ,tblad.DocumentId
      ,tblad.Name
      ,tblad.Description
      ,tblad.IsActive Status
      ,tblad.IsDeleted
      ,tblad.CreatedBy
      ,tblad.CreatedDate
      ,tblad.UpdatedBy
      ,tblad.UpdatedDate
      ,tblad.UpdatedDate
	  ,tbld.FileName as Image
	   FROM [StriveCarSalon].[tblAdSetup] tblad 
	   inner join tbldocument tbld on tblad.documentid= tbld.documentid 
where tblad.IsDeleted =0 
end