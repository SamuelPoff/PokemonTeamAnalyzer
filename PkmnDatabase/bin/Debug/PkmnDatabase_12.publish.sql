﻿/*
Deployment script for PkmnDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "PkmnDatabase"
:setvar DefaultFilePrefix "PkmnDatabase"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering Procedure [dbo].[spPokemon_Insert]...';


GO
ALTER PROCEDURE [dbo].[spPokemon_Insert]
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
	@Spd INT
AS
BEGIN

INSERT INTO dbo.Pokemon([Name], Type1, Type2, Ability1, Ability2, HiddenAbility, Hp, Att, Def, SpAtt, SpDef, Spd)
VALUES (@Name, @Type1, @Type2, @Ability1, @Ability2, @HiddenAbility, @Hp, @Att, @Def, @SpAtt, @SpDef, @Spd);

END
GO
PRINT N'Altering Procedure [dbo].[spPokemon_Update]...';


GO
ALTER PROCEDURE [dbo].[spPokemon_Update]
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
	@Spd INT
AS
BEGIN

UPDATE dbo.Pokemon
set [Name]=@Name, Type1=@Type1, Type2=@Type2, Ability1=@Ability1, Ability2=@Ability2, HiddenAbility=@HiddenAbility, Hp=@Hp, Att=@Att, Def=@Def, SpAtt=@SpAtt, SpDef=@SpDef, Spd=@Spd
WHERE Id = @Id;

END
GO
PRINT N'Update complete.';


GO
