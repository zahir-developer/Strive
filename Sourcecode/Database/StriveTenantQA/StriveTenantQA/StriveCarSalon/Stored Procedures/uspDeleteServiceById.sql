﻿



CREATE PROCEDURE [StriveCarSalon].[uspDeleteServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblService] 
    SET IsDeleted=1 WHERE ServiceId = @tblServiceId
END
