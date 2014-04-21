CREATE PROCEDURE [dbo].[GameRoundResult_Create]
	@PlayerId int, @RoshamboGameId int, @GameRoundId int, @SelectionId int
AS
INSERT INTO GameRoundResult (PlayerId, RoshamboGameId, GameRoundId, SelectionId)
VALUES (@PlayerId, @RoshamboGameId, @GameRoundId, @SelectionId)
