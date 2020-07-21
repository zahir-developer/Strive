CREATE TABLE [StriveCarSalon].[tblCashRegisterOthers] (
    [CashRegOtherId] INT             IDENTITY (1, 1) NOT NULL,
    [CreditCard1]    DECIMAL (19, 4) NULL,
    [CreditCard2]    DECIMAL (19, 4) NULL,
    [CreditCard3]    DECIMAL (19, 4) NULL,
    [Checks]         DECIMAL (19, 4) NULL,
    [Payouts]        DECIMAL (19, 4) NULL,
    [DateEntered]    DATETIME        NULL,
    CONSTRAINT [PK_tblCashRegisterOthers] PRIMARY KEY CLUSTERED ([CashRegOtherId] ASC)
);

