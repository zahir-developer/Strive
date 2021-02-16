CREATE TABLE [StriveCarSalon].[tblJobItem] (
    [JobItemId]       INT                IDENTITY (1, 1) NOT NULL,
    [JobId]           INT                NULL,
    [ServiceId]       INT                NULL,
    [EmployeeId]      INT                NULL,
    [Commission]      DECIMAL (16, 2)    NULL,
    [CommissionType]  INT                NULL,
    [Price]           DECIMAL (16, 2)    NULL,
    [Quantity]        INT                NULL,
    [ReviewNote]      VARCHAR (50)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    [MrgRefRecItemid] INT                NULL,
    CONSTRAINT [PK_tblJobItem] PRIMARY KEY CLUSTERED ([JobItemId] ASC),
    CONSTRAINT [FK_tblJobItem_JobId] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobItem_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId]),
    CONSTRAINT [FK_tblJobItem_tblCodeValue] FOREIGN KEY ([CommissionType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);








GO
CREATE NONCLUSTERED INDEX [IX_tblJobItem_JobId]
    ON [StriveCarSalon].[tblJobItem]([JobId] ASC);

