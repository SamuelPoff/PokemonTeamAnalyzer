CREATE TABLE [dbo].[Pokemon]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(25) NOT NULL, 
    [Type1] NCHAR(10) NOT NULL, 
    [Type2] NCHAR(10) NULL, 
    [Hp] INT NOT NULL, 
    [Att] INT NOT NULL, 
    [Def] INT NOT NULL, 
    [SpAtt] INT NOT NULL, 
    [SpDef] INT NOT NULL, 
    [Spd] INT NOT NULL
)
