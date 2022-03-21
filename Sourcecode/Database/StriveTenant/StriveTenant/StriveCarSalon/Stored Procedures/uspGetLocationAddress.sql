
-- =============================================================
-- Author:         Zahir Hussain M
-- Created date:   2021-08-11
-- Description:    Retrieves location lat. and lon.
-- Example: uspGetLocationAddress 1
-- =============================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetLocationAddress] 
    (
     @locationId int)
AS 
BEGIN

Select top 1 Latitude, Longitude from tblLocationAddress where locationId = @locationId

END