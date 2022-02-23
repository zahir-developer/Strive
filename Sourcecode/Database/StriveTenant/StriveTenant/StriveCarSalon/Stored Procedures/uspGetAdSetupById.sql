--[StriveCarSalon].[uspGetAdSetupById] 4
CREATE PROCEDURE [StriveCarSalon].[uspGetAdSetupById]
@AdSetupid int
as
begin
select tblad.AdSetupId
,tblad.DocumentId
,tblad.Name
,tblad.Description
,tblad.IsActive
,tblad.IsDeleted

,tbld.FileName as Image,
tbld.OriginalFileName

,tblad.LaunchDate
 FROM [tblAdSetup] tblad 
 inner join tbldocument tbld on tblad.documentid= tbld.documentid 

where tblad.AdSetupId =@AdSetupId
end