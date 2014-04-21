CREATE PROCEDURE [dbo].[GameRound_GetStatisticByRoundNumber]
	@RoundNumber int
AS
RETURN 
SELECT SelectionId,Count(*) as Count FROM GameRoundResult as grr
Inner Join GameRound as gr on grr.GameRoundId = gr.GameRoundId
Inner Join Player as p on p.PlayerId = grr.PlayerId
Where p.IsBot =0 and RoundNumber = @RoundNumber

Group by SelectionId