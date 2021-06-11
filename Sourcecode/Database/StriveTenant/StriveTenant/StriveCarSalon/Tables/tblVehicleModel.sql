CREATE TABLE [StriveCarSalon].[tblVehicleModel] (
    [ModelId]     INT                IDENTITY (1, 1) NOT NULL,
    [MakeId]      INT                NULL,
    [SortOrder]   INT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [ModelValue]  VARCHAR (100)      NULL,
    [TypeId]      INT                NULL,
    CONSTRAINT [PK_tblVehicleModel] PRIMARY KEY CLUSTERED ([ModelId] ASC),
    CONSTRAINT [FK_tblVehicleModel_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [StriveCarSalon].[tblVehicleType] ([TypeId])
);



