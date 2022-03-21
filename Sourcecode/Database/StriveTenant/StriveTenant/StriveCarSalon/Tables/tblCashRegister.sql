CREATE TABLE [StriveCarSalon].[tblCashRegister] (
    [CashRegisterId]       INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterType]     INT                NULL,
    [LocationId]           INT                NULL,
    [DrawerId]             INT                NULL,
    [CashRegisterDate]     DATE               NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    [RefCashdid]           INT                NULL,
    [UserId]               INT                NULL,
    [CashRegisterTime]     DATETIMEOFFSET (7) NULL,
    [StoreTimeIn]          DATETIME           NULL,
    [StoreTimeOut]         DATETIME           NULL,
    [StoreOpenCloseStatus] INT                NULL,
    [WashTips]             DECIMAL (18, 2)    NULL,
    [DetailTips]           DECIMAL (18, 2)    NULL,
    [Tips]                 DECIMAL (18, 2)    NULL,
    [TotalAmount]          DECIMAL (18, 2)    NULL,
    CONSTRAINT [PK_tblCashRegister] PRIMARY KEY CLUSTERED ([CashRegisterId] ASC),
    CONSTRAINT [FK_StrivecarSalon_TblCashRegister_UserId] FOREIGN KEY ([UserId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblCashRegister_CashRegisterType] FOREIGN KEY ([CashRegisterType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblCashRegister_DrawerId] FOREIGN KEY ([DrawerId]) REFERENCES [StriveCarSalon].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblCashRegister_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblCashRegister_StoreOpenCloseStatus] FOREIGN KEY ([StoreOpenCloseStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);












GO
CREATE NONCLUSTERED INDEX [IX_tblCashRegister_CashRegisterDate]
    ON [StriveCarSalon].[tblCashRegister]([CashRegisterDate] ASC);

