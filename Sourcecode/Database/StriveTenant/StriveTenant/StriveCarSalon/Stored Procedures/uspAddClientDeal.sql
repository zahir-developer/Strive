
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 03 DEC 2021
-- Description:	Adds/Update the customer deal
-- EXEC uspAddClientDeal 2, 57400, '2022-12-07'

-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspAddClientDeal]
 
--DECLARE 
@DealId int, 
@ClientId int,
@Date DateTime
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @CustomerDealId INT = 0;
	DECLARE @ResetDeal bit = 0;
	
	DECLARE @DealCount INT;
	DECLARE @EndDate DateTime;
	
	DECLARE @GetNextFree NVARCHAR(50);
	
	DECLARE @BounceBack NVARCHAR(50);
	
	DECLARE @StartDate DateTime;

	Select @CustomerDealId = ClientDealId, @StartDate = StartDate, @EndDate = EndDate, @DealCount = DealCount from tblClientDeal where DealId = @DealId and ClientId = @ClientId and IsDeleted = 0

	Select top 1 @GetNextFree = DealName  from tblDeal where DealId = @DealId and IsDeleted = 0 and UPPER(DealName) like '%GET%NEXT%FREE%'

	Select top 1 @BounceBack = DealName from tblDeal where DealId = @DealId and IsDeleted = 0 and UPPER(DealName) like '%BOUNCE%BACK%'
	
	--Print @CustomerDealId

	IF @CustomerDealId >= 1
	BEGIN

	IF @Date BETWEEN @StartDate AND @EndDate
	BEGIN
		
		
		--GET NEXT FREE
		IF @GetNextFree IS NOT NULL
		BEGIN
			--Print @GetNextFree

			IF @DealCount >= 10
				SET @ResetDeal = 1;
		END
		ELSE IF @BounceBack IS NOT NULL
		BEGIN 
			IF @DealCount >= 5
			SET @ResetDeal = 1;
		END

		--Print @ResetDeal
		IF @ResetDeal = 0
			BEGIN
				--Print 'Updated DealCount'
				Update tblClientDeal SET DealCount = DealCount+1 where ClientDealId = @CustomerDealId
			END
			ELSE IF @ResetDeal = 1
			BEGIN
				Update tblClientDeal SET isDeleted = 1 where ClientDealId = @CustomerDealId
				--Print 'IsDeleted = true'
				SET @CustomerDealId =  0;
				--Print @CustomerDealId
			END

			--Return 1;
	
	END
	END

	IF @Date NOT BETWEEN @StartDate AND @EndDate
		BEGIN
		Update tblClientDeal SET isDeleted = 1 where ClientDealId = @CustomerDealId
		--Print 'IsDeleted = true'
		SET @CustomerDealId =  0;
		--Print @CustomerDealId
	END

	IF @CustomerDealId = 0
	BEGIN

	DECLARE @TimePeriod INT;
	SET @StartDate = @Date;
	
	DECLARE @IntitialDealCount INT = 1;

	Select @TimePeriod = TimePeriod from tblDeal 
	where dealId = @DealId
	
	IF @TimePeriod = 1 
	BEGIN
	SELECT @EndDate = DATEADD(MONTH, 1, @StartDate);
	END
	ELSE
	IF @TimePeriod = 2
	BEGIN
	SELECT @EndDate = DATEADD(YEAR, 1, @StartDate);
	END
	ELSE
	IF @TimePeriod = 3
	BEGIN
	Select @EndDate = EndDate, @StartDate = StartDate from tblDeal where dealId = @DealId
	END

	INSERT INTO tblClientDeal (DealId,ClientId, StartDate, EndDate, DealCount, IsDeleted) values(@DealId, @ClientId, @StartDate, @EndDate, @IntitialDealCount, 0)
	--Print 'New Deal'	
	Return 1;
	END

END