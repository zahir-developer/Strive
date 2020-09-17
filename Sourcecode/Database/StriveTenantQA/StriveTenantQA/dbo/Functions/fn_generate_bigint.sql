
CREATE FUNCTION [dbo].[fn_generate_bigint](@datetime DATETIME)

RETURNS BIGINT



AS



BEGIN



RETURN (

    SELECT (

           DATEPART(YEAR, @datetime) * 10000000000 +

           DATEPART(MONTH, @datetime) * 100000000 +

           DATEPART(DAY, @datetime) * 1000000 +

           DATEPART(HOUR, @datetime) * 10000 +

           DATEPART(MINUTE, @datetime) * 100 +

           DATEPART(SECOND, @datetime)

           ) * 1000 +

           DATEPART(millisecond, @datetime)

)

END

