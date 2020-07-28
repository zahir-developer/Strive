CREATE TABLE [StriveCarSalon].[tblBay] (
    [BayId]       INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]  INT                NULL,
    [BayName]     VARCHAR (20)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBay] PRIMARY KEY CLUSTERED ([BayId] ASC),
    CONSTRAINT [FK_tblBay_tblBay] FOREIGN KEY ([BayId]) REFERENCES [StriveCarSalon].[tblBay] ([BayId]),
    CONSTRAINT [FK_tblBay_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





