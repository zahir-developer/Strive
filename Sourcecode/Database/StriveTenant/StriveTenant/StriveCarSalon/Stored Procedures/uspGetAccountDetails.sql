CREATE Procedure [StriveCarSalon].[uspGetAccountDetails] --'649592'
@TicketNumber varchar(10)

as begin
Declare @CurrentDate DateTime 

Set @CurrentDate=(SELECT CAST(GETDATE() as Date))

    Select tbljb.TicketNumber,
	       tblcli.ClientId,
	       --IsAccount,
		   tblcli.ClientType As AccountType,
		   tblcva.CodeValue,
		   tblcvm.MembershipId,

		   CASE  When  CodeValue='Comp' Then tblcli.Amount 
			     When  CodeValue!='Comp' Then .00 end as Amount,
			        
		   CASE When  CodeValue='Regular' Then 0
		        When  CodeValue='Comp' Then 1  
		        When  CodeValue='Monthly' and tblcvm.MembershipId != 0 Then 1 ELSE 0 End as IsAccount 
				

		   From tblJob tbljb 
		   Inner Join tblClient tblcli ON tbljb.ClientId = tblcli.ClientId
		   Left Join tblClientVehicle tblcv ON tblcli.ClientId = tblcv.ClientId
		   Left Join tblClientVehicleMembershipDetails tblcvm ON tblcv.VehicleId = tblcvm.ClientVehicleId
		   Left Join  tblCodeValue tblcva ON tblcli.ClientType = tblcva.id
		   Left Join  tblCodeCategory tblcc ON tblcva.CategoryId = tblcc.id
		   Where tblcc.Category ='ClientType' and tbljb.TicketNumber=@TicketNumber --and tblcvm.StartDate<= @CurrentDate and tblcvm.EndDate>=@CurrentDate 

End