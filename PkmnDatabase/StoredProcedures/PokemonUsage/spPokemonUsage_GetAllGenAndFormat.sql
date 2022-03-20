CREATE PROCEDURE [dbo].[spPokemonUsage_GetAllGenAndFormat]
	@Generation SMALLINT,
	@Format nvarchar(12)
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters]
	FROM dbo.PokemonUsage
	WHERE Generation=@Generation AND [Format]=@Format;

END
