CREATE TABLE [StriveCarSalon].[Stg_TimeClock] (
    [LocationID] INT      NOT NULL,
    [UserID]     INT      NOT NULL,
    [ALT_UserID] INT      NULL,
    [ClockID]    INT      NOT NULL,
    [Cdatetime]  DATETIME NULL,
    [Caction]    INT      NULL,
    [editby]     INT      NULL,
    [editdate]   DATETIME NULL,
    [Paid]       BIT      NULL,
    [CType]      CHAR (2) NULL
);

