DROP PROCEDURE IF EXISTS `Game_GetGamesPlayed`;
DELIMITER $$


CREATE PROCEDURE `Game_GetGamesPlayed` ()

BEGIN

SELECT Count(*)

FROM roshambogame

WHERE GameStatus = 6

;

END$$



DELIMITER ;