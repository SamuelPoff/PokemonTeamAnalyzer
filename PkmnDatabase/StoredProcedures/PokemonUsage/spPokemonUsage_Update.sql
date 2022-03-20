CREATE PROCEDURE [dbo].[spPokemonUsage_Update]
	@Id int,
	
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
	
	UPDATE dbo.PokemonUsage
	SET [Generation]=@Generation, [Format]=@Format, [PokemonId]=@PokemonId, [RawCount]=@RawCount, [Abilities]=@Abilities, [Items]=@Items, [Spreads]=@Spreads, [Moves]=@Moves, [Teammates]=@Teammates, [ChecksAndCounters]=@ChecksAndCounters
	WHERE Id=@Id;

END
