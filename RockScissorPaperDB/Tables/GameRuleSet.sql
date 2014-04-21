CREATE TABLE [dbo].[GameRuleSet]
(
	[GameRuleSetId] INT NOT NULL Identity(1,1) Primary Key,
	[NumberOfRounds] INT NOT NULL,
	[AllowDraw] bit NOT NULL,
	[GameType] varchar(25) NOT NULL
)
