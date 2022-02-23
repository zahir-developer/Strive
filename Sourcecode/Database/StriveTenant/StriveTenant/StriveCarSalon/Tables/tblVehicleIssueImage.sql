CREATE TABLE [StriveCarSalon].[tblVehicleIssueImage] (
    [VehicleIssueImageId] INT                IDENTITY (1, 1) NOT NULL,
    [VehicleIssueId]      INT                NOT NULL,
    [DocumentType]        INT                NULL,
    [ImageName]           VARCHAR (150)      NOT NULL,
    [OriginalImageName]   VARCHAR (100)      NULL,
    [ThumbnailFileName]   VARCHAR (100)      NULL,
    [FilePath]            VARCHAR (1000)     NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_tblVehicleIssueImage_VehicleIssueImageId] PRIMARY KEY CLUSTERED ([VehicleIssueImageId] ASC),
    CONSTRAINT [FK_StriveCarSalon_tblVehicleIssueImage_DocumentType] FOREIGN KEY ([DocumentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_StriveCarSalon_tblVehicleIssueImage_VehicleIssueId] FOREIGN KEY ([VehicleIssueId]) REFERENCES [StriveCarSalon].[tblVehicleIssue] ([VehicleIssueId])
);

