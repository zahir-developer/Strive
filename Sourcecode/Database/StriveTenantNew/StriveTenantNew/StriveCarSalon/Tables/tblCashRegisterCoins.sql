﻿CREATE TABLE [StriveCarSalon].[tblCashRegisterCoins] (
    [CashRegCoinId] INT                IDENTITY (1, 1) NOT NULL,
    [Pennies]       INT                NULL,
    [Nickels]       INT                NULL,
    [Dimes]         INT                NULL,
    [Quarters]      INT                NULL,
    [HalfDollars]   INT                NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL
);

