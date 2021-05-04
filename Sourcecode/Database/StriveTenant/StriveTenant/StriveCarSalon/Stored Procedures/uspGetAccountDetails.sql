CREATE Procedure [StriveCarSalon].[uspGetAccountDetails] --[StriveCarSalon].[uspGetAccountDetails]'993306'
@TicketNumber varchar(10)

as begin
Declare @CurrentDate DateTime 

Set @CurrentDate=(SELECT CAST(GETDATE() as Date))



Select tblcli.Clientid,tbljb.TicketNumber,
tblcli.Amount,Tblcli.IsCreditAccount From tblJob tbljb 
Inner Join tblClient tblcli ON tbljb.ClientId = tblcli.ClientId
where  tbljb.TicketNumber=@TicketNumber and tblcli.IsCreditAccount =1


    Select tbljb.TicketNumber,
	       tblcli.ClientId,
		   tblcv.VehicleId,
	       --IsAccount,
		   tblcli.ClientType As AccountType,
		   tblcva.CodeValue as AccountTypeName,
		   tblcvm.MembershipId 
		   --tblcv.IsMembershipAvalable

		   From tblJob tbljb 
		   Inner Join tblClient tblcli ON tbljb.ClientId = tblcli.ClientId
		   INNER Join tblClientVehicle tblcv ON tblcli.ClientId = tblcv.ClientId
		   Left Join tblClientVehicleMembershipDetails tblcvm ON tblcv.VehicleId = tblcvm.ClientVehicleId and tblcvm.isDeleted =0
		   Left Join  tblCodeValue tblcva ON tblcli.ClientType = tblcva.id
		   Left Join  tblCodeCategory tblcc ON tblcva.CategoryId = tblcc.id
		   Where tblcc.Category ='ClientType' and tbljb.TicketNumber in (@TicketNumber) 

		   --group by
		   --TicketNumber,tblcli.ClientId,tblcv.VehicleId,tblcli.ClientType,tblcva.CodeValue,tblcvm.MembershipId

		   
		    --and tblcvm.StartDate<= @CurrentDate and tblcvm.EndDate>=@CurrentDate 

End