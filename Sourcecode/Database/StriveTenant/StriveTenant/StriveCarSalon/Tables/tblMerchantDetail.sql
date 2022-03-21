CREATE TABLE [StriveCarSalon].[tblMerchantDetail] (
    [MerchantDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]       INT                NULL,
    [UserName]         VARCHAR (50)       NULL,
    [MID]              VARCHAR (50)       NULL,
    [Password]         VARCHAR (50)       NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    [URL]              VARCHAR (200)      NULL,
    [IsRecurring]      BIT                NULL,
    CONSTRAINT [PK_tblMerchantDetail] PRIMARY KEY CLUSTERED ([MerchantDetailId] ASC),
    CONSTRAINT [FK_tblMerchantDetail_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

