CREATE TABLE [StriveCarSalon].[tblLocationOffSet] (
    [LocationOffSetId] INT             IDENTITY (1, 1) NOT NULL,
    [LocationId]       INT             NULL,
    [OffSet1On]        BIT             NULL,
    [OffSet1]          DECIMAL (18, 2) NULL,
    [OffSetA]          DECIMAL (18, 2) NULL,
    [OffSetB]          DECIMAL (18, 2) NULL,
    [OffSetC]          DECIMAL (18, 2) NULL,
    [OffSetD]          DECIMAL (18, 2) NULL,
    [OffSetE]          DECIMAL (18, 2) NULL,
    [OffSetF]          DECIMAL (18, 2) NULL,
    [IsActive]         BIT             NULL,
    [IsDeleted]        BIT             NULL,
    CONSTRAINT [PK__tblLocationOffSet] PRIMARY KEY CLUSTERED ([LocationOffSetId] ASC),
    CONSTRAINT [FK_tblLocation_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





