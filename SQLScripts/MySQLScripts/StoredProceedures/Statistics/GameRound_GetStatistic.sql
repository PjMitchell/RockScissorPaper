DROP PROCEDURE IF EXISTS `GameRound_GetStatistic`;
DELIMITER $$


CREATE PROCEDURE `GameRound_GetStatistic` ()

BEGIN
SELECT RoundNumber,
 SUM(CASE WHEN SelectionId = 1 THEN 1 ELSE 0 END) as Rock,
 SUM(CASE WHEN SelectionId = 2 THEN 1 ELSE 0 END) as Scissor,
 SUM(CASE WHEN SelectionId = 3 THEN 1 ELSE 0 END) as Paper
 FROM roundselectionstatistics
 Group by RoundNumber;


END$$



DELIMITER ;