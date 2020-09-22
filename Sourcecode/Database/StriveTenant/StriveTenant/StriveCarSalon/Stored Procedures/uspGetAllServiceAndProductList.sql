-- ================================================
-- Author:		Vineeth B
-- Create date: 17-09-2020
-- Description:	To get All Service And Product List
-- ================================================

CREATE PROCEDURE [StriveCarSalon].uspGetAllServiceAndProductList
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
 FROM tblService WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0;

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
FROM tblProduct WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0;

END