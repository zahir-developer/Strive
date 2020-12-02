CREATE TABLE [StriveCarSalon].[tblChecklist] (
    [ChecklistId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [RoleId]      INT           NULL,
    [IsActive]    BIT           NULL,
    [IsDeleted]   BIT           NULL,
    CONSTRAINT [PK__tblCheck__4C1D499AD706CA4E] PRIMARY KEY CLUSTERED ([ChecklistId] ASC)
);

