DROP PROCEDURE IF EXISTS `GamePlayer_GetBotVsHumanVictoryCount`;

DELIMITER $$


CREATE PROCEDURE `GamePlayer_GetBotVsHumanVictoryCount` ()

BEGIN

SELECT 
(
	SELECT Count(*) 
	FROM gameplayer as gp
	Inner Join Player as p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 1 and PlayerGameResult = 3
)
as BotVictory,
(
	SELECT Count(*) 
	FROM gameplayer as gp
	Inner Join Player as p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 0 and PlayerGameResult = 3
)
as HumanVictory
;



END$$



DELIMITER ;