CREATE TABLE [StriveCarSalon].[tblCashRegisterBills] (
    [CashRegBillId] INT      IDENTITY (1, 1) NOT NULL,
    [1s]            INT      NULL,
    [5s]            INT      NULL,
    [10s]           INT      NULL,
    [20s]           INT      NULL,
    [50s]           INT      NULL,
    [100s]          INT      NULL,
    [DateEntered]   DATETIME NULL,
    CONSTRAINT [PK_tblCashRegisterBills] PRIMARY KEY CLUSTERED ([CashRegBillId] ASC)
);

