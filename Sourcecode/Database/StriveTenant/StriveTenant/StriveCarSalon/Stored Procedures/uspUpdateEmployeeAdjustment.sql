CREATE  procedure [StriveCarSalon].[uspUpdateEmployeeAdjustment]  --75,'85.00' 
(@EmployeeId int, @Adjustment decimal)    
AS    
-- =============================================================    
-- Author:  Vineeth.B    
-- Create date: 24-08-2020    
-- Description: To update Adjustment for Respective EmployeeId's    
-- =============================================================    
BEGIN        
DECLARE @LiabilityCategoryId INT  
DECLARE @COUNT int;
Declare @LiabilityId int
Declare @AdjusmentId int
Declare @EmpLiablityId int
  
			 --Set @Count=(SELECT Count(tblCV.id) FROM tblCodeCategory tblCC JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 --Inner Join tblEmployeeLiabilityDetail tblel ON tblCV.id = tblel.LiabilityDetailType
			 --Inner Join tblEmployeeLiability tble ON tblel.LiabilityId = tble.LiabilityId
			 --where  tblCC.Category='LiabilityType' AND tblCV.CodeValue='Adjustment' and tble.EmployeeId=@EmployeeId)
			 ----select Count(*) From tblEmployeeLiabilityDetail where LiabilityId=@EmployeeId)

			 Set @Count=(Select Count(1) from tblEmployeeLiability tblli Inner Join tblEmployeeLiabilityDetail tblcEmpDe ON tblli.LiabilityId = tblcEmpDe.LiabilityId
			 where LiabilityType=102 and EmployeeId=@EmployeeId)

 If @Count = 0 
         begin
		    
			--DECLARE @CountLia int;
   --     set @CountLia = (select Count(1) from StriveCarSalon.tblEmployeeLiability  where EmployeeId=1 and LiabilityType=102)
		   

		     SELECT @LiabilityId=tble.LiabilityId FROM tblCodeCategory tblCC JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 Inner Join tblEmployeeLiability tble ON tble.LiabilityType = tblCV.id
			 where  tblCC.Category='LiabilityType' AND tblCV.CodeValue='Adjustment' and tble.EmployeeId=@EmployeeId

			 SELECT @AdjusmentId=tblCV.id FROM tblCodeCategory tblCC 
			 JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 where  tblCC.Category='LiabilityDetailType' AND tblCV.CodeValue='Adjustment' 
			


             Insert into tblEmployeeLiability(EmployeeId,LiabilityType) values 
			             (@EmployeeId,102)
             
			 --select * from tblEmployeeLiability order by LiabilityId desc

             --Select Top(1) @EmpLiablityId=[EmployeeId] from tblEmployeeLiability  order by EmployeeId desc

		     Insert into tblEmployeeLiabilityDetail(LiabilityId,LiabilityDetailType,Amount,PaymentType) values 
			             (SCOPE_IDENTITY(),@AdjusmentId,@Adjustment,1)  				  
		 end
else
 begin
    UPDATE tbleld set tbleld.Amount=@Adjustment FROM tblEmployeeLiability tblel INNER JOIN
    tblEmployeeLiabilityDetail tbleld ON(tblel.LiabilityId = tbleld.LiabilityId) 
	Where tblel.employeeid=@EmployeeId ---and tblel.LiabilityType=@LiabilityCategoryId  
 end
END