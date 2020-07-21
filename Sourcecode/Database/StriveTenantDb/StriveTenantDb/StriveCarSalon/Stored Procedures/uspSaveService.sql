
CREATE procedure [StriveCarSalon].[uspSaveService]
@tvpService tvpService READONLY
AS 
BEGIN
MERGE  [StriveCarSalon].[tblService] TRG
USING @tvpService SRC
ON (TRG.ServiceId = SRC.ServiceId)
WHEN MATCHED 
THEN

UPDATE SET TRG.ServiceName = SRC.ServiceName, TRG.ServiceType = SRC.ServiceType, 
TRG.LocationId = SRC.LocationId, TRG.Cost = SRC.Cost, TRG.Commision = SRC.Commision,
TRG.CommisionType = SRC.CommisionType, TRG.Upcharges = SRC.Upcharges, TRG.ParentServiceId = SRC.ParentServiceId, TRG.IsActive = SRC.IsActive,
TRG.DateEntered = SRC.DateEntered

WHEN NOT MATCHED  THEN

INSERT (ServiceName, ServiceType, LocationId,Cost,Commision,CommisionType,Upcharges,ParentServiceId,IsActive,DateEntered)
VALUES (SRC.ServiceName, SRC.ServiceType, SRC.LocationId,SRC.Cost,SRC.Commision,SRC.CommisionType,SRC.Upcharges,SRC.ParentServiceId,SRC.IsActive,DateEntered);

END