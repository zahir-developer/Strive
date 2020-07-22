CREATE TYPE [StriveCarSalon].[tvpCashRegisterNew] AS TABLE (
    [CashRegisterId]   BIGINT NULL,
    [CashRegisterType] INT    NULL,
    [LocationId]       INT    NULL,
    [DrawerId]         INT    NULL,
    [UserId]           BIGINT NULL,
    [CashRegCoinId]    INT    NULL,
    [CashRegBillId]    INT    NULL,
    [CashRegRollId]    INT    NULL,
    [CashRegOtherId]   INT    NULL);

