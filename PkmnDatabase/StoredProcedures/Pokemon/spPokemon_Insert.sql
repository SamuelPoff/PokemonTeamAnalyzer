CREATE PROCEDURE [dbo].[spPokemon_Insert]
	@Name NVARCHAR(25),
	@Type1 NVARCHAR(10),
	@Type2 NVARCHAR(10) NULL,
	@Ability1 NVARCHAR(20),
	@Ability2 NVARCHAR(20) NULL,
	@HiddenAbility NVARCHAR(20) NULL,
	@Hp INT,
	@Att INT,
	@Def INT,
	@SpAtt INT,
	@SpDef INT,
	@Spd INT,
	@SpriteUrl NVARCHAR(100) NULL
AS
BEGIN

INSERT INTO dbo.Pokemon([Name], Type1, Type2, Ability1, Ability2, HiddenAbility, Hp, Att, Def, SpAtt, SpDef, Spd, SpriteUrl)
VALUES (@Name, @Type1, @Type2, @Ability1, @Ability2, @HiddenAbility, @Hp, @Att, @Def, @SpAtt, @SpDef, @Spd, @SpriteUrl);

END
