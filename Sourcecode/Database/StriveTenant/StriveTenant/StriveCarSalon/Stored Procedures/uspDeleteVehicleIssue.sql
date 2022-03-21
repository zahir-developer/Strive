
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2022-02-4
-- Description:	Delete Vehicle Issue and Issue Images
-- =============================================
-- =====================History=================
-- =============================================
-- <Modified Date>, <Author> - <Description>

------------------------------------------------
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspDeleteVehicleIssue] 
@VehicleIssueId INT
AS
BEGIN

	DECLARE @_VehicleIssueId INT = @VehicleIssueId;

	DECLARE @IsDeleted INT = 1;
	
	Update tblVehicleIssue set IsDeleted = @IsDeleted where VehicleIssueId = @VehicleIssueId

	Update tblVehicleIssueImage set IsDeleted = @IsDeleted where VehicleIssueId = @VehicleIssueId

END