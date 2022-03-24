CREATE PROCEDURE [dbo].[spPokemon_Get]
	@id int
AS
BEGIN

SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
FROM dbo.Pokemon
WHERE Id = @id;

END
