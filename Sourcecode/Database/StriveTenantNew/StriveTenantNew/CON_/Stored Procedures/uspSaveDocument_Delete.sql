











CREATE procedure [CON].[uspSaveDocument_Delete]
@tvpDocument tvpDocument READONLY

AS 
BEGIN
MERGE  [CON].[tblDocument] TRG
USING @tvpDocument SRC
ON (TRG.DocumentId = SRC.DocumentId)
WHEN MATCHED 
THEN
UPDATE SET TRG.EmployeeId=SRC.EmployeeId, TRG.FileName=SRC.FileName, TRG.FilePath=SRC.FilePath, TRG.Password=SRC.Password, 
      TRG.CreatedDate=SRC.CreatedDate, TRG.ModifiedDate = SRC.ModifiedDate,
	  TRG.IsActive = SRC.IsActive
WHEN NOT MATCHED  THEN

INSERT (EmployeeId,FileName,FilePath,Password,CreatedDate,ModifiedDate,IsActive)
 VALUES (SRC.EmployeeId,SRC.FileName,SRC.FilePath,SRC.Password,SRC.CreatedDate,SRC.ModifiedDate,SRC.IsActive);

END