-- =============================================
-- Author:		shalini
-- Create date: 14-12-2020
-- Description:	Get All LocationOffset
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocationOffset]
AS 
BEGIN
SELECT tbllo.LocationOffsetId,
tbllo.LocationId,
tbllo.OffSet1,
tbllo.OffSet1On,
tbllo.OffSetA,
tbllo.OffSetB,
tbllo.OffSetB,
tbllo.OffSetC,
tbllo.OffSetD,
tbllo.OffSetE,
tbllo.OffSetF	   
FROM [StriveCarSalon].[tblLocationOffset] tbllo
 Order By tbllo.LocationId desc
END