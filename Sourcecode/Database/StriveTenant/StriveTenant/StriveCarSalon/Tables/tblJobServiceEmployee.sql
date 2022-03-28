CREATE TABLE [StriveCarSalon].[tblJobServiceEmployee] (
    [JobServiceEmployeeId] INT                IDENTITY (1, 1) NOT NULL,
    [JobItemId]            INT                NULL,
    [ServiceId]            INT                NULL,
    [EmployeeId]           INT                NULL,
    [CommissionAmount]     DECIMAL (18, 2)    NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblJobServiceEmployee] PRIMARY KEY CLUSTERED ([JobServiceEmployeeId] ASC),
    CONSTRAINT [FK_tblJobServiceEmployee_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblJobServiceEmployee_JobItemId] FOREIGN KEY ([JobItemId]) REFERENCES [StriveCarSalon].[tblJobItem] ([JobItemId]),
    CONSTRAINT [FK_tblJobServiceEmployee_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId])
);






GO
CREATE NONCLUSTERED INDEX [tblJobServiceEmployee_Index_JobItemId]
    ON [StriveCarSalon].[tblJobServiceEmployee]([JobItemId] ASC);

