CREATE PROCEDURE [StriveCarSalon].[uspGetWhiteLabel] 
AS
BEGIN

SELECT 
	tblWL.WhiteLabelId,
	tblWL.LogoPath,
	tblWl.Title,
	isNull(tblWL.ThemeId,0) as ThemeId,
	tblWL.FontFace
FROM [tblWhiteLabel] tblWL

WHERE isnull(tblWL.IsDeleted,0)=0

SELECT 
	tblT.ThemeId,
	tblT.ThemeName,
	tblT.FontFace,
	tblT.PrimaryColor,
	tblT.SecondaryColor,
	tblT.TertiaryColor,
	tblT.NavigationColor,
	tblT.BodyColor,
	tblT.DefaultLogoPath,
	tblT.DefaultTitle,
	tblT.Comments
FROM [tblThemes] tblT

WHERE isnull(tblT.IsDeleted,0)=0

END