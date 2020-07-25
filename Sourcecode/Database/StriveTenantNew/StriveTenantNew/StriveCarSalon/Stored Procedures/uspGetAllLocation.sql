
CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocation]
AS
BEGIN
SELECT 
LocationId           
,LocationType         
,LocationName
,LocationDescription
,IsFranchise
,L.IsActive
,TaxRate
,SiteUrl
,Currency
,Facebook
,Twitter
,Instagram
,WifiDetail
,WorkhourThreshold

,AddressId      AS LocationAddress_AddressId      
,RelationshipId	AS LocationAddress_LocationAddressId	
,Address1		AS LocationAddress_Address1		
,Address2		AS LocationAddress_Address2		
,PhoneNumber	AS LocationAddress_PhoneNumber	
,PhoneNumber2	AS LocationAddress_PhoneNumber2	
,Email			AS LocationAddress_Email			
,City			AS LocationAddress_City			
,[State]		AS LocationAddress_State		
,Zip			AS LocationAddress_Zip			


 from StriveCarSalon.tblLocation L LEFT JOIN StriveCarSalon.tblLocationAddress LA on L.LocationId = LA.RelationshipId

END