DROP PROCEDURE IF EXISTS `GameRound_GetStatistic`;
DELIMITER $$


CREATE PROCEDURE `GameRound_GetStatistic` ()

BEGIN
SELECT RoundNumber,
 SUM(CASE WHEN SelectionId = 1 THEN 1 ELSE 0 END) as Rock,
 SUM(CASE WHEN SelectionId = 2 THEN 1 ELSE 0 END) as Scissor,
 SUM(CASE WHEN SelectionId = 3 THEN 1 ELSE 0 END) as Paper
 FROM gameroundresult as grr
 Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
 Inner Join player as p on p.PlayerId = grr.PlayerId
 Where p.IsBot = 0
 Group by RoundNumber
 ;


END$$



DELIMITER ;