
DELIMITER $$

CREATE PROCEDURE `Proc_Select_RoundStatisticsByRoundNumber` (in RoundNumberInput int)

BEGIN

SELECT SelectionId,Count(*) as Count FROM gameroundresult as grr

Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
Inner Join player as p on p.PlayerId = grr.PlayerId

Where p.IsBot =0 and RoundNumber = RoundNumberInput

Group by SelectionId

;

END$$



DELIMITER ;



