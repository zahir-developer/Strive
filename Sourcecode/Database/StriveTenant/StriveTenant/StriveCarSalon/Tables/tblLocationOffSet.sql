CREATE TABLE [StriveCarSalon].[tblLocationOffSet] (
    [LocationOffSetId] INT IDENTITY (1, 1) NOT NULL,
    [OffSet1]          BIT NULL,
    [OffSetA]          BIT NULL,
    [OffSetB]          BIT NULL,
    [OffSetC]          BIT NULL,
    [OffSetD]          BIT NULL,
    [OffSetE]          BIT NULL,
    [OffSetF]          BIT NULL,
    [LocationId]       INT NULL,
    [OffSet1On]        BIT NULL,
    [IsActive]         BIT NULL,
    [IsDeleted]        BIT NULL,
    CONSTRAINT [PK__tblLocationOffSet] PRIMARY KEY CLUSTERED ([LocationOffSetId] ASC),
    CONSTRAINT [FK_tblLocation_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);



