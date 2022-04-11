CREATE PROCEDURE [dbo].[spPokemon_GetByNameSearchLimited]
	@SearchString NVARCHAR(25),
	@NumberOfResults int
AS
BEGIN

SELECT TOP (@NumberOfResults) [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
FROM dbo.Pokemon
WHERE [Name] LIKE '%'+@SearchString+'%';

END
