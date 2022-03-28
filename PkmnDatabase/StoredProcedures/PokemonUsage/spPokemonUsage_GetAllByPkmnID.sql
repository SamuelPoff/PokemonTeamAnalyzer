CREATE PROCEDURE [dbo].[spPokemonUsage_GetAllByPkmnID]
	@PokemonId int
AS
BEGIN

SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
FROM dbo.PokemonUsage
WHERE PokemonId = @PokemonId;

END
