CREATE TABLE [StriveCarSalon].[tblPaymentGateway] (
    [PaymentGatewayId]   INT                IDENTITY (1, 1) NOT NULL,
    [PaymentGatewayName] VARCHAR (150)      NULL,
    [BaseURL]            VARCHAR (200)      NULL,
    [APIKey]             VARCHAR (150)      NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    PRIMARY KEY CLUSTERED ([PaymentGatewayId] ASC)
);

