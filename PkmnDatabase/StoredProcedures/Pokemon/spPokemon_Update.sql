CREATE PROCEDURE [dbo].[spPokemon_Update]
	@Id INT,
	@Name nvarchar(25),
	@Type1 nvarchar(10),
	@Type2 nvarchar(10),
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

UPDATE dbo.Pokemon
set [Name]=@Name, Type1=@Type1, Type2=@Type2, Ability1=@Ability1, Ability2=@Ability2, HiddenAbility=@HiddenAbility, Hp=@Hp, Att=@Att, Def=@Def, SpAtt=@SpAtt, SpDef=@SpDef, Spd=@Spd, SpriteUrl=@SpriteUrl
WHERE Id = @Id;

END
