-- =============================================
-- Author:		Zahir Hussain 
-- Create date: 2021-Sep-16
-- Description:	Add Client vehicle if not exists
-- Sample: uspAddClientVehicle 1, 'T7HYT5', 45, 998, NULL, 2
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspAddDriveUpVehicle]
	@LocationId INT,
	@Barcode NVARCHAR(20),
	@Make INT = NULL,
	@Model INT = NULL,
	@Color INT = NULL,
	@CreatedBy INT = NULL
	AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @VehicleCount INT;
	Select @VehicleCount = count(1) from tblClientVehicle where barcode = @Barcode

	IF @VehicleCount = 0
	BEGIN
	INSERT INTO tblClientVehicle (LocationId, Barcode, VehicleMfr, VehicleModel, VehicleColor, IsActive, IsDeleted, CreatedBy, CreatedDate) 
						  VALUES (@locationId, @Barcode, @Make, @model, @Color, 1, 0, @CreatedBy, GETDATE())

	END
END