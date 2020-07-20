CREATE TABLE [dbo].[tblAuthOTP] (
    [AuthOTPId]    INT                                         IDENTITY (1, 1) NOT NULL,
    [AuthId]       INT                                         NOT NULL,
    [MobileNumber] BIGINT MASKED WITH (FUNCTION = 'default()') NULL,
    [EmailId]      NVARCHAR (50)                               NULL,
    [IsVerified]   SMALLINT                                    NOT NULL,
    [CreatedDate]  DATETIME                                    NULL,
    [OTP]          VARCHAR (10)                                NULL,
    CONSTRAINT [PK_AuthOTP_AuthOTPId] PRIMARY KEY CLUSTERED ([AuthOTPId] ASC),
    CONSTRAINT [FK_AuthOTP_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[tblAuthMaster] ([AuthId]),
    CONSTRAINT [UK_AuthOTP_MobileNumber] UNIQUE NONCLUSTERED ([MobileNumber] ASC)
);





