CREATE PROCEDURE [dbo].[GameRound_Create]
	@GameId int = 0,
	@RoundNumber int
AS
	
INSERT INTO GameRound (RoshamboGameId, RoundNumber)

VALUES (@GameId, @RoundNumber)

RETURN SELECT @@IDENTITY
