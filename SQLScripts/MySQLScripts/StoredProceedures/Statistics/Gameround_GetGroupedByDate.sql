DROP PROCEDURE IF EXISTS `Gameround_GetGroupedByDate`;
DELIMITER $$


CREATE PROCEDURE `Gameround_GetGroupedByDate` ()

BEGIN 

SELECT Date(game.StartDate) as 'Date',
 SUM(CASE WHEN SelectionId = 1 THEN 1 ELSE 0 END) as Rock,
 SUM(CASE WHEN SelectionId = 2 THEN 1 ELSE 0 END) as Scissor,
 SUM(CASE WHEN SelectionId = 3 THEN 1 ELSE 0 END) as Paper
 FROM gameroundresult as grr
 Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
 Inner Join player as p on p.PlayerId = grr.PlayerId
 Inner Join roshambogame as game on grr.RoshamboGameId = game.RoshamboGameId 
 Where p.IsBot = 0
 Group by Date(game.StartDate)
 Order by Date(game.StartDate)
 ;
END$$



DELIMITER ;