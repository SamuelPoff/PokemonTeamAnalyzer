CREATE PROCEDURE [dbo].[spPokemon_Delete]
	@Id int
AS
BEGIN

DELETE
FROM dbo.Pokemon 
WHERE Id = @Id;

END
