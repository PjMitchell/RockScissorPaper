CREATE TABLE [dbo].[GameRoundResult]
(
	[GameRoundResultId] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PlayerId] int  NOT NULL,
	[RoshamboGameId] int NOT NULL,
	[GameRoundId] int NOT NULL,
	[SelectionId] int NOT NULL,
	Constraint FK_GameRoundResult_PlayerId FOREIGN KEY ([PlayerId]) REFERENCES Player(PlayerId),
	Constraint FK_GameRoundResult_RoshamboGameId FOREIGN KEY ([RoshamboGameId]) REFERENCES RoshamboGame(RoshamboGameId),
	Constraint FK_GameRoundResult_GameRoundId FOREIGN KEY ([GameRoundId]) REFERENCES GameRound(GameRoundId),
	Constraint FK_GameRoundResult_SelectionId FOREIGN KEY ([SelectionId]) REFERENCES RoshamboSelection(SelectionId)

)
