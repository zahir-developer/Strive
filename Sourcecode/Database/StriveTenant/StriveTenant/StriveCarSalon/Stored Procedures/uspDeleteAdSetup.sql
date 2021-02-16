-- =============================================
-- Author:		shalini
-- Create date: 2020-12-21
-- Description:	delete's the adsetup
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspDeleteAdSetup]
(
@AdSetupId int
)
AS 
BEGIN

UPDATE [StriveCarSalon].[tblAdsetup] SET
  IsDeleted=1 
   WHERE AdSetupId = @AdSetupId	

END