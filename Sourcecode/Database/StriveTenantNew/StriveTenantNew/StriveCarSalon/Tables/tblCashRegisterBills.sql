CREATE TABLE [StriveCarSalon].[tblCashRegisterBills] (
    [CashRegBillId]  INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterId] INT                NOT NULL,
    [s1]             INT                NULL,
    [s5]             INT                NULL,
    [s10]            INT                NULL,
    [s20]            INT                NULL,
    [s50]            INT                NULL,
    [s100]           INT                NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblCashRegisterBills] PRIMARY KEY CLUSTERED ([CashRegBillId] ASC),
    CONSTRAINT [FK_tblCashRegisterBills_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [StriveCarSalon].[tblCashRegister] ([CashRegisterId])
);






GO
CREATE NONCLUSTERED INDEX [IX_tblCashRegisterBills_CashRegisterId]
    ON [StriveCarSalon].[tblCashRegisterBills]([CashRegisterId] ASC);

