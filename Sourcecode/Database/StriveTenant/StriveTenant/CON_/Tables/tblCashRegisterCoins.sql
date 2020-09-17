CREATE TABLE [CON].[tblCashRegisterCoins] (
    [CashRegCoinId]  INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterId] INT                NOT NULL,
    [Pennies]        INT                NULL,
    [Nickels]        INT                NULL,
    [Dimes]          INT                NULL,
    [Quarters]       INT                NULL,
    [HalfDollars]    INT                NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblCashRegisterCoins] PRIMARY KEY CLUSTERED ([CashRegCoinId] ASC),
    CONSTRAINT [FK_tblCashRegisterCoins_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [CON].[tblCashRegister] ([CashRegisterId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblCashRegisterCoins_CashRegisterId]
    ON [CON].[tblCashRegisterCoins]([CashRegisterId] ASC);

