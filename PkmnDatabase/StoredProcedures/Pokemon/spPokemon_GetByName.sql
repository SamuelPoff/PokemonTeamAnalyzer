CREATE PROCEDURE [dbo].[spPokemon_GetByName]
	@Name NVARCHAR(25)
AS
BEGIN

	SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
	FROM dbo.Pokemon
	WHERE [Name] = @Name;

END
