CREATE TABLE [StriveCarSalon].[tblChatCommunication] (
    [ChatCommunicationId] INT           IDENTITY (1, 1) NOT NULL,
    [EmployeeId]          INT           NOT NULL,
    [CommunicationId]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_tblChatCommunication] PRIMARY KEY CLUSTERED ([ChatCommunicationId] ASC),
    CONSTRAINT [FK_tblChatCommunication_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

