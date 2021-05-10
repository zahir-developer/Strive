CREATE TABLE [StriveCarSalon].[tblVehicleMake] (
    [MakeId]      INT                IDENTITY (1, 1) NOT NULL,
    [MakeValue]   VARCHAR (100)      NULL,
    [SortOrder]   INT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblVehicleMake] PRIMARY KEY CLUSTERED ([MakeId] ASC)
);

