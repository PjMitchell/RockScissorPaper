USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Select_BotVsHumanVictoryCount`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Select_BotVsHumanVictoryCount` ()

BEGIN

SELECT 
(
	SELECT Count(*) 
	FROM joelmitc_petertestdatabase.gameplayer
	WHERE PlayerId = 2 and PlayerGameResult = 3
)
as BotVictory,
(
	SELECT Count(*) 
	FROM joelmitc_petertestdatabase.gameplayer
	WHERE PlayerId != 2 and PlayerGameResult = 3
)
as HumanVictory
;



END$$



DELIMITER ;