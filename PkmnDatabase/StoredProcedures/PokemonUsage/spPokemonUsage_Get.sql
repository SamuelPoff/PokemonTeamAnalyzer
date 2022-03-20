CREATE PROCEDURE [dbo].[spPokemonUsage_Get]
	@Id int
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
	FROM dbo.PokemonUsage
	WHERE Id = @Id;

END
