CREATE PROCEDURE [dbo].[GameRound_GetStatistic]
AS

RETURN 
SELECT RoundNumber,
 SUM(CASE WHEN SelectionId = 1 THEN 1 ELSE 0 END) as Rock,
 SUM(CASE WHEN SelectionId = 2 THEN 1 ELSE 0 END) as Scissor,
 SUM(CASE WHEN SelectionId = 3 THEN 1 ELSE 0 END) as Paper
 FROM RoundSelectionStatistics
 Group by RoundNumber;