CREATE PROCEDURE [dbo].[spPokemonUsage_GetAll]
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
	FROM dbo.PokemonUsage;

END
