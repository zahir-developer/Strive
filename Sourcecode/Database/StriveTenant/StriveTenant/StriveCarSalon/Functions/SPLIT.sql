CREATE FUNCTION [StriveCarSalon].[SPLIT]
(
	@DATA VARCHAR(2000)	
)  
RETURNS @TEMP TABLE (IDT INT IDENTITY(1,1), ID NVARCHAR(100)) 
AS  
/************************************************************************************** TESTING			:   SELECT * FROM DBO.FUN_SPLIT('A,B,C',',')
**************************************************************************************/
BEGIN 	
	DECLARE @SEP VARCHAR(5) = ','
	DECLARE @CNT INT	
	SET @CNT = 1	
	WHILE (CHARINDEX(@SEP,@DATA)>0)
	BEGIN		
		INSERT INTO @TEMP (ID)		
		SELECT	DATA = LTRIM(RTRIM(SUBSTRING(@DATA,1,CHARINDEX(@SEP,@DATA)-1)))		
		SET @DATA = SUBSTRING(@DATA,CHARINDEX(@SEP,@DATA)+1,LEN(@DATA))		
		SET @CNT = @CNT + 1	
	END		
	INSERT INTO @TEMP (ID)	
	SELECT DATA = LTRIM(RTRIM(@DATA))	
	RETURN

END