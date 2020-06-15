CREATE TABLE [dbo].[tblTenantDetail] (
    [TenantId]      INT           NOT NULL,
    [TenantName]    VARCHAR (64)  NOT NULL,
    [ColorTheme]    VARCHAR (16)  NOT NULL,
    [Currency]      VARCHAR (4)   NOT NULL,
    [TelantLogoUrl] VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_TenantDetail_TenantId] PRIMARY KEY CLUSTERED ([TenantId] ASC),
    CONSTRAINT [FK_TenantDetail_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[tblTenantMaster] ([TenantId])
);

