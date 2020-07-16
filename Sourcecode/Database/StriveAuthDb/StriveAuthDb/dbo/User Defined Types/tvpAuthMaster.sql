CREATE TYPE [dbo].[tvpAuthMaster] AS TABLE (
    [AuthId]         INT          NULL,
    [UserGuid]       VARCHAR (64) NOT NULL,
    [EmailId]        VARCHAR (64) NOT NULL,
    [MobileNumber]   BIGINT       NOT NULL,
    [EmailVerified]  SMALLINT     NOT NULL,
    [LockoutEnabled] SMALLINT     NOT NULL,
    [PasswordHash]   VARCHAR (64) NOT NULL,
    [SecurityStamp]  VARCHAR (64) NOT NULL,
    [CreatedDate]    DATETIME     NULL);

