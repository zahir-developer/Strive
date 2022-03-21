-- ================================================
-- Author:		Vineeth B
-- Create date: 17-09-2020
-- Description:	To get All Service And Product List
-- ================================================
-- 16-06-2021, Shalini -removed wildcard from Query

------------------------------------------------

CREATE PROCEDURE [StriveCarSalon].[uspGetAllServiceAndProductList] 
@LocationId int = NULL ,
@Query Varchar(100) =Null
AS
BEGIN

SELECT 
ServiceId
,ServiceName
,ServiceType
,LocationId
,Cost
,Commision
,CommisionType
,Upcharges
,ParentServiceId
,CommissionCost
,IsActive
,IsDeleted
,CreatedBy
,CreatedDate
,UpdatedBy
,UpdatedDate
,Price
 FROM tblService WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0 
 --and (locationid =@LocationId OR @LocationId IS NULL OR @LocationId = 0)
 and (@Query is null or ServiceName like @Query+'%' )
 

SELECT 
ProductId
,ProductName
,ProductCode
,ProductDescription
,ProductType
,FileName
,ThumbFileName
,LocationId
,VendorId
,Size
,SizeDescription
,Quantity
,QuantityDescription
,Cost
,Price
,IsTaxable
,TaxAmount
,ThresholdLimit
,IsActive
,IsDeleted
,CreatedBy
,CreatedDate
,UpdatedBy
,UpdatedDate

FROM tblProduct WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0 
and (locationid =@LocationId OR @LocationId IS NULL OR @LocationId = 0)
and (@Query is null or ProductName like @Query+'%' )

END