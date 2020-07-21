









CREATE procedure [StriveCarSalon].[uspSaveEmployee]
@tvpEmployee tvpEmployee READONLY,
@tvpEmployeeDetail tvpEmployeeDetail READONLY,
@tvpEmployeeAddress tvpEmployeeAddress READONLY,
@tvpEmployeeRole tvpEmployeeRole READONLY

AS 
BEGIN

DECLARE @EmployeeId int 
MERGE  [StriveCarSalon].[tblEmployee] TRG
USING @tvpEmployee SRC
ON (TRG.EmployeeId = SRC.EmployeeId)
WHEN MATCHED 
THEN
UPDATE SET TRG.FirstName=SRC.FirstName, TRG.MiddleName=SRC.MiddleName, TRG.LastName=SRC.LastName, TRG.Gender=SRC.Gender, 
      TRG.SSNo=SRC.SSNo, TRG.MaritalStatus = SRC.MaritalStatus, TRG.IsCitizen = SRC.IsCitizen, TRG.AlienNo = SRC.AlienNo, TRG.BirthDate = SRC.BirthDate, 
	  TRG.ImmigrationStatus = SRC.ImmigrationStatus,TRG.CreatedDate = SRC.CreatedDate, 
	  TRG.IsActive = SRC.IsActive
WHEN NOT MATCHED  THEN

INSERT (FirstName,MiddleName,LastName,Gender,SSNo,MaritalStatus,IsCitizen,AlienNo,BirthDate,ImmigrationStatus,CreatedDate
,IsActive)
 VALUES (SRC.FirstName,SRC.MiddleName,SRC.LastName,SRC.Gender,SRC.SSNo,SRC.MaritalStatus,SRC.IsCitizen,SRC.AlienNo,SRC.BirthDate,SRC.ImmigrationStatus,SRC.CreatedDate,
 SRC.IsActive);
 
 SELECT @EmployeeId = scope_identity();


--tblEmployeeDetail--
MERGE  [StriveCarSalon].[tblEmployeeDetail] TRG
USING @tvpEmployeeDetail SRC
ON (TRG.EmployeeDetailId = SRC.EmployeeDetailId)
WHEN MATCHED 
THEN
UPDATE SET TRG.EmployeeId=SRC.EmployeeId, TRG.EmployeeCode=SRC.EmployeeCode, TRG.AuthId=SRC.AuthId, TRG.LocationId=SRC.LocationId, 
    TRG.PayRate=SRC.PayRate, TRG.SickRate= SRC.SickRate,TRG.VacRate= SRC.VacRate,
	TRG.ComRate= SRC.ComRate,TRG.HiredDate= SRC.HiredDate,TRG.Salary= SRC.Salary,
	TRG.Tip= SRC.Tip,TRG.LRT= SRC.LRT,TRG.Exemptions= SRC.Exemptions, TRG.IsActive = SRC.IsActive


WHEN NOT MATCHED THEN

INSERT (EmployeeId, EmployeeCode, AuthId, LocationId, PayRate,SickRate,VacRate,ComRate,HiredDate,Salary,Tip,LRT,Exemptions,IsActive)
 VALUES (@EmployeeId, SRC.EmployeeCode, SRC.AuthId, SRC.LocationId, SRC.PayRate,SRC.SickRate,SRC.VacRate,SRC.ComRate,SRC.HiredDate,SRC.Salary,SRC.Tip,SRC.LRT,SRC.Exemptions,SRC.IsActive);

 ---tblEmployeeAddress---
MERGE  [StriveCarSalon].[tblEmployeeAddress] TRG
USING @tvpEmployeeAddress SRC
ON (TRG.AddressId = SRC.EmployeeAddressId)
WHEN MATCHED 
THEN
UPDATE SET TRG.RelationshipId=SRC.RelationshipId, TRG.Address1=SRC.Address1, TRG.Address2=SRC.Address2, TRG.PhoneNumber=SRC.PhoneNumber, 
    TRG.PhoneNumber2=SRC.PhoneNumber2, TRG.Email= SRC.Email, TRG.City= SRC.City,TRG.State= SRC.State,
	TRG.Zip= SRC.Zip,TRG.IsActive= SRC.IsActive,TRG.Country= SRC.Country
WHEN NOT MATCHED  THEN
INSERT (RelationshipId, Address1, Address2,PhoneNumber,PhoneNumber2,Email,City,State,Zip,IsActive,Country)
 VALUES (@EmployeeId, SRC.Address1, SRC.Address2,SRC.PhoneNumber,SRC.PhoneNumber2,SRC.Email,SRC.City,SRC.State,SRC.Zip,SRC.IsActive,SRC.Country);

---tblEmployeeRole---
MERGE  [StriveCarSalon].[tblEmployeeRole] TRG
USING @tvpEmployeeRole SRC
ON (TRG.tblEmployeeRoleId = SRC.EmployeeRolesId AND TRG.EmployeeId = SRC.EmployeeId)
WHEN MATCHED 
THEN
UPDATE SET TRG.EmployeeId=SRC.EmployeeId,TRG.RoleId=SRC.RoleId,TRG.IsDefault=SRC.IsDefault, TRG.IsActive=SRC.IsActive
WHEN NOT MATCHED  THEN
INSERT (EmployeeId, RoleId,IsDefault, IsActive)
 VALUES (@EmployeeId, SRC.RoleId,SRC.IsDefault, SRC.IsActive);
END

Select * from StriveCarSalon.tblEmployeeRole