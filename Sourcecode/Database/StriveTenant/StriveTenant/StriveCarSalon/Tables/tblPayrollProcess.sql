CREATE TABLE [StriveCarSalon].[tblPayrollProcess] (
    [PayrollProcessId] INT                IDENTITY (1, 1) NOT NULL,
    [FromDate]         DATE               NULL,
    [ToDate]           DATE               NULL,
    [LocationId]       INT                NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblPayrollProcess] PRIMARY KEY CLUSTERED ([PayrollProcessId] ASC)
);



