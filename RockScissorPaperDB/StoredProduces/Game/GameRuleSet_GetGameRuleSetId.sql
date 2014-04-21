CREATE PROCEDURE [dbo].[GameRuleSet_GetGameRuleSetId]
	@GameType varchar(25), @AllowDraw bit, @NumberOfRounds int
AS
	
RETURN 
SELECT GameRuleSetId
From GameRuleSet
Where GameType= @GameType AND NumberOfRounds = @NumberOfRounds AND AllowDraw = @AllowDraw
