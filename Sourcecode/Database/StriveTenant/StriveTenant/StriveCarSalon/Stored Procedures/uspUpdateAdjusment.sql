﻿CREATE PROCEDURE [StriveCarSalon].[uspUpdateAdjusment]
@LiabilityId int,
@Amount decimal
as
Begin

Update StriveCarSalon.tblEmployeeLiabilityDetail set Amount=@Amount where LiabilityId=@LiabilityId

End