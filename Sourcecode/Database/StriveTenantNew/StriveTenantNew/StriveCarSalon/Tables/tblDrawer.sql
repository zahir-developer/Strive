CREATE TABLE [StriveCarSalon].[tblDrawer] (
    [DrawerId]    INT                IDENTITY (1, 1) NOT NULL,
    [DrawerName]  VARCHAR (10)       NULL,
    [LocationId]  INT                NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblDrawer] PRIMARY KEY CLUSTERED ([DrawerId] ASC),
    CONSTRAINT [FK_tblDrawer_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





