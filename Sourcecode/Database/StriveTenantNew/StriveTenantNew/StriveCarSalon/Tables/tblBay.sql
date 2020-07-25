CREATE TABLE [StriveCarSalon].[tblBay] (
    [BayId]       INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]  INT                NULL,
    [BayName]     VARCHAR (20)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL
);

