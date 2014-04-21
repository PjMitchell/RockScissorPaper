CREATE TABLE [dbo].[GameRound]
(
	[GameRoundId] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[RoshamboGameId] int NOT NULL,
	[RoundNumber] int NOT NULL,
	Constraint FK_GameRound_RoshamboGameId FOREIGN KEY ([RoshamboGameId]) REFERENCES RoshamboGame(RoshamboGameId)
)
