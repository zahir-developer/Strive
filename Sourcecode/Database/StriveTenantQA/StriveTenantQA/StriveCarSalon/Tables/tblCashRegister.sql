CREATE TABLE [StriveCarSalon].[tblCashRegister] (
    [CashRegisterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterType] INT                NULL,
    [LocationId]       INT                NULL,
    [DrawerId]         INT                NULL,
    [CashRegisterDate] DATE               NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblCashRegister] PRIMARY KEY CLUSTERED ([CashRegisterId] ASC),
    CONSTRAINT [FK_tblCashRegister_CashRegisterType] FOREIGN KEY ([CashRegisterType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblCashRegister_DrawerId] FOREIGN KEY ([DrawerId]) REFERENCES [StriveCarSalon].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblCashRegister_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblCashRegister_CashRegisterDate]
    ON [StriveCarSalon].[tblCashRegister]([CashRegisterDate] ASC);

