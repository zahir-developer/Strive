
--=================================================
--=====================History=====================
--=================================================
--2022-02-21 - Zahir H - Changed logic for Get Opening & Current Balance 


--=================================================
--EXEC uspGetCreditAccountBalance 41477, 2, 0
--=================================================
CREATE PROCEDURE [StriveCarSalon].[uspGetCreditAccountBalance]     
@ClientId varchar(10),
@month INT,
@year INT
as    
begin    

--DECLARE @ClientId INT = 57418, @Month INT = 2, @Year INT = 2022

IF @Year = 0 
BEGIN
SET @Year = YEAR(GETDATE())
END

    
declare @ActivityAmount DECIMAL(18,2)=0, @OpeningBalance DECIMAL(18,2)=0;

DECLARE @OpeningBalanceMonth INT = @Month
	
DECLARE @OpeningBalanceYear INT = @Year 

	
IF @Month = 1 
	BEGIN
	SET @OpeningBalanceMonth = 12
	SET @OpeningBalanceYear = @Year - 1;
END
ELSE IF @Month > 1
BEGIN

SET @OpeningBalanceMonth = @Month - 1

END


IF(@year > 0)
BEGIN

DECLARE @ClientBalance DECIMAL(18,2) = (Select Amount from tblClient where ClientId = @ClientId)

DROP TABLE IF EXISTS #CreditHistory

	select CAH.CreditAccountHistoryId, CAH.Amount, CAH.CreatedDate, CAH.UpdatedDate INTO #CreditHistory
	from [tblCreditAccountHistory] CAH 
	WHERE CAH.ClientId = @ClientId 
	AND YEAR(ISNULL(CAH.UpdatedDate, CAH.CreatedDate)) <= @year
	AND MONTH(ISNULL(CAH.UpdatedDate, CAH.CreatedDate)) <= @month
	AND CAH.IsDeleted = 0

	DECLARE @OpendingDate DATETIME;
	
	WITH #Temp1 
	AS(
	Select top 1 ISNULL(UpdatedDate, CreatedDate) as LastModified, Amount,CreditAccountHistoryId  from #CreditHistory ORDER BY CreditAccountHistoryId
	)

	Select top 1 @OpeningBalance = Amount, @OpendingDate = LastModified  from #Temp1 

	Select @ActivityAmount = SUM(Amount) from #CreditHistory

	/*
	select @OpeningBalance = SUM(isnull(CAH.Amount,0))
	FROM [tblCreditAccountHistory] CAH 
	WHERE CAH.ClientId = @ClientId AND YEAR(ISNULL(CAH.UpdatedDate, CAH.CreatedDate)) <= @year
	AND MONTH(ISNULL(CAH.UpdatedDate, CAH.CreatedDate)) <= @month AND DAY(ISNULL(CAH.UpdatedDate, CAH.CreatedDate)) <= DAY(@OpendingDate)
	AND CAH.IsDeleted = 0
	*/
	END
	
	Select ISNULL(@OpeningBalance,0) + ISNULL(@ClientBalance,0) as OpeningBalance, ISNULL(@ActivityAmount, 0) + ISNULL(@ClientBalance,0) as ClosingBalance

end