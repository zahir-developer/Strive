
-- ================================================      
-- Author:  Premalatha     
-- Create date: 29-November-2021      
-- Description: Retrieve data for email send     
-- ================================================      
-------------------------------------------------------      
--[StriveCarSalon].[uspGetClientMailList]  '2021-11-29','2021-06-30',1      
CREATE PROCEDURE [StriveCarSalon].[uspGetClientMailList]      
    
AS      
BEGIN      
      
    
SELECT       
tblc.ClientId,  
tblca.ClientAddressId,      
tblc.FirstName,      
tblc.LastName,      
tblca.email,      
tblc.IsActive,      
tblc.ClientType,        
tblca.PhoneNumber AS PhoneNumber,      
tblca.Address1,  
ISNULL(tblca.IsNotified ,0) as IsNotified      
FROM [tblClient] tblc       
     left join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)      
         
WHERE     
ISNULL(tblc.IsDeleted,0) = 0   AND      
isnull(tblc.IsDeleted,0)=0 and       
isnull(tblca.IsDeleted,0)=0  AND 
ISNULL(tblca.IsNotified ,0) = 0    
  AND tblca.email IS NOT NULL    
END