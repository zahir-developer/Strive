CREATE TABLE [StriveCarSalon].[StgTimeClock_Out] (
    [LocationID] INT      NOT NULL,
    [UserID]     INT      NULL,
    [ClockID]    INT      NOT NULL,
    [Cdate]      DATE     NULL,
    [Cdatetime]  DATETIME NULL,
    [Caction]    INT      NULL,
    [editby]     INT      NULL,
    [editdate]   DATETIME NULL,
    [Paid]       BIT      NULL,
    [CType]      CHAR (2) NULL,
    [RNK]        INT      NULL
);



