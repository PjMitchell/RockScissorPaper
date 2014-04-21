CREATE VIEW [dbo].[RoundSelectionStatistics]
AS 
SELECT SelectionId, RoundNumber, StartDate, ButtonOrder
FROM gameroundresult as grr
Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
Inner Join player as p on p.PlayerId = grr.PlayerId
Inner Join roshambogame as game on grr.RoshamboGameId = game.RoshamboGameId 
Where p.IsBot = 0