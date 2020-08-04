CREATE PROCEDURE uspGetAllServiceType
AS 
BEGIN
    SELECT * FROM  [StriveCarSalon].[tblCodeValue] where CategoryId=3
END