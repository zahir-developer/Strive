CREATE TABLE [CON].[tblCashRegisterOthers] (
    [CashRegOtherId] INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterId] INT                NOT NULL,
    [CreditCard1]    DECIMAL (19, 4)    NULL,
    [CreditCard2]    DECIMAL (19, 4)    NULL,
    [CreditCard3]    DECIMAL (19, 4)    NULL,
    [Checks]         DECIMAL (19, 4)    NULL,
    [Payouts]        DECIMAL (19, 4)    NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblCashRegisterOthers] PRIMARY KEY CLUSTERED ([CashRegOtherId] ASC),
    CONSTRAINT [FK_tblCashRegisterOthers_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [CON].[tblCashRegister] ([CashRegisterId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblCashRegisterOthers_CashRegisterId]
    ON [CON].[tblCashRegisterOthers]([CashRegisterId] ASC);

