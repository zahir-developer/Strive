CREATE FUNCTION StriveCarSalon.SPLIT
(
	@DATA VARCHAR(2000),	
	@SEP VARCHAR(5)
)  
RETURNS @TEMP TABLE (ID INT IDENTITY(1,1), DATA NVARCHAR(100)) 
AS  
/************************************************************************************** TESTING			:   SELECT * FROM DBO.FUN_SPLIT('A,B,C',',')
**************************************************************************************/
BEGIN 	
	DECLARE @CNT INT	
	SET @CNT = 1	
	WHILE (CHARINDEX(@SEP,@DATA)>0)
	BEGIN		
		INSERT INTO @TEMP (DATA)		
		SELECT	DATA = LTRIM(RTRIM(SUBSTRING(@DATA,1,CHARINDEX(@SEP,@DATA)-1)))		
		SET @DATA = SUBSTRING(@DATA,CHARINDEX(@SEP,@DATA)+1,LEN(@DATA))		
		SET @CNT = @CNT + 1	
	END		
	INSERT INTO @TEMP (DATA)	
	SELECT DATA = LTRIM(RTRIM(@DATA))	
	RETURN

END