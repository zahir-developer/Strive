-- =============================================    
-- Author:  Juki B  
-- Create date: 2022-Mar-22    
-- Description: Update barcode for client vehicle if it barcode is not available    
-- Sample: uspUpdateVehicleBarcode 1, 'T7HYT5', 45, 998  
-- =============================================    
CREATE PROCEDURE StriveCarSalon.uspUpdateVehicleBarcode  
 @LocationId INT,    
 @Barcode NVARCHAR(20),     
 @VehicleId INT,  
 @CreatedBy INT = NULL    
 AS    
BEGIN    
 SET NOCOUNT ON;    
    
 DECLARE @VehicleCount INT;    
 Select @VehicleCount = count(1) from tblClientVehicle where barcode = @Barcode    
    
 IF @VehicleCount = 0    
 BEGIN    
 UPDATE tblClientVehicle SET Barcode = @Barcode, UpdatedBy = @CreatedBy, UpdatedDate = GETDATE() where VehicleId = @VehicleId;    
 END    
END 