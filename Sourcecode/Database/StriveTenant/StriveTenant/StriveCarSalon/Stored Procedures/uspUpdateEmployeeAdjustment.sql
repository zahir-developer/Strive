


--[StriveCarSalon].[uspUpdateEmployeeAdjustment] 1495,'99.00' 
CREATE  procedure [StriveCarSalon].[uspUpdateEmployeeAdjustment] 
(@EmployeeId int, @Adjustment decimal(9,2))    
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
 DECLARE @AdjustmentLiabilityId INT =(SELECT valueid FROM GetTable('LiabilityType') WHERE valuedesc='Adjustment');
  
			 --Set @Count=(SELECT Count(tblCV.id) FROM tblCodeCategory tblCC JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 --Inner Join tblEmployeeLiabilityDetail tblel ON tblCV.id = tblel.LiabilityDetailType
			 --Inner Join tblEmployeeLiability tble ON tblel.LiabilityId = tble.LiabilityId
			 --where  tblCC.Category='LiabilityType' AND tblCV.CodeValue='Adjustment' and tble.EmployeeId=@EmployeeId)
			 ----select Count(*) From tblEmployeeLiabilityDetail where LiabilityId=@EmployeeId)

			 Set @Count=(Select Count(1) from tblEmployeeLiability tblli
			  Inner Join tblEmployeeLiabilityDetail tblcEmpDe ON tblli.LiabilityId = tblcEmpDe.LiabilityId
			  inner join GetTable('LiabilityType') gt on tblli.LiabilityType =gt.valueid and gt.valuedesc ='Adjustment'
			 where EmployeeId=@EmployeeId)

 If @Count = 0 
         begin
		    
    
			--DECLARE @CountLia int;
   --     set @CountLia = (select Count(1) from StriveCarSalon.tblEmployeeLiability  where EmployeeId=1 and LiabilityType=102)
		   

		     SELECT @LiabilityId=tble.LiabilityId FROM tblCodeCategory tblCC JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 Inner Join tblEmployeeLiability tble ON tble.LiabilityType = tblCV.id
			 where  tblCC.Category='LiabilityType'  and tble.EmployeeId=@EmployeeId

			 SELECT @AdjusmentId=tblCV.id FROM tblCodeCategory tblCC 
			 JOIN tblCodeValue tblCV ON tblcc.id=tblcv.CategoryId 
			 where  tblCC.Category='LiabilityDetailType' AND tblCV.CodeValue='Adjustment' 
			


             Insert into tblEmployeeLiability(EmployeeId,LiabilityType,IsActive,IsDeleted) values 
			             (@EmployeeId,@AdjustmentLiabilityId,1,0)
             
			 --select * from tblEmployeeLiability order by LiabilityId desc

             --Select Top(1) @EmpLiablityId=[EmployeeId] from tblEmployeeLiability  order by EmployeeId desc

		     Insert into tblEmployeeLiabilityDetail(LiabilityId,LiabilityDetailType,Amount,PaymentType,IsActive,IsDeleted) values 
			             (SCOPE_IDENTITY(),@AdjusmentId,@Adjustment,1,1,0)  				  
		 end
else
 begin
    UPDATE tbleld set tbleld.Amount=@Adjustment,tbleld.IsActive=1,tbleld.IsDeleted=0 
	FROM tblEmployeeLiability tblel INNER JOIN
    tblEmployeeLiabilityDetail tbleld ON(tblel.LiabilityId = tbleld.LiabilityId) 
	Where tblel.employeeid=@EmployeeId and tblel.LiabilityType= @AdjustmentLiabilityId ---and tblel.LiabilityType=@LiabilityCategoryId  

	  UPDATE tblel set tblel.IsActive=1,tblel.IsDeleted=0 FROM tblEmployeeLiability tblel INNER JOIN
    tblEmployeeLiabilityDetail tbleld ON(tblel.LiabilityId = tbleld.LiabilityId) 
	Where tblel.employeeid=@EmployeeId and tblel.LiabilityType= @AdjustmentLiabilityId

 end
END