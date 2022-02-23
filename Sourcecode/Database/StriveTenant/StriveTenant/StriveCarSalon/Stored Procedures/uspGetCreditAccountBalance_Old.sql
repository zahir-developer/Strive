
--EXEC uspGetCreditAccountBalance 32,2,2022

CREATE PROCEDURE [StriveCarSalon].[uspGetCreditAccountBalance_Old]     
@ClientId varchar(10),
@month INT,
@year INT
as    
begin    
    
declare @rolledBackAmount DECIMAL(18,2) =0, @ActivityAmount DECIMAL(18,2)=0, @openingAmount DECIMAL(18,2)=0, @openingrollback DECIMAL(18,2)=0

IF(@year > 0)
BEGIN
	select @rolledBackAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 1 OR ISNULL(jp.IsRollBack,0) =1) AND CAH.IsDeleted = 0	
	AND  YEAR(CAH.UpdatedDate) <= @year
	AND  MONTH(CAH.UpdatedDate) <= @month
	
	select @openingrollback = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 1 OR ISNULL(jp.IsRollBack,0) =1) AND CAH.IsDeleted = 0	
	AND  YEAR(CAH.UpdatedDate) <= @year
	AND  MONTH(CAH.UpdatedDate) <= @month
	AND  day(CAH.UpdatedDate) < 1

	select @ActivityAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 0 OR ISNULL(jp.IsRollBack,0) =0) AND CAH.IsDeleted = 0
	AND  YEAR(CAH.UpdatedDate) <= @year
	AND  MONTH(CAH.UpdatedDate) <= @month

	select @openingAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 0 OR ISNULL(jp.IsRollBack,0) =0) AND CAH.IsDeleted = 0
	AND  YEAR(CAH.UpdatedDate) <= @year
	AND  MONTH(CAH.UpdatedDate) <= @month
	AND  day(CAH.UpdatedDate) < 1

	Select (ISNULL(tblcli.Amount,0) +  (-1*ISNULL(@rolledBackAmount,0)) + ISNULL(@ActivityAmount,0) ) ClosingBalance,
	(ISNULL(tblcli.Amount,0) +  (-1*ISNULL(@openingrollback,0)) + ISNULL(@openingAmount,0) ) OpeningBalance
	From  tblClient tblcli 
	where  tblcli.ClientId = @ClientId 
	Select @rolledBackAmount , @ActivityAmount, @openingAmount , @openingrollback
	END
else
begin
	select @rolledBackAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 1 OR ISNULL(jp.IsRollBack,0) =1) AND CAH.IsDeleted = 0	

	select @openingrollback = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 1 OR ISNULL(jp.IsRollBack,0) =1) AND CAH.IsDeleted = 0	
	AND  YEAR(CAH.UpdatedDate) <= YEAR(GETDATE())
	AND  MONTH(CAH.UpdatedDate) <= @month
	AND  day(CAH.UpdatedDate) < 1

	select @ActivityAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 0 OR ISNULL(jp.IsRollBack,0) =0) AND CAH.IsDeleted = 0

	select @openingAmount = SUM(isnull(CAH.Amount,0))
	FROM tblClient tblcli 
	LEFT JOIN [tblCreditAccountHistory] CAH ON CAH.ClientId = tblcli.ClientId AND CAH.IsDeleted = 0
	LEFT JOIN tblJobPayment jp ON jp.JobPaymentId = CAH.JobPaymentId
	WHERE   tblcli.ClientId = @ClientId 
	AND (jp.IsDeleted = 0 OR ISNULL(jp.IsRollBack,0) =0) AND CAH.IsDeleted = 0
	AND  YEAR(CAH.UpdatedDate) <= YEAR(GETDATE())
	AND  MONTH(CAH.UpdatedDate) <= @month
	AND  day(CAH.UpdatedDate) < 1

	Select (ISNULL(tblcli.Amount,0) +  (-1*ISNULL(@rolledBackAmount,0)) + ISNULL(@ActivityAmount,0) ) ClosingBalance,
	(ISNULL(tblcli.Amount,0) +  (-1*ISNULL(@openingrollback,0)) + ISNULL(@openingAmount,0) ) OpeningBalance
	From  tblClient tblcli 
	where  tblcli.ClientId = @ClientId 
	Select @rolledBackAmount , @ActivityAmount, @openingAmount , @openingrollback
	end
end