CREATE TABLE [dbo].[PokemonUsage]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Generation] SMALLINT NOT NULL,
	[Format] NVARCHAR(12) NOT NULL,

	[PokemonId] INT NOT NULL FOREIGN KEY REFERENCES Pokemon(Id),
	[RawCount] INT NOT NULL,
	[Abilities] NVARCHAR(300) NOT NULL,
	[Items] NVARCHAR(800) NOT NULL,
	[Spreads] NVARCHAR(800) NOT NULL,
	[Moves] NVARCHAR(1950) NOT NULL,
	[Teammates] NVARCHAR(800) NULL,
	[ChecksAndCounters] NVARCHAR(800) NULL
)
