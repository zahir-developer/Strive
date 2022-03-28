CREATE TABLE [StriveCarSalon].[tblJobProductItem] (
    [JobProductItemId] INT                IDENTITY (1, 1) NOT NULL,
    [JobId]            INT                NULL,
    [ProductId]        INT                NULL,
    [Commission]       DECIMAL (16, 2)    NULL,
    [Price]            DECIMAL (16, 2)    NULL,
    [Quantity]         INT                NULL,
    [ReviewNote]       VARCHAR (50)       NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    [RefRecItemid]     INT                NULL,
    CONSTRAINT [PK_tblJobProductItem_JobItemProductId] PRIMARY KEY CLUSTERED ([JobProductItemId] ASC),
    CONSTRAINT [FK_tblJobProductItem_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId])
);




GO
CREATE NONCLUSTERED INDEX [Index_tblJobProductItem_JobId]
    ON [StriveCarSalon].[tblJobProductItem]([JobId] ASC);

