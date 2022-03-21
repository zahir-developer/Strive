CREATE TABLE [dbo].[tblAuthMaster] (
    [AuthId]         INT          IDENTITY (1, 1) NOT NULL,
    [UserGuid]       VARCHAR (64) NOT NULL,
    [EmailId]        VARCHAR (64) NOT NULL,
    [MobileNumber]   VARCHAR (50) NOT NULL,
    [EmailVerified]  SMALLINT     NOT NULL,
    [LockoutEnabled] SMALLINT     NOT NULL,
    [PasswordHash]   VARCHAR (64) NOT NULL,
    [SecurityStamp]  VARCHAR (64) NOT NULL,
    [CreatedDate]    DATETIME     NULL,
    [UserType]       INT          NULL,
    [TenantId]       INT          NULL,
    [LoginId]        VARCHAR (32) NULL,
    [IsActive] BIT NULL, 
    [IsDeleted] BIT NULL, 
    CONSTRAINT [PK_AuthMaster_AuthId] PRIMARY KEY CLUSTERED ([AuthId] ASC),
    CONSTRAINT [FK_tblAuthMaster_tblTenantMaster] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[tblTenantMaster] ([TenantId]),
    CONSTRAINT [IX_tblAuthMaster] UNIQUE NONCLUSTERED ([EmailId] ASC, [UserType] ASC, [TenantId] ASC),
    CONSTRAINT [UK_AuthMaster_UserGuid] UNIQUE NONCLUSTERED ([UserGuid] ASC)
);



