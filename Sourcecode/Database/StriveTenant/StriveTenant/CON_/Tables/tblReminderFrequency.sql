CREATE TABLE [CON].[tblReminderFrequency] (
    [ReminderFrequencyId] INT          IDENTITY (1, 1) NOT NULL,
    [Title]               VARCHAR (15) NULL,
    [Frequency]           INT          NULL,
    [IsActive]            BIT          NULL,
    [IsDeleted]           BIT          NULL,
    CONSTRAINT [PK_tblReminderFrequency] PRIMARY KEY CLUSTERED ([ReminderFrequencyId] ASC)
);

