CREATE TABLE [StriveCarSalon].[tblEmployeeLocation] (
    [EmployeeLocationId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]         INT                NOT NULL,
    [LocationId]         INT                NOT NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeLocation] PRIMARY KEY CLUSTERED ([EmployeeLocationId] ASC),
    CONSTRAINT [FK_tblEmployeeLocation_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeLocation_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeLocation_EmployeeId]
    ON [StriveCarSalon].[tblEmployeeLocation]([EmployeeId] ASC);

