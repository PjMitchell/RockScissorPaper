CREATE TABLE [dbo].[GamePlayer]
(
	[PlayerId] int  NOT NULL,
	[RoshamboGameId] int NOT NULL,
	[Position] int NOT NULL,
	[PlayerGameResult] int,
	[PlayerGameScore] int,
	CONSTRAINT PK_GamePlayerId PRIMARY KEY (PlayerId,RoshamboGameId),
	CONSTRAINT FK_GamePlayer_PlayerId  FOREIGN KEY (PlayerId)  REFERENCES Player(PlayerId),
	CONSTRAINT FK_GamePlayer_RoshamboGameId  FOREIGN KEY (RoshamboGameId)  REFERENCES RoshamboGame(RoshamboGameId),
	CONSTRAINT FK_GamePlayer_GameResult  FOREIGN KEY ([PlayerGameResult])  REFERENCES GameResult(GameResultId)
)
