CREATE TABLE [StriveCarSalon].[tblChatUserGroup] (
    [ChatGroupUserId] INT IDENTITY (1, 1) NOT NULL,
    [UserId]          INT NULL,
    [GroupId]         INT NULL,
    [IsActive]        BIT NULL,
    [IsDeleted]       BIT NULL,
    [CreatedBy]       INT NULL,
    [CreatedDate]     INT NULL,
    CONSTRAINT [PK_tblChatUserGroup] PRIMARY KEY CLUSTERED ([ChatGroupUserId] ASC),
    CONSTRAINT [FK_tblChatUserGroup_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [StriveCarSalon].[tblChatGroup] ([ChatGroupId]),
    CONSTRAINT [FK_tblChatUserGroup_UserId] FOREIGN KEY ([UserId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);




GO
CREATE NONCLUSTERED INDEX [IX_tblChatUserGroup_UserId]
    ON [StriveCarSalon].[tblChatUserGroup]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatUserGroup_GroupId]
    ON [StriveCarSalon].[tblChatUserGroup]([GroupId] ASC);

