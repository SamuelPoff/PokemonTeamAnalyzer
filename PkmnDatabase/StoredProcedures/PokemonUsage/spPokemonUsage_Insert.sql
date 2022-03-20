CREATE PROCEDURE [dbo].[spPokemonUsage_Insert]
	@Generation smallint,
	@Format nvarchar(12),

	@PokemonId int,
	@RawCount int,
	@Abilities nvarchar(300),
	@Items nvarchar(800),
	@Spreads nvarchar(800),
	@Moves nvarchar(1950),
	@Teammates nvarchar(800),
	@ChecksAndCounters nvarchar(800)
AS
BEGIN

	INSERT INTO dbo.PokemonUsage(Generation, [Format], PokemonId, RawCount, Abilities, Items, Spreads, Moves, Teammates, ChecksAndCounters)
	VALUES(@Generation, @Format, @PokemonId, @Rawcount, @Abilities, @Items, @Spreads, @Moves, @Teammates, @ChecksAndCounters)

END
