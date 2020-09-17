




CREATE procedure [StriveCarSalon].[uspSaveGiftCard]
@tvpGiftCard tvpGiftCard READONLY,
@tvpGiftCardHistory tvpGiftCardHistory READONLY

AS 
BEGIN

DECLARE @GiftCardId int 
MERGE  [StriveCarSalon].[tblGiftCard] TRG
USING @tvpGiftCard SRC
ON (TRG.GiftCardId = SRC.GiftCardId)
WHEN MATCHED 
THEN
UPDATE SET TRG.LocationId=SRC.LocationId, TRG.GiftCardCode=SRC.GiftCardCode, TRG.GiftCardName=SRC.GiftCardName, 
      TRG.ExpiryDate=SRC.ExpiryDate, TRG.Comments = SRC.Comments,TRG.IsActive = SRC.IsActive, TRG.IsDeleted = SRC.IsDeleted
WHEN NOT MATCHED  THEN

INSERT (LocationId,GiftCardCode,GiftCardName,ExpiryDate,Comments,IsActive,IsDeleted)
 VALUES (SRC.LocationId,SRC.GiftCardCode,SRC.GiftCardName,SRC.ExpiryDate,SRC.Comments,SRC.IsActive,SRC.IsDeleted);
 
 SELECT @GiftCardId = scope_identity();


--tblGiftCardHisory--
MERGE  [StriveCarSalon].[tblGiftCardHistory] TRG
USING @tvpGiftCardHistory SRC
ON (TRG.GiftCardHistoryId = SRC.GiftCardHistoryId)
WHEN MATCHED 
THEN
UPDATE SET TRG.GiftCardId=SRC.GiftCardId, TRG.LocationId=SRC.LocationId, TRG.TransactionType=SRC.TransactionType, 
    TRG.TransactionAmount=SRC.TransactionAmount,TRG.TransactionDate= SRC.TransactionDate,
	TRG.Comments= SRC.Comments, TRG.IsActive = SRC.IsActive, TRG.IsDeleted = SRC.IsDeleted


WHEN NOT MATCHED THEN

INSERT (GiftCardId, LocationId, TransactionType, TransactionAmount,TransactionDate,Comments,IsActive,IsDeleted)
 VALUES (@GiftCardId, SRC.LocationId, SRC.TransactionType, SRC.TransactionAmount,SRC.TransactionDate,SRC.Comments,SRC.IsActive,SRC.IsDeleted);

 
END
