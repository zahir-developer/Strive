CREATE TABLE [StriveCarSalon].[tblVehicleIssue] (
    [VehicleIssueId] INT                IDENTITY (1, 1) NOT NULL,
    [VehicleId]      INT                NOT NULL,
    [Description]    NVARCHAR (MAX)     NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    [DocumentType]   INT                NULL,
    CONSTRAINT [PK_StriveCarSalon_tblVehicleIssue_VehicleIssueId] PRIMARY KEY CLUSTERED ([VehicleIssueId] ASC),
    CONSTRAINT [FK_StriveCarSalon_tblVehicleIssue_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId])
);

