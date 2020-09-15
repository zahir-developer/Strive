
CREATE PROCEDURE [CON].[uspGetAllServiceType]
AS 
BEGIN
    SELECT * FROM  [CON].[tblCodeValue] where CategoryId=3
END
