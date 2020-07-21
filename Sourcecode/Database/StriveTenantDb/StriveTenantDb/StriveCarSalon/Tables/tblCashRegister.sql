CREATE TABLE [StriveCarSalon].[tblCashRegister] (
    [CashRegisterId]      BIGINT   IDENTITY (1, 1) NOT NULL,
    [CashRegisterType]    INT      NULL,
    [LocationId]          INT      NULL,
    [DrawerId]            INT      NULL,
    [UserId]              BIGINT   NULL,
    [EnteredDateTime]     DATETIME NULL,
    [CashRegisterCoinId]  INT      NULL,
    [CashRegisterBillId]  INT      NULL,
    [CashRegisterRollId]  INT      NULL,
    [CashRegisterOtherId] INT      NULL,
    CONSTRAINT [PK_tblCashRegister] PRIMARY KEY CLUSTERED ([CashRegisterId] ASC),
    CONSTRAINT [FK_tblCashRegister_tblCashRegister] FOREIGN KEY ([CashRegisterId]) REFERENCES [StriveCarSalon].[tblCashRegister] ([CashRegisterId]),
    CONSTRAINT [FK_tblCashRegister_tblCashRegisterBills] FOREIGN KEY ([CashRegisterBillId]) REFERENCES [StriveCarSalon].[tblCashRegisterBills] ([CashRegBillId]),
    CONSTRAINT [FK_tblCashRegister_tblCashRegisterCoins] FOREIGN KEY ([CashRegisterCoinId]) REFERENCES [StriveCarSalon].[tblCashRegisterCoins] ([CashRegCoinId]),
    CONSTRAINT [FK_tblCashRegister_tblCashRegisterOthers] FOREIGN KEY ([CashRegisterOtherId]) REFERENCES [StriveCarSalon].[tblCashRegisterOthers] ([CashRegOtherId]),
    CONSTRAINT [FK_tblCashRegister_tblCashRegisterRolls] FOREIGN KEY ([CashRegisterRollId]) REFERENCES [StriveCarSalon].[tblCashRegisterRolls] ([CashRegRollId]),
    CONSTRAINT [FK_tblCashRegister_tblDrawer] FOREIGN KEY ([DrawerId]) REFERENCES [StriveCarSalon].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblCashRegister_tblEmployee] FOREIGN KEY ([UserId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblCashRegister_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

