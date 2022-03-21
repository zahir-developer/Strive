-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2022-02-04
-- Description:	Returns list Client Details
-- Sample: uspGetAllClientDetail 'a'
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllClientDetail] 
@Name NVARCHAR(100)
AS
BEGIN

Select FirstName, LastName, PhoneNumber, ca.Email from tblClient c
LEFT JOIN tblClientAddress ca on ca.clientId = c.ClientId 
WHERE FirstName like  @Name +'%' OR LastName like @Name +'%' order by FirstName


END