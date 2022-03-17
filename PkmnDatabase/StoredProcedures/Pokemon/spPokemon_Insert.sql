CREATE PROCEDURE [dbo].[spPokemon_Insert]
	@Name NVARCHAR(25),
	@Type1 NVARCHAR(10),
	@Type2 NVARCHAR(10) NULL,
	@Hp INT,
	@Att INT,
	@Def INT,
	@SpAtt INT,
	@SpDef INT,
	@Spd INT
AS
BEGIN

INSERT INTO dbo.Pokemon([Name], Type1, Type2, Hp, Att, Def, SpAtt, SpDef, Spd)
VALUES (@Name, @Type1, @Type2, @Hp, @Att, @Def, @SpAtt, @SpDef, @Spd);

END
