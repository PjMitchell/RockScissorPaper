DROP PROCEDURE IF EXISTS `GameRound_GetStatisticSummary`;
DELIMITER $$

CREATE PROCEDURE `GameRound_GetStatisticSummary` ()

BEGIN

SELECT SelectionId, Count(*) as Count FROM gameroundresult as grr
Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
Inner Join player as p on p.PlayerId = grr.PlayerId

Where p.IsBot =0
Group by SelectionId

;

END$$



DELIMITER ;
			
			