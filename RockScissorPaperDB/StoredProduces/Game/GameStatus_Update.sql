CREATE PROCEDURE [dbo].[GameStatus_Update]
	@GameId int,
	@NewStatus int
AS
RETURN 
UPDATE RoshamboGame
SET GameStatus= @NewStatus
WHERE RoshamboGameId = @GameId