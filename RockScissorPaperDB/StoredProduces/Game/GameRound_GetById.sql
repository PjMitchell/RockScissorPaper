CREATE PROCEDURE [dbo].[GameRound_GetById]
	@GameId int,
	@PlayerOneId int,
	@PlayerTwoId int
AS
	
RETURN 
Select gr.GameRoundId, gr.RoundNumber,
(SELECT SelectionId From gameroundresult Where GameRoundId = gr.GameRoundId and PlayerId = @PlayerOneId) as PlayerOneChoice,
(SELECT SelectionId From gameroundresult Where GameRoundId = gr.GameRoundId and PlayerId = @PlayerTwoId) as PlayerTwoChoice

From gameround as gr

WHERE gr.RoshamboGameId = @GameId

