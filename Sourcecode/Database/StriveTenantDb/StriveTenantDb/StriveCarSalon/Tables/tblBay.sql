CREATE TABLE [StriveCarSalon].[tblBay] (
    [BayId]       INT          IDENTITY (1, 1) NOT NULL,
    [LocationId]  INT          NULL,
    [BayName]     VARCHAR (20) NULL,
    [IsActive]    BIT          NULL,
    [CreatedDate] DATETIME     NULL,
    CONSTRAINT [PK_tblBay] PRIMARY KEY CLUSTERED ([BayId] ASC),
    CONSTRAINT [FK_tblBay_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

