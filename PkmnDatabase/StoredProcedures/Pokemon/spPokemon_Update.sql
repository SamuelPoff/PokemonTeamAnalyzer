CREATE PROCEDURE [dbo].[spPokemon_Update]
	@Id INT,
	@Name nvarchar(25),
	@Type1 nvarchar(10),
	@Type2 nvarchar(10),
	@Hp INT,
	@Att INT,
	@Def INT,
	@SpAtt INT,
	@SpDef INT, 
	@Spd INT
AS
BEGIN

UPDATE dbo.Pokemon
set [Name]=@Name, Type1=@Type1, Type2=@Type2, Hp=@Hp, Att=@Att, Def=@Def, SpAtt=@SpAtt, SpDef=@SpDef, Spd=@Spd
WHERE Id = @Id;

END
