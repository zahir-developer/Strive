
CREATE PROCEDURE [CON].[uspGet]
AS
BEGIN
SELECT 
 tblEmployee.[EmployeeId]             AS Employee_EmployeeId	
,tblEmployee.[FirstName]              AS Employee_FirstName	
,tblEmployee.[MiddleName]             AS Employee_MiddleName	
,tblEmployee.[LastName]               AS Employee_LastName	
,tblEmployee.[Gender]                 AS Employee_Gender	
,tblEmployee.[SSNo]                   AS Employee_SSNo	
,tblEmployee.[MaritalStatus]          AS Employee_MaritalStatus	
,tblEmployee.[IsCitizen]              AS Employee_IsCitizen	
,tblEmployee.[AlienNo]                AS Employee_AlienNo	
,tblEmployee.[BirthDate]              AS Employee_BirthDate	
,tblEmployee.[ImmigrationStatus]      AS Employee_ImmigrationStatus	
,tblEmployee.[CreatedDate]            AS Employee_CreatedDate	
,tblEmployee.[EmployeeId]             AS Employee_EmployeeId	
,tblEmployee.[FirstName]              AS Employee_FirstName	
,tblEmployee.[MiddleName]             AS Employee_MiddleName	
,tblEmployee.[LastName]               AS Employee_LastName	
,tblEmployee.[Gender]                 AS Employee_Gender	
,tblEmployee.[SSNo]                   AS Employee_SSNo	
,tblEmployee.[MaritalStatus]          AS Employee_MaritalStatus	
,tblEmployee.[IsCitizen]              AS Employee_IsCitizen	
,tblEmployee.[AlienNo]                AS Employee_AlienNo	
,tblEmployee.[BirthDate]              AS Employee_BirthDate	
,tblEmployee.[ImmigrationStatus]      AS Employee_ImmigrationStatus	
,tblEmployee.[CreatedDate]            AS Employee_CreatedDate	
,tblEmployeeAddress.[EmployeeAddressId]       AS EmployeeAddress_EmployeeAddressId	
,tblEmployeeAddress.[EmployeeId]  AS EmployeeAddress_EmployeeId	
,tblEmployeeAddress.[Address1]        AS EmployeeAddress_Address1	
,tblEmployeeAddress.[Address2]        AS EmployeeAddress_Address2	
,tblEmployeeAddress.[PhoneNumber]     AS EmployeeAddress_PhoneNumber	
,tblEmployeeAddress.[PhoneNumber2]    AS EmployeeAddress_PhoneNumber2	
,tblEmployeeAddress.[Email]           AS EmployeeAddress_Email	
,tblEmployeeAddress.[City]            AS EmployeeAddress_City	
,tblEmployeeAddress.[State]           AS EmployeeAddress_State	
,tblEmployeeAddress.[Zip]             AS EmployeeAddress_Zip	
,tblEmployeeAddress.[IsActive]        AS EmployeeAddress_IsActive	
,tblEmployeeAddress.[EmployeeId]  AS EmployeeAddress_EmployeeId	
,tblEmployeeAddress.[Address1]        AS EmployeeAddress_Address1	
,tblEmployeeAddress.[Address2]        AS EmployeeAddress_Address2	
,tblEmployeeAddress.[PhoneNumber]     AS EmployeeAddress_PhoneNumber	
,tblEmployeeAddress.[PhoneNumber2]    AS EmployeeAddress_PhoneNumber2	
,tblEmployeeAddress.[Email]           AS EmployeeAddress_Email	
,tblEmployeeAddress.[City]            AS EmployeeAddress_City	
,tblEmployeeAddress.[State]           AS EmployeeAddress_State	
,tblEmployeeAddress.[Zip]             AS EmployeeAddress_Zip	
,tblEmployeeAddress.[IsActive]        AS EmployeeAddress_IsActive	
,tblEmployeeAddress.[Country]         AS EmployeeAddress_Country	
,tblEmployeeDetail.[EmployeeId]       AS EmployeeDetail_EmployeeId	
,tblEmployeeDetail.[EmployeeCode]     AS EmployeeDetail_EmployeeCode	
,tblEmployeeDetail.[AuthId]           AS EmployeeDetail_AuthId	
--,tblEmployeeDetail.[LocationId]       AS EmployeeDetail_LocationId	
,tblEmployeeDetail.[PayRate]          AS EmployeeDetail_PayRate	
,tblEmployeeDetail.[SickRate]         AS EmployeeDetail_SickRate	
,tblEmployeeDetail.[VacRate]          AS EmployeeDetail_VacRate	
,tblEmployeeDetail.[ComRate]          AS EmployeeDetail_ComRate	
,tblEmployeeDetail.[HiredDate]        AS EmployeeDetail_HiredDate	
,tblEmployeeDetail.[Salary]           AS EmployeeDetail_Salary	
,tblEmployeeDetail.[Tip]              AS EmployeeDetail_Tip	
,tblEmployeeDetail.[LRT]              AS EmployeeDetail_LRT	
,tblEmployeeDetail.[Exemptions]       AS EmployeeDetail_Exemptions	
,tblEmployeeDetail.[EmployeeDetailId] AS EmployeeDetail_EmployeeDetailId	
,tblEmployeeDetail.[EmployeeId]       AS EmployeeDetail_EmployeeId	
,tblEmployeeDetail.[EmployeeCode]     AS EmployeeDetail_EmployeeCode	
,tblEmployeeDetail.[AuthId]           AS EmployeeDetail_AuthId	
--,tblEmployeeDetail.[LocationId]       AS EmployeeDetail_LocationId	
,tblEmployeeDetail.[PayRate]          AS EmployeeDetail_PayRate	
,tblEmployeeDetail.[SickRate]         AS EmployeeDetail_SickRate	
,tblEmployeeDetail.[VacRate]          AS EmployeeDetail_VacRate	
,tblEmployeeDetail.[ComRate]          AS EmployeeDetail_ComRate	
,tblEmployeeDetail.[HiredDate]        AS EmployeeDetail_HiredDate	
,tblEmployeeDetail.[Salary]           AS EmployeeDetail_Salary	
,tblEmployeeDetail.[Tip]              AS EmployeeDetail_Tip	
,tblEmployeeDetail.[LRT]              AS EmployeeDetail_LRT	
,tblEmployeeDetail.[Exemptions]       AS EmployeeDetail_Exemptions
FROM 
[CON].[tblEmployee]					WITH (NOLOCK)
LEFT JOIN [CON].[tblEmployeeAddress]	WITH (NOLOCK) ON tblEmployee.EmployeeId=tblEmployeeAddress.EmployeeId
LEFT JOIN [CON].[tblEmployeeDetail]	WITH (NOLOCK) ON tblEmployee.EmployeeId=tblEmployeeDetail.EmployeeId
END