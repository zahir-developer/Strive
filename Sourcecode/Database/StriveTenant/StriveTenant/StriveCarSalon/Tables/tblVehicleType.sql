CREATE TABLE [StriveCarSalon].[tblVehicleType] (
    [TypeId]      INT                IDENTITY (1, 1) NOT NULL,
    [TypeValue]   VARCHAR (100)      NULL,
    [Category]    VARCHAR (10)       NULL,
    [SortOrder]   INT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblVehicleType] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);

