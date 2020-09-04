CREATE TYPE [StriveCarSalonTest].[tvpCashRegister] AS TABLE (
    [CashRegisterId]      BIGINT   NULL,
    [CashRegisterType]    INT      NULL,
    [LocationId]          INT      NULL,
    [DrawerId]            INT      NULL,
    [UserId]              BIGINT   NULL,
    [EnteredDateTime]     DATETIME NULL,
    [CashRegisterCoinId]  INT      NULL,
    [CashRegisterBillId]  INT      NULL,
    [CashRegisterRollId]  INT      NULL,
    [CashRegisterOtherId] INT      NULL);

