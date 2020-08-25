CREATE TABLE [dbo].[tblAuthOTP_BAK] (
    [AuthOTP_BAKId] INT                                         IDENTITY (1, 1) NOT NULL,
    [AuthId]        INT                                         NOT NULL,
    [MobileNumber]  BIGINT MASKED WITH (FUNCTION = 'default()') NULL,
    [EmailId]       NVARCHAR (50)                               NULL,
    [IsVerified]    SMALLINT                                    NOT NULL,
    [CreatedDate]   DATETIME                                    NULL,
    [OTP]           VARCHAR (10)                                NULL,
    CONSTRAINT [PK_AuthOTP_BAK_AuthOTP_BAKId] PRIMARY KEY CLUSTERED ([AuthOTP_BAKId] ASC),
    CONSTRAINT [FK_AuthOTP_BAK_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[tblAuthMaster] ([AuthId]),
    CONSTRAINT [UK_AuthOTP_BAK_MobileNumber] UNIQUE NONCLUSTERED ([MobileNumber] ASC)
);

