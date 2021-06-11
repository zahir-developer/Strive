-- ================================================
-- Author:		Shalini
-- Create date: 10-June-2021
-- Description:	Retrieve data for email blast
-- ================================================
-------------------------------------------------------
--[StriveCarSalon].[uspGetClientList] '2021-04-01','2021-06-30',1
CREATE PROCEDURE [StriveCarSalon].[uspGetClientList]
@fromDate date,
@toDate date,
@IsMemebership bit =null
AS
BEGIN

DROP TABLE IF EXISTS #GetClinetList
SELECT 
tblc.ClientId,
tblc.FirstName,
tblc.LastName,
tblca.email,
tblc.IsActive,
tblc.ClientType,
ct.valuedesc AS Type,
tblca.PhoneNumber AS PhoneNumber,
tblca.Address1,
cv.VehicleId,
cv.Barcode,
cvm.ClientMembershipId,
case when Isnull(cvm.ClientMembershipId,0)=0  then 0 Else 1 end as IsMembership
into #GetClinetList
FROM [tblClient] tblc 
     left join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 left join GetTable('ClientType') ct ON tblc.ClientType = ct.valueid
	 left join tblClientVehicle cv on cv.ClientId =tblc.ClientId	
	 left join tblClientVehicleMembershipDetails cvm on cv.VehicleId= cvm.ClientVehicleId 
WHERE tblc.CreatedDate between @fromDate and @toDate 
and
ISNULL(tblc.IsDeleted,0) = 0   AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 

if (@IsMemebership is null)
select * from #GetClinetList
else
select * from #GetClinetList cl where cl.IsMembership =@IsMemebership
END