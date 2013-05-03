
DELIMITER $$


CREATE PROCEDURE `Proc_Select_GameTotalPlayed` ()

BEGIN

SELECT Count(*)

FROM roshambogame

WHERE GameStatus = 5

;

END$$



DELIMITER ;