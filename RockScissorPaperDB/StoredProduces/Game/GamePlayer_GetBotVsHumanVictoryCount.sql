CREATE PROCEDURE [dbo].[GamePlayer_GetBotVsHumanVictoryCount]
AS

RETURN SELECT 
(
	SELECT Count(*) 
	FROM gameplayer AS gp
	Inner Join Player AS p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 1 and PlayerGameResult = 3
)
AS BotVictory,
(
	SELECT Count(*) 
	FROM gameplayer AS gp
	Inner Join Player AS p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 0 and PlayerGameResult = 3
)
AS HumanVictory
GO