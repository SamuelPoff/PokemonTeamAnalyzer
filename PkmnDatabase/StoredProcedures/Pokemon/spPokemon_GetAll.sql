CREATE PROCEDURE [dbo].[spPokemon_GetAll]
AS
BEGIN

SELECT [Id], [Name], [Type1], [Type2], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd] 
FROM dbo.Pokemon;

END
