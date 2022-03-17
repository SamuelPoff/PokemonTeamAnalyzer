CREATE PROCEDURE [dbo].[spPokemon_Get]
	@id int
AS
BEGIN

SELECT [Id], [Name], [Type1], [Type2], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd] 
FROM dbo.Pokemon
WHERE Id = @id;

END
