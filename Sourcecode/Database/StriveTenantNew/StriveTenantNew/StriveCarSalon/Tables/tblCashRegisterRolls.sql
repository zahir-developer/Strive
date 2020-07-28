CREATE TABLE [StriveCarSalon].[tblCashRegisterRolls] (
    [CashRegRollId]  INT                IDENTITY (1, 1) NOT NULL,
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
    CONSTRAINT [PK_tblCashRegisterRolls] PRIMARY KEY CLUSTERED ([CashRegRollId] ASC),
    CONSTRAINT [FK_tblCashRegisterRolls_tblCashRegister] FOREIGN KEY ([CashRegisterId]) REFERENCES [StriveCarSalon].[tblCashRegister] ([CashRegisterId])
);



