-- ================================================  
-- Author:  Vineeth B  
-- Create date: 17-08-2020  
-- Description: Returns all jobs for checkout screen  
-- Example:  EXEC [StriveCarSalon].[uspGetAllCheckOutDetails] 1, '', 1, 100, 'ASC', NULL, '2022-01-27', '2022-01-27'  
-- ================================================  
  
-- ================================================  
-- ---------------History--------------------------  
-- ================================================  
--24-03-2021 - Zahir - Query optimized by reusing job and jobitem tables  
--23-04-2021 - Zahir - JOB Status join changed to Left from Inner.  
--05-05-2021 - Zahir - tblVehicleMake/Model table used instead of tblCodevalue table.  
--14-05-2021 - Zahir - Changed tblClient/tblClientVehicle INNER JOIN to Left Join. (Services added from sales won't have client information)   
--04-06-2021 - Zahir - Unk added for Model/Make/Color if empty.   
--06-06-2021 - Zahir - Overall SP Optimized, Few Search conditions removed.   
--20-07-2021 - Vetriselvi - Included all Service Type .   
--22-07-2021 - Vetriselvi - Remove the extra none from additional services  
--29-07-2021 - Vetriselvi - Included null values too while checking for deleted, table - ClientVehicleMembershipDetail   
--09-08-2021 - Vetriselvi - Removed the distinct keyword from checkout calculation  
--16-08-2021 - Zahir - Make/Model/Color taken from Job table instead of the clientVehicle table  
--14-09-2021 - Vetriselvi - Added Tips,Products and tax in cost  
--13-10-2021 - Zahir - JobType, JobStatus, IsHold added. 
--17-12-2021 - Lakshmana - TipAmount removed from pay amount field - #1415
--08-03-2022 - Juki - Fixed duplicate record
  
  
  
CREATE PROCEDURE [StriveCarSalon].[uspGetAllCheckOutDetails]  
@locationId int,  
@Query NVARCHAR(50) = NULL,  
@PageNo INT = NULL,  
@PageSize INT = 100,   
@SortOrder VARCHAR(5) = 'ASC',  
@SortBy VARCHAR(100) = NULL,  
@StartDate date = NULL,   
@EndDate date = NULL  
AS  
BEGIN  
  
SET NOCOUNT ON;  
  
DECLARE @Skip INT = 0;  
IF @PageSize is not NULL  
BEGIN  
SET @Skip = @PageSize * (@PageNo-1);  
END  
  
IF @PageSize is NULL  
BEGIN  
SET @PageSize = (Select count(1) from tblJob (NOLOCK) where isDeleted = 0 and CheckOut = 0);  
SET @PageNo = 1;  
SET @Skip = @PageSize * (@PageNo-1);  
Print @PageSize  
Print @PageNo  
Print @Skip  
END  
  
  
  
--DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')  
--DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')  
--Collision,Uniform,Adjusment   
  
DROP TABLE IF EXISTS #GetAllServices  
  
DROP TABLE IF EXISTS #NeedOfPayment  
  
DROP TABLE IF EXISTS #JobType  
Select valueid, valuedesc into #JobType from GetTable('JobType')  
  
DROP TABLE IF EXISTS #ServiceType  
Select valueid, valuedesc into #ServiceType from GetTable('ServiceType')  
  
  
DROP TABLE IF EXISTS #JobStatus  
Select valueid, valuedesc into #JobStatus from GetTable('JobStatus')  
  
DROP TABLE IF EXISTS #VehicleColor  
Select valueid, valuedesc into #VehicleColor from GetTable('VehicleColor')  
  
DROP TABLE IF EXISTS #PaymentStatus  
Select valueid, valuedesc into #PaymentStatus from GetTable('PaymentStatus')  
  
  
DROP TABLE IF EXISTS #Jobs  
  
Select tblj.JobId,   
tblj.JobPaymentId,  
tblj.TicketNumber,   
tblji.JobItemId,   
tblji.ServiceId,   
tblji.Price,  
tblj.VehicleId,  
tblj.ClientId,  
tblj.Make,  
tblj.Model,  
tblj.Color,  
tblj.TimeIn,  
tblj.EstimatedTimeOut,  
tblj.JobType,  
tblj.JobStatus,  
tblj.CheckOut,  
tblj.IsHold  
into #Jobs from tbljob tblj  
INNER JOIN tblJobItem tblji (NOLOCK) on tblj.JobId = tblji.JobId  
INNER JOIN tblService tbls (NOLOCK) on tbls.ServiceId = tblji.ServiceId  
INNER JOIN #ServiceType st (NOLOCK) ON(tbls.ServiceType = st.valueid)  
where ( @locationId is null or tblj.LocationId = @locationId) AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0  
AND ISNULL(tblji.IsDeleted,0)=0  
AND (tblj.JobDate between @StartDate and @EndDate or (@StartDate is NULL and @EndDate is Null))  
AND ISNULL(tblj.CheckOut,0)=0   
  
  
SELECT DISTINCT tblj.JobId--,tblji.ServiceId ServiceUsed,tblcvmds.ServiceId ServicePurchased   
,CASE WHEN tblcvmds.ServiceId IS NULL THEN 'Y'  
WHEN tblj.ServiceId = tblcvmds.ServiceId THEN 'N'  
END AS PaymentNeed  
INTO #NeedOfPayment  
FROM #Jobs tblj   
INNER JOIN tblClientVehicleMembershipDetails tblcvmd (NOLOCK)  
ON tblj.VehicleId = tblcvmd.ClientVehicleId AND tblcvmd.IsActive =1 AND ISNULL(tblcvmd.IsDeleted,0)=0   
INNER JOIN tblMembership tblm (NOLOCK) ON tblcvmd.MembershipId = tblm.MembershipId AND tblm.IsActive=1 AND ISNULL(tblm.IsDeleted,0)=0  
--INNER JOIN tblJobItem tblJi ON tblj.JobId = tblji.JobId AND tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0  
LEFT JOIN tblClientVehicleMembershipService tblcvmds (NOLOCK) ON tblcvmds.ServiceId = tblj.ServiceId AND tblcvmds.IsActive=1 AND ISNULL(tblcvmds.IsDeleted,0)=0  
--where ( @locationId is null or tblj.LocationId = @locationId )  
-- and (tblj.JobDate between @StartDate and @EndDate or( @StartDate is NULL and @EndDate is Null))  
ORDER BY tblj.JobId  
  
DROP TABLE IF EXISTS #PaymentDo  
SELECT JobId,PaymentNeed,ROW_NUMBER() OVER (PARTITION BY JobId ORDER BY PaymentNeed desc)  
NeedToPay INTO #PaymentDo FROM #NeedOfPayment    
  
DROP TABLE IF EXISTS #PaymentNeedToDone  
SELECT JobId,PaymentNeed INTO #PaymentNeedToDone FROM #PaymentDo WHERE NeedToPay=1  
  
  
-- Product List    
DROP TABLE IF EXISTS #JobProductList    
    
SELECT     
 tbljb.JobId,    
   
 SUM(CAST((ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0))*((ISNULL(tblp.TaxAmount,0)/100)) AS DECIMAL(18,2))) as TaxAmount,  
-- ((ISNULL(tbljbP.Quantity,0)) * ISNULL(tblp.TaxAmount,0) /100) AS TaxAmount,    
 SUM((ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0))) AS Cost    
INTO    
 #JobProductList    
FROM     
 tblJob tbljb     
 JOIN     
 tblJobProductItem tbljbP     
ON  tbljb.JobId = tbljbP.JobId      
 JOIN    
 tblProduct tblp    
ON  tblp.ProductId=tbljbP.ProductId    
LEFT JOIN     
 tblCodeValue tblCV     
ON  tblP.ProductType = tblcv.id    
WHERE  (tbljb.JobDate between @StartDate and @EndDate or (@StartDate is NULL and @EndDate is Null)) AND (tbljb.LocationId = @LocationId OR @LocationId IS NULL)    
AND ISNULL(tbljbP.IsDeleted,0)=0     
AND ISNULL(tbljbP.IsActive,1)=1     
AND ISNULL(tbljb.IsDeleted,0)=0     
GROUP BY tbljb.JobId  
  
--DROP TABLE IF EXISTS #TipsAmount  
--SELECT j.JobId,SUM(ISNULL(pd.Amount,0)) TipsAmount  
--into #TipsAmount  
--From tblJob j   
--join tblJobPayment p on j.JobPaymentId = p.JobPaymentId  
--JOIN tblJobPaymentDetail pd on pd.JobPaymentId = p.JobPaymentId  
--join tblCodeValue cv on cv.id = pd.PaymentType  
--WHERE (j.JobDate between @StartDate and @EndDate ) AND cv.CodeValue = 'Tips'  
----AND p.PaymentStatus=@CompletedPaymentStatus  
----AND j.JobStatus=@CompletedJobStatus   
--and ISNULL(p.IsRollBack,0) != 1  
--GROUP BY j.JobId  
  
drop table if exists #checkout  
SELECT   
tblj.JobId,  
js.valuedesc,  
tblj.JobPaymentId,  
tblj.TicketNumber,  
tblj.IsHold,  
tblc.FirstName AS CustomerFirstName,  
tblc.LastName AS CustomerLastName,  
ISNULL(vm.MakeValue, 'Unk') AS VehicleMake,  
ISNULL(vmo.ModelValue, 'Unk') AS VehicleModel,  
ISNULL(vc.valuedesc, 'Unk') AS VehicleColor,  
CONCAT(ISNULL(vm.MakeValue, 'Unk'),'/',ISNULL(vmo.ModelValue, 'Unk'),'/',ISNULL(vc.valuedesc, 'Unk')) AS VehicleDescription,  
tbls.ServiceName,  
st.valuedesc AS ServiceTypeName,  
CASE WHEN st.valuedesc='Additional Services' THEN TRIM(tbls.ServiceName) END AS AdditionalServices,  
CASE WHEN st.valuedesc !='Additional Services' THEN TRIM(tbls.ServiceName) END AS [Services],  
(ISNULL(tblj.Price,0)-- + ISNULL(pl.Cost,0) + ISNULL(pl.TaxAmount,0)  
) Price,  
CONVERT(VARCHAR(5),tblj.TimeIn,108) AS Checkin,  
CONVERT(VARCHAR(5),tblj.EstimatedTimeOut,108) AS Checkout,  
ISNULL(tblm.MembershipName,'') AS MembershipName,  
ISNULL(ps.valuedesc,'') AS PaymentStatus,  
CASE  
 WHEN tblj.IsHold = 1 THEN '#00BFFF' -- TO SHOW PAID  
 WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc='Completed' AND pntd.PaymentNeed ='Y' THEN '#FF1493'-- TO SHOW MEMBERSHIP NAME  
 WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Completed' THEN '#008000'   
 WHEN tblj.TimeIn !='' AND js.valuedesc!='Hold' AND st.valuedesc='Additional Services' and (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) THEN '#FFA500'  
 WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFA500'-- TO SHOW MEMBERSHIP NAME  
 WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc='Completed' AND pntd.PaymentNeed ='N' THEN '#008000'-- TO SHOW MEMBERSHIP NAME  
 WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc NOT IN('Completed','Hold')  THEN '#FF1493'-- TO SHOW MEMBERSHIP NAME  
 --WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFFF00'-- TO SHOW MEMBERSHIP NAME  
 WHEN ps.valuedesc ='Success' AND st.valuedesc IN('Additional Services') AND tblm.MembershipName IS NULL AND js.valuedesc NOT IN('Completed','Hold') THEN '#FFA500'  
 WHEN ps.valuedesc ='Success' AND js.valuedesc='Completed' THEN '#008000' -- TO SHOW PAID  
 WHEN ps.valuedesc ='Success' AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493' -- TO SHOW PAID  
 WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493'   
 WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Hold' THEN '#00BFFF'   
 WHEN tblj.TimeIn !='' AND (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc !='Hold' THEN '#FF1493'  
 WHEN tblm.MembershipName IS NULL THEN ''  
END AS ColorCode,  
CASE  
    WHEN ps.valuedesc = 'Success' AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 THEN 'Paid'  
 WHEN ps.valuedesc = 'Success' AND tbljp.IsProcessed=0 AND tbljp.IsRollBack=1 THEN ''  
 WHEN (ps.valuedesc != 'Success' OR ps.valuedesc IS NULL) THEN js.valuedesc  
END AS MembershipNameOrPaymentStatus,  
CASE    
    WHEN js.valuedesc='Completed' THEN 1  
 WHEN js.valuedesc='In Progress' THEN 2  
 WHEN js.valuedesc='Waiting' THEN 3  
 --WHEN js.valuedesc='Hold' THEN 4  
END AS JobStatusOrder,  
js.valuedesc as JobStatus,  
jt.valuedesc as JobType  
INTO   
 #Checkout  
FROM   
 #Jobs tblj WITH(NOLOCK)  
INNER JOIN  
 tblService tbls  WITH(NOLOCK) ON(tblj.ServiceId = tbls.ServiceId)  
INNER JOIN #ServiceType st ON(tbls.ServiceType = st.valueid)  
INNER JOIN #JobType jt ON(tblj.JobType = jt.valueid)  
LEFT JOIN #JobStatus js ON(tblj.JobStatus = js.valueid)  
LEFT JOIN  
 tblClient tblc  WITH(NOLOCK) ON(tblj.ClientId = tblc.ClientId)  
LEFT JOIN  
 tblClientVehicle tblcv  WITH(NOLOCK) ON(tblj.VehicleId = tblcv.VehicleId)  
LEFT JOIN   
 tblVehicleMake vm (NOLOCK) ON(tblj.Make = vm.MakeId)  
LEFT JOIN  
 tblVehicleModel vmo (NOLOCK) ON(tblj.Model = vmo.ModelId) and vm.MakeId = vmo.MakeId  
LEFT JOIN #VehicleColor vc ON(tblj.Color = vc.valueid)  
LEFT JOIN  
    tblClientVehicleMembershipDetails tblcvmd  WITH(NOLOCK)  
    ON tblcv.VehicleId = tblcvmd.ClientVehicleId AND  ISNULL(tblcvmd.IsActive,1) = 1 AND ISNULL(tblcvmd.IsDeleted,0) = 0  
    --AND tblcvmd.StartDate<=CAST(getdate() AS Date)  
    --AND tblcvmd.EndDate>=CAST(getdate() AS Date)  
LEFT JOIN  
 tblMembership tblm  WITH(NOLOCK) ON(tblm.MembershipId = tblcvmd.MembershipId) AND ISNULL(tblm.IsActive, 1) = 1 AND ISNULL(tblm.IsDeleted,0)=0  
LEFT JOIN  
 tblJobPayment tbljp  WITH(NOLOCK) ON tblj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0   
LEFT JOIN #PaymentStatus ps ON(tbljp.PaymentStatus = ps.valueid)  
LEFT JOIN   
    #PaymentNeedToDone pntd ON(tblj.JobId = pntd.JobId)  
LEFT JOIN   
    #JobProductList pl ON(tblj.JobId = pl.JobId)   
WHERE   
--( @locationId is null or tblj.LocationId =@locationId )   
--and (tblj.JobDate  between @StartDate and @EndDate or( @StartDate is NULL and @EndDate is Null)) and  
--AND tblj.IsActive = 1   
--AND tblc.IsActive = 1 AND tblcv.IsActive = 1   
--AND tblji.IsActive = 1   
--AND tbls.IsActive = 1    
--AND ISNULL(tblj.IsDeleted,0) = 0 AND   
--AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0   
--AND ISNULL(tbls.IsDeleted,0) = 0   
 ISNULL(tblcvmd.IsDeleted,0) = 0 and  
(@Query is null) OR (tblj.JobId like @Query  
--OR tblj.TimeIn like +@Query+'%'  
--OR tblj.EstimatedTimeOut like +@Query+'%'  
OR tblm.MembershipName like +@Query+'%'  
--OR ps.valuedesc like +@Query+'%'  
OR tblc.FirstName like +@Query+'%'  
OR tblc.LastName like +@Query+'%'  
--OR vm.MakeValue like +@Query+'%'  
--OR vmo.ModelValue like +@Query+'%'  
--OR vc.valuedesc like +@Query+'%'   
OR tbls.ServiceName  like +@Query+'%'  
--OR CONCAT(vm.MakeValue,' ',vmo.valuedesc,'/',vc.valuedesc) like '%'+@Query+'%'   
OR CONCAT_WS(' ',tblc.FirstName,tblc.LastName) like '%'+@Query+'%')  
  
  
ORDER BY js.valuedesc   
  
ALTER Table #Checkout ALTER COLUMN ColorCode Varchar(20)  
UPDATE #Checkout SET ColorCode= CASE WHEN ServiceTypeName='Additional Services' THEN '1-'+ColorCode ELSE '2-'+ColorCode END  
  
SELECT   
tmp.JobId,  
valuedesc,  
JobPaymentId,  
tmp.TicketNumber,  
CustomerFirstName,  
CustomerLastName,  
VehicleMake,  
VehicleModel,  
VehicleColor,  
IsHold,  
VehicleDescription,  
   
ISNULL(STUFF(  
   (SELECT  ', ' + AdditionalServices  
    FROM #Checkout C  
 WHERE C.JobId= tmp.JobID   
 and  ISNULL(AdditionalServices,'') != ''  
    FOR XML PATH('')  
 ), 1, 2, ''), 'None')   AS AdditionalServices,  
STUFF(  
   (SELECT DISTINCT', ' + [Services]  
    FROM #Checkout C1  
 WHERE C1.JobId= tmp.JobID  
    FOR XML PATH('')  
 ), 1, 2, '')  AS [Services]  
 , (SUM(Price)+ISNULL(pl.Cost,0) + ISNULL(pl.TaxAmount,0)) AS Cost
-- + ISNULL(ta.TipsAmount,0))   
 , Checkin  
 , Checkout  
 , MembershipName  
 , PaymentStatus  
 ,STUFF(MIN(ColorCode),1,2,'') AS ColorCode  
 , MembershipNameOrPaymentStatus  
 ,JobStatusOrder  
 ,JobStatus  
 ,JobType  
 into #GetAllServices  
 FROM   
 #Checkout tmp   
 LEFT JOIN #JobProductList pl ON(tmp.JobId = pl.JobId)   
 --LEFT JOIN #TipsAmount ta ON tmp.JobId = ta.JobId  
GROUP BY    
Tmp.JobId,  
Tmp.valuedesc,  
Tmp.JobPaymentId,  
tmp.TicketNumber,  
Tmp.CustomerFirstName,  
Tmp.CustomerLastName,  
VehicleDescription,  
Tmp.Checkin,  
tmp.IsHold,  
VehicleMake,  
VehicleModel,  
VehicleColor,  
Tmp.Checkout,  
Tmp.MembershipName,  
Tmp.PaymentStatus,  
Tmp.MembershipNameOrPaymentStatus,  
JobStatusOrder,  
JobStatus,  
JobType,  
pl.Cost, pl.TaxAmount
--ta.TipsAmount  
   
order by   
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='ASC' THEN tmp.TicketNumber END ASC,  
CASE WHEN @SortBy = 'CustomerFirstName' AND @SortOrder='ASC' THEN tmp.CustomerFirstName END ASC,  
  
CASE WHEN @SortBy = 'CustomerLastName' AND @SortOrder='ASC' THEN tmp.CustomerLastName END ASC,  
CASE WHEN @SortBy = 'CheckIn' AND @SortOrder='ASC' THEN Checkin END ASC,  
CASE WHEN @SortBy = 'CheckOut' AND @SortOrder='ASC' THEN Checkout END ASC,  
  
CASE WHEN @SortBy = 'MembershipName' AND @SortOrder='ASC' THEN MembershipName END ASC,  
  
CASE WHEN @SortBy = 'MembershipNameOrPaymentStatus' AND @SortOrder='ASC' THEN MembershipNameOrPaymentStatus END ASC,  
  
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,  
----DESC  
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='DESC' THEN tmp.TicketNumber END DESC,  
CASE WHEN @SortBy = 'CheckIn' AND @SortOrder='DESC' THEN Checkin END DESC,  
CASE WHEN @SortBy = 'CheckOut' AND @SortOrder='DESC' THEN Checkout END DESC,  
CASE WHEN @SortBy = 'MembershipName' AND @SortOrder='DESC' THEN MembershipName END DESC,  
CASE WHEN @SortBy = 'CustomerFirstName' AND @SortOrder='DESC' THEN CustomerFirstName END DESC,  
CASE WHEN @SortBy = 'CustomerLastName' AND @SortOrder='DESC' THEN CustomerLastName END DESC,  
  
CASE WHEN @SortBy = 'MembershipNameOrPaymentStatus' AND @SortOrder='DESC' THEN MembershipNameOrPaymentStatus END DESC,  
  
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN tmp.jobid END DESC,  
  
CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN tmp.jobId END ASC  
  
OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY  
  
select * from #GetAllServices  
  
IF ( @Query IS NULL OR @Query = ' ' ) AND (@StartDate IS NOT NULL AND @EndDate IS NOT NULL)  
BEGIN   
  
Select count(1) as Count from tbljob j (NOLOCK) where j.jobDate Between @StartDate and @EndDate and ( @locationId is null or j.LocationId = @locationId) AND j.IsActive=1 AND ISNULL(j.IsDeleted,0)=0   
  
END  
ELSE  
IF (@Query IS Not NULL AND @Query != ' ') OR (@StartDate IS NOT NULL AND @EndDate IS NOT NULL)  
BEGIN  
SELECT   
DISTINCT count(tblj.JobId)  
FROM   
 #Jobs tblj WITH(NOLOCK)  
INNER JOIN  
 tblService tbls WITH(NOLOCK) ON(tblj.ServiceId = tbls.ServiceId)  
INNER JOIN #ServiceType st ON(tbls.ServiceType = st.valueid)  
LEFT JOIN tblClient tblc WITH(NOLOCK) ON(tblj.ClientId = tblc.ClientId)  
LEFT JOIN  
 tblClientVehicle tblcv WITH(NOLOCK) ON(tblj.VehicleId = tblcv.VehicleId)  
LEFT JOIN  
    tblClientVehicleMembershipDetails tblcvmd WITH(NOLOCK)  
    ON tblcv.VehicleId = tblcvmd.ClientVehicleId AND  ISNULL(tblcvmd.IsActive,1) = 1 AND ISNULL(tblcvmd.IsDeleted,0) = 0  
LEFT JOIN  
 tblMembership tblm WITH(NOLOCK) ON(tblm.MembershipId = tblcvmd.MembershipId) AND ISNULL(tblm.IsActive, 1) = 1 AND ISNULL(tblm.IsDeleted,0)=0  
WHERE   
--( @locationId is null or tblj.LocationId =@locationId )   
--and (tblj.JobDate  between @StartDate and @EndDate or( @StartDate is NULL and @EndDate is Null)) and  
 --AND tblj.IsActive = 1   
--AND tblc.IsActive = 1 AND tblcv.IsActive = 1   
--AND tblji.IsActive = 1   
--AND tbls.IsActive = 1    
--AND ISNULL(tblj.IsDeleted,0) = 0 AND   
--AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0   
--AND ISNULL(tbls.IsDeleted,0) = 0   
tblcvmd.IsDeleted = 0 and  
((@Query is null) OR (tblj.JobId like @Query  
--OR tblj.TimeIn like +@Query+'%'  
--OR tblj.EstimatedTimeOut like +@Query+'%'  
OR tblm.MembershipName like +@Query+'%'  
--OR ps.valuedesc like +@Query+'%'  
OR tblc.FirstName like +@Query+'%'  
OR tblc.LastName like +@Query+'%'  
--OR vm.MakeValue like +@Query+'%'  
--OR vmo.ModelValue like +@Query+'%'  
--OR vc.valuedesc like +@Query+'%'   
OR tbls.ServiceName  like +@Query+'%'  
--OR CONCAT(vm.MakeValue,' ',vmo.valuedesc,'/',vc.valuedesc) like '%'+@Query+'%'   
  
OR CONCAT_WS(' ',tblc.FirstName, tblc.LastName) like +@Query+'%'))  
  
END  
  
END  
