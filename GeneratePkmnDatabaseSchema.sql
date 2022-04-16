USE [master]
GO
/****** Object:  Database [PkmnDatabase]    Script Date: 4/16/2022 5:43:27 PM ******/
CREATE DATABASE [PkmnDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PkmnDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PkmnDatabase_Primary.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PkmnDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PkmnDatabase_Primary.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PkmnDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PkmnDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PkmnDatabase] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [PkmnDatabase] SET ANSI_NULLS ON 
GO
ALTER DATABASE [PkmnDatabase] SET ANSI_PADDING ON 
GO
ALTER DATABASE [PkmnDatabase] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [PkmnDatabase] SET ARITHABORT ON 
GO
ALTER DATABASE [PkmnDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PkmnDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PkmnDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PkmnDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PkmnDatabase] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [PkmnDatabase] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [PkmnDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PkmnDatabase] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [PkmnDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PkmnDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PkmnDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PkmnDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PkmnDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PkmnDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PkmnDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PkmnDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PkmnDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PkmnDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [PkmnDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [PkmnDatabase] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [PkmnDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PkmnDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PkmnDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PkmnDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PkmnDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PkmnDatabase] SET QUERY_STORE = OFF
GO
USE [PkmnDatabase]
GO
/****** Object:  Table [dbo].[Pokemon]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pokemon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Type1] [nchar](10) NOT NULL,
	[Type2] [nchar](10) NULL,
	[Ability1] [nvarchar](20) NOT NULL,
	[Ability2] [nvarchar](20) NULL,
	[HiddenAbility] [nvarchar](20) NULL,
	[Hp] [int] NOT NULL,
	[Att] [int] NOT NULL,
	[Def] [int] NOT NULL,
	[SpAtt] [int] NOT NULL,
	[SpDef] [int] NOT NULL,
	[Spd] [int] NOT NULL,
	[SpriteUrl] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PokemonUsage]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokemonUsage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Generation] [smallint] NOT NULL,
	[Format] [nvarchar](12) NOT NULL,
	[PokemonId] [int] NOT NULL,
	[RawCount] [int] NOT NULL,
	[Abilities] [nvarchar](300) NOT NULL,
	[Items] [nvarchar](800) NOT NULL,
	[Spreads] [nvarchar](800) NOT NULL,
	[Moves] [nvarchar](1950) NOT NULL,
	[Teammates] [nvarchar](800) NULL,
	[ChecksAndCounters] [nvarchar](800) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PokemonUsage]  WITH CHECK ADD FOREIGN KEY([PokemonId])
REFERENCES [dbo].[Pokemon] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_Delete]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_Delete]
	@Id int
AS
BEGIN

DELETE
FROM dbo.Pokemon 
WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_Get]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_Get]
	@id int
AS
BEGIN

SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
FROM dbo.Pokemon
WHERE Id = @id;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_GetAll]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_GetAll]
AS
BEGIN

SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
FROM dbo.Pokemon;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_GetByName]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_GetByName]
	@Name NVARCHAR(25)
AS
BEGIN

	SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
	FROM dbo.Pokemon
	WHERE [Name] = @Name;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_GetByNameSearchLimited]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_GetByNameSearchLimited]
	@SearchString NVARCHAR(25),
	@NumberOfResults int
AS
BEGIN

SELECT TOP (@NumberOfResults) [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
FROM dbo.Pokemon
WHERE [Name] LIKE '%'+@SearchString+'%';

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_GetByNameStringSearch]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_GetByNameStringSearch]
	@SearchString NVARCHAR(25)
AS
BEGIN

	SELECT [Id], [Name], [Type1], [Type2], [Ability1], [Ability2], [HiddenAbility], [Hp], [Att], [Def], [SpAtt], [SpDef], [Spd], [SpriteUrl]
	FROM dbo.Pokemon
	WHERE [Name] LIKE '%'+@SearchString+'%';

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_Insert]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_Insert]
	@Name NVARCHAR(25),
	@Type1 NVARCHAR(10),
	@Type2 NVARCHAR(10) NULL,
	@Ability1 NVARCHAR(20),
	@Ability2 NVARCHAR(20) NULL,
	@HiddenAbility NVARCHAR(20) NULL,
	@Hp INT,
	@Att INT,
	@Def INT,
	@SpAtt INT,
	@SpDef INT,
	@Spd INT,
	@SpriteUrl NVARCHAR(100) NULL
AS
BEGIN

INSERT INTO dbo.Pokemon([Name], Type1, Type2, Ability1, Ability2, HiddenAbility, Hp, Att, Def, SpAtt, SpDef, Spd, SpriteUrl)
VALUES (@Name, @Type1, @Type2, @Ability1, @Ability2, @HiddenAbility, @Hp, @Att, @Def, @SpAtt, @SpDef, @Spd, @SpriteUrl);

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemon_Update]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemon_Update]
	@Id INT,
	@Name nvarchar(25),
	@Type1 nvarchar(10),
	@Type2 nvarchar(10),
	@Ability1 NVARCHAR(20),
	@Ability2 NVARCHAR(20) NULL,
	@HiddenAbility NVARCHAR(20) NULL,
	@Hp INT,
	@Att INT,
	@Def INT,
	@SpAtt INT,
	@SpDef INT, 
	@Spd INT,
	@SpriteUrl NVARCHAR(100) NULL
AS
BEGIN

UPDATE dbo.Pokemon
set [Name]=@Name, Type1=@Type1, Type2=@Type2, Ability1=@Ability1, Ability2=@Ability2, HiddenAbility=@HiddenAbility, Hp=@Hp, Att=@Att, Def=@Def, SpAtt=@SpAtt, SpDef=@SpDef, Spd=@Spd, SpriteUrl=@SpriteUrl
WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_Delete]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemonUsage_Delete]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_Get]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemonUsage_Get]
	@Id int
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
	FROM dbo.PokemonUsage
	WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_GetAll]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemonUsage_GetAll]
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
	FROM dbo.PokemonUsage;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_GetAllByPkmnID]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemonUsage_GetAllByPkmnID]
	@PokemonId int
AS
BEGIN

SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters] 
FROM dbo.PokemonUsage
WHERE PokemonId = @PokemonId;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_GetAllGenAndFormat]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPokemonUsage_GetAllGenAndFormat]
	@Generation SMALLINT,
	@Format nvarchar(12)
AS
BEGIN

	SELECT [Id], [Generation], [Format], [PokemonId], [RawCount], [Abilities], [Items], [Spreads], [Moves], [Teammates], [ChecksAndCounters]
	FROM dbo.PokemonUsage
	WHERE Generation=@Generation AND [Format]=@Format;

END
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_Insert]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[spPokemonUsage_Update]    Script Date: 4/16/2022 5:43:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
USE [master]
GO
ALTER DATABASE [PkmnDatabase] SET  READ_WRITE 
GO
