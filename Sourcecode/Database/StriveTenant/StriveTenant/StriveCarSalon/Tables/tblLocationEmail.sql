CREATE TABLE [StriveCarSalon].[tblLocationEmail] (
    [LocationEmailId] INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]      INT                NULL,
    [EmailAddress]    VARCHAR (50)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblLocationEmail] PRIMARY KEY CLUSTERED ([LocationEmailId] ASC),
    CONSTRAINT [FK_tblLocationEmail_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

