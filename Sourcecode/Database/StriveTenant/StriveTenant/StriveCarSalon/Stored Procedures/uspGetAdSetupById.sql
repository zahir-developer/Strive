CREATE proc [StriveCarSalon].[uspGetAdSetupById]--[StriveCarSalon].[uspGetAdSetupById] 4
@AdSetupid int
as
begin
select tblad.AdSetupId
      ,tblad.DocumentId
      ,tblad.Name
      ,tblad.Description
      ,tblad.IsActive
      ,tblad.IsDeleted
      ,tblad.CreatedBy
      ,tblad.CreatedDate
      ,tblad.UpdatedBy
      ,tblad.UpdatedDate
	  ,tbld.FileName as Image,
	  tbld.OriginalFileName
	   FROM [StriveCarSalon].[tblAdSetup] tblad 
	   inner join tbldocument tbld on tblad.documentid= tbld.documentid 
 
where tblad.AdSetupId =@AdSetupId and
tblad.IsActive=1 
end