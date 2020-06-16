CREATE TABLE [dbo].[tblAuthMaster] (
    [AuthId]         INT                                               IDENTITY (1, 1) NOT NULL,
    [UserGuid]       VARCHAR (64)                                      NOT NULL,
    [EmailId]        VARCHAR (64) MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [MobileNumber]   BIGINT MASKED WITH (FUNCTION = 'default()')       NOT NULL,
    [EmailVerified]  SMALLINT                                          NOT NULL,
    [LockoutEnabled] SMALLINT                                          NOT NULL,
    [PasswordHash]   VARCHAR (64)                                      NOT NULL,
    [SecurityStamp]  VARCHAR (64)                                      NOT NULL,
    [CreatedDate]    DATETIME                                          NULL,
    CONSTRAINT [PK_AuthMaster_AuthId] PRIMARY KEY CLUSTERED ([AuthId] ASC),
    CONSTRAINT [UK_AuthMaster_EmailId] UNIQUE NONCLUSTERED ([EmailId] ASC),
    CONSTRAINT [UK_AuthMaster_MobileNumber] UNIQUE NONCLUSTERED ([MobileNumber] ASC),
    CONSTRAINT [UK_AuthMaster_UserGuid] UNIQUE NONCLUSTERED ([UserGuid] ASC)
);

