
CREATE procedure [StriveLimoSalon].[uspSaveEmployee]
@tvpEmployee tvpEmployee READONLY
AS 
BEGIN
MERGE  [StriveLimoSalon].[tblEmployee] TRG
USING @tvpEmployee SRC
ON (TRG.EmployeeId = SRC.EmployeeId)
WHEN MATCHED 
THEN

UPDATE SET TRG.FirstName = SRC.FirstName, TRG.LastName = SRC.LastName, TRG.[Role] = SRC.[Role]

WHEN NOT MATCHED  THEN

INSERT (FirstName, LastName, [Role])
VALUES (SRC.FirstName, SRC.LastName, SRC.[Role]);

END