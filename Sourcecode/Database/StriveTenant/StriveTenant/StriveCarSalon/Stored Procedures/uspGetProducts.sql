-------------history-----------------
-- =============================================
-- 1  shalini 2021-06-01  -added status in search filter
--[StriveCarSalon].[uspGetProducts]
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetProducts] 
(@ProductSearch varchar(50)=null, @ProductTypeNames varchar(50) = null,@status bit=null)
AS
BEGIN

if @ProductSearch = ''
BEGIN
SET @ProductSearch = NULL
END

SELECT 
	prd.ProductId, 
	prd.ProductName, 
	prd.ProductCode, 
	prd.ProductDescription, 
	prd.ProductType,
	prd.[FileName],
	prd.[ThumbFileName],
	prd.OriginalFileName,
	prd.LocationId, 
	prd.VendorId, 
	prd.Size, 
	prd.SizeDescription, 
	prd.Quantity, 
	prd.QuantityDescription,
	prd.Cost, 
	prd.Price,
	prd.IsTaxable, 
	prd.TaxAmount, 
	prd.IsActive, 
	prd.ThresholdLimit,
	loc.LocationName,
	STUFF((SELECT DISTINCT ', ' + ven.VendorName
    from [tblProductVendor] prodV
	INNER JOIN [tblVendor] ven on ven.VendorId = prodV.VendorId
	WHERE prodV.ProductId = prd.ProductId and prodV.IsDeleted = 0
    FOR XML PATH('')
	), 1, 2, '')  AS VendorName,
	STUFF((SELECT DISTINCT ', ' + CAST(ven.VendorId AS VARCHAR(5))
    from [tblProductVendor] prodV
	INNER JOIN [tblVendor] ven on ven.VendorId = prodV.VendorId
	WHERE prodV.ProductId = prd.ProductId and prodV.IsDeleted = 0
    FOR XML PATH('')
	), 1, 2, '')  AS VendorId,
	STUFF((SELECT DISTINCT ', ' + TRIM(venAdd.PhoneNumber)
    FROM [tblProductVendor] prodV
	INNER JOIN [tblVendorAddress] venAdd on venAdd.VendorId = prodV.VendorId
	WHERE prodV.ProductId = prd.ProductId and prodV.IsDeleted = 0
    FOR XML PATH('')
	), 1, 2, '')  AS VendorPhone,
	tbpt.valuedesc as ProductTypeName,
	tbsz.valuedesc as SizeName
FROM [tblProduct] prd
INNER JOIN [tblLocation] as loc ON (prd.LocationId = loc.LocationId)

--LEFT JOIN [tblVendor] as ven ON (prodven.ProductVendorId = ven.VendorId)
LEFT JOIN [GetTable]('ProductType') tbpt ON (prd.ProductType = tbpt.valueid)
LEFT JOIN [GetTable]('Size') tbsz ON (prd.Size = tbsz.valueid)

WHERE isnull(prd.IsDeleted,0)=0
AND
 ((@ProductSearch is null or prd.ProductName like '%'+@ProductSearch+'%' or loc.LocationName like'%'+@ProductSearch+'%' 
   --or ven.VendorName like '%'+@ProductSearch+'%' 
or tbsz.valuedesc like '%'+@ProductSearch+'%'
or tbpt.valuedesc like '%'+@ProductSearch+'%') AND 
(tbpt.valuedesc IN (@ProductTypeNames) OR (@ProductTypeNames is NULL)) And
(@status is null or prd.IsActive = @status))
 --or prd. like '%'+@LocationSearch+'%' or tblla.Address2 like '%'+@LocationSearch+'%'
 --or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 --or tblla.Email like '%'+@LocationSearch+'%')
 Order by ProductId DESC
END
