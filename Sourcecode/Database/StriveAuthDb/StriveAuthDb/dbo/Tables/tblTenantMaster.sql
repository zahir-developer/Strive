CREATE TABLE [dbo].[tblTenantMaster] (
    [TenantId]       INT              IDENTITY (1, 1) NOT NULL,
    [TenantGuid]     UNIQUEIDENTIFIER NULL,
    [SubscriptionId] INT              NULL,
    [ClientId]       INT              NULL,
    [IsActive]       SMALLINT         NULL,
    [IsDeleted]      INT              NULL,
    [EmpSize]        SMALLINT         NULL,
    [UTCPlusMinus]   SMALLINT         NULL,
    [TimeZoneMinus]  SMALLINT         NULL,
    [ExpiryDate]     DATETIME         NULL,
    [CreatedDate]    DATETIME         NULL,
    CONSTRAINT [PK_tblTenantMaster] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);

