CREATE TABLE [StriveCarSalon].[tblCashRegister] (
    [CashRegisterId]      INT                IDENTITY (1, 1) NOT NULL,
    [CashRegisterType]    INT                NULL,
    [LocationId]          INT                NULL,
    [DrawerId]            INT                NULL,
    [UserId]              INT                NULL,
    [CashRegisterCoinId]  INT                NULL,
    [CashRegisterBillId]  INT                NULL,
    [CashRegisterRollId]  INT                NULL,
    [CashRegisterOtherId] INT                NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL
);

