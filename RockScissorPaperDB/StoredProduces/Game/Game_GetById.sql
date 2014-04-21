CREATE PROCEDURE [dbo].[Game_GetById]
	@GameId int
AS
	
RETURN SELECT rg.*, gpone.PlayerId as PlayerOneId, gpone.PlayerName as PlayerOneName, gptwo.PlayerId as PlayerTwoId, gptwo.PlayerName as PlayerTwoName FROM RoshamboGame as rg

Inner Join Player as gpone on gpone.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = @GameId and Position = 1)

Inner Join Player as gptwo on gptwo.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = @GameId and Position = 2)

Where rg.RoshamboGameId = @GameId


