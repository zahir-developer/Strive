
create procedure [CON].[uspSaveCollision]
@tvpEmployeeLiability tvpEmployeeLiability READONLY,
@tvpEmployeeLiabilityDetail tvpEmployeeLiabilityDetail READONLY

AS 
BEGIN

DECLARE @LiabilityId bigint 
--tblemployeeliability
MERGE  [CON].[tblEmployeeLiability] TRG
USING @tvpEmployeeLiability SRC
ON (TRG.LiabilityId = SRC.LiabilityId)
WHEN MATCHED 
THEN
UPDATE SET TRG.EmployeeId=SRC.EmployeeId, TRG.LiabilityType=(select id from StriveCarSalon.tblCodeCategory where Category='liabilitytype'), TRG.LiabilityDescription=SRC.LiabilityDescription, TRG.ProductId=SRC.ProductId, 
      TRG.Status=SRC.Status, TRG.CreatedDate = SRC.CreatedDate, TRG.IsActive = SRC.IsActive
WHEN NOT MATCHED  THEN

INSERT (EmployeeId,LiabilityType,LiabilityDescription,ProductId,Status,CreatedDate,IsActive)
 VALUES (SRC.EmployeeId,(select id from StriveCarSalon.tblCodeCategory where Category='liabilitytype'),SRC.LiabilityDescription,SRC.ProductId,SRC.Status,SRC.CreatedDate,SRC.IsActive);
 SELECT @LiabilityId = scope_identity();
--tblEmployeeLiabilityDetail--
MERGE  [CON].[tblEmployeeLiabilityDetail] TRG
USING @tvpEmployeeLiabilityDetail SRC
ON (TRG.LiabilityDetailId = SRC.LiabilityDetailId)
WHEN MATCHED 
THEN
UPDATE SET TRG.LiabilityId=SRC.LiabilityId, TRG.LiabilityDetailType=(select id from StriveCarSalon.tblCodeCategory where Category='liabilityDetailtype'), TRG.Amount=SRC.Amount, TRG.PaymentType=SRC.PaymentType, 
    TRG.DocumentPath=SRC.DocumentPath, TRG.Description= SRC.Description,TRG.CreatedDate= SRC.CreatedDate,
	TRG.IsActive= SRC.IsActive


WHEN NOT MATCHED THEN

INSERT (LiabilityId, LiabilityDetailType, Amount, PaymentType, DocumentPath,Description,CreatedDate,IsActive)
 VALUES (@LiabilityId,(select id from StriveCarSalon.tblCodeCategory where Category='liabilityDetailtype'), SRC.Amount, SRC.PaymentType, SRC.DocumentPath,SRC.Description,SRC.CreatedDate,SRC.IsActive);

 END