-- =============================================
-- Author:		Vineeth B
-- Create date: 05-09-2020
-- Description:	To show data in DetailsGrid
-- =============================================
---------------------History--------------------
-- =============================================
-- 07-09-2020, Vineeth - Add a new select for bay
--						 Add bayid col in 2dselect
-- 08-09-2020, Vineeth - Add service type name col
-- 09-09-2020, Vineeth - Add isactive condition
-- 09-10-2020, Vineeth - Add LocationId condition
-- 17-09-2020, Vineeth - Add valuedesc-Details
-- 21-09-2020, Vineeth - Add Outside service cond
--						 and order by condition
------------------------------------------------
-- =============================================
CREATE proc [StriveCarSalon].[uspGetAllDetails] 
(@JobDate Date,@LocationId int)
AS
BEGIN

SELECT
DISTINCT
tblb.BayId
,tblb.BayName
FROM 
tblJobDetail tbljd
right join tblBay tblb ON(tbljd.BayId = tblb.BayId)
where 
tblb.IsActive=1  
and tbljd.IsActive=1
and ISNULL(tblb.IsDeleted,0)=0
and ISNULL(tbljd.IsDeleted,0)=0
and tblb.LocationId=@LocationId
order by tblb.BayId




SELECT 
tblb.BayId,
tblb.BayName
,tblj.JobId 
,tblj.TicketNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6) AS TimeIn
,CONCAT(tblc.FirstName,'',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6) AS EstimatedTimeOut
,tbls.ServiceName
,st.valuedesc AS ServiceTypeName
FROM 
tblJob tblj inner join tblClient tblc ON(tblj.ClientId = tblc.ClientId) 
inner join tblJobDetail tbljd ON(tblj.JobId = tbljd.JobId)
inner join tblClientAddress tblca ON(tblj.ClientId = tblca.ClientId)
inner join tblJobItem tblji ON(tblj.JobId = tblji.JobId)
inner join tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
right join tblBay tblb ON(tbljd.BayId = tblb.BayId)
inner join GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType)
WHERE 
(tblj.JobDate is null OR tblj.JobDate=@JobDate)
and 
(tblj.LocationId is null OR tblj.LocationId=@LocationId)
and
st.valuedesc='Details' 
or
st.valuedesc='Outside Services'
and
tblj.IsActive=1
and
tbljd.IsActive=1
and
tblji.IsActive=1
and
tblb.IsActive=1
and
ISNULL(tblb.IsDeleted,0)=0
and
ISNULL(tblj.IsDeleted,0)=0
and
ISNULL(tbljd.IsDeleted,0)=0
and
ISNULL(tblji.IsDeleted,0)=0
order by tblj.JobId
END
GO

