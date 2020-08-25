CREATE TABLE [dbo].[AuthMaster] (
    [AuthMasterId]   INT          IDENTITY (1, 1) NOT NULL,
    [UserGuid]       VARCHAR (64) NOT NULL,
    [EmailId]        VARCHAR (64) NOT NULL,
    [MobileNumber]   VARCHAR (50) NOT NULL,
    [EmailVerified]  SMALLINT     NOT NULL,
    [LockoutEnabled] SMALLINT     NOT NULL,
    [PasswordHash]   VARCHAR (64) NOT NULL,
    [SecurityStamp]  VARCHAR (64) NOT NULL,
    [CreatedDate]    DATETIME     NULL,
    [UserType]       INT          NULL
);

