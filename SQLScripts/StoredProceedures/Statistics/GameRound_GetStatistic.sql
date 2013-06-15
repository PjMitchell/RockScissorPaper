
DELIMITER $$


CREATE PROCEDURE `Proc_Select_RoundStatistics` ()

BEGIN

SELECT r.RoundNumber, r.Rock, s.Scissor, p.Paper
FROM
(
	SELECT RoundNumber,Count(*) as Rock
	FROM gameroundresult as grr
	Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
	Inner Join player as p on p.PlayerId = grr.PlayerId
	Where p.IsBot =0 and SelectionId = 1
	Group by RoundNumber
) 
as r
inner join
(
	Select RoundNumber,Count(*) as Scissor
	FROM gameroundresult as grr
	Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
	Inner Join player as p on p.PlayerId = grr.PlayerId
	Where p.IsBot =0 and SelectionId = 2
	Group by RoundNumber
) 
as s on r.RoundNumber = s.RoundNumber
inner join
(
	Select RoundNumber,Count(*) as Paper
	FROM gameroundresult as grr
	Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId
	Inner Join player as p on p.PlayerId = grr.PlayerId
	Where p.IsBot =0 and SelectionId = 3
	Group by RoundNumber
) 
as p on r.RoundNumber = p.RoundNumber
;

END$$



DELIMITER ;