CREATE TABLE [StriveCarSalon].[tblVehicleImage] (
    [VehicleImageId]    INT                IDENTITY (1, 1) NOT NULL,
    [ImageName]         VARCHAR (150)      NOT NULL,
    [OriginalImageName] VARCHAR (100)      NULL,
    [ThumbnailFileName] VARCHAR (100)      NULL,
    [FilePath]          VARCHAR (1000)     NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    [DocumentType]      INT                NULL,
    [VehicleId]         INT                NULL,
    [Description]       NVARCHAR (500)     NULL,
    CONSTRAINT [PK_StriveCarSalon_tblVehicleImage_VehicleImageId] PRIMARY KEY CLUSTERED ([VehicleImageId] ASC),
    CONSTRAINT [FK_StriveCarSalon_tblVehicleImage_DocumentType] FOREIGN KEY ([DocumentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_StriveCarSalon_tblVehicleImage_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId])
);





