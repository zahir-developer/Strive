﻿CREATE Procedure [StriveCarSalon].[uspUpdateAccountDetails] 
@ClientId int,
@Amount decimal(18,2)
As begin

    Update StriveCarSalon.tblClient set Amount=@Amount where ClientId=@ClientId

End