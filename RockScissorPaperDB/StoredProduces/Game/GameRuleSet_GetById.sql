CREATE PROCEDURE [dbo].[GameRuleSet_GetById]
	@GameRuleSet int
AS
RETURN 
SELECT *
From GameRuleSet
Where GameRuleSetId = @GameRuleSet
