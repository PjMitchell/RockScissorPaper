
DROP PROCEDURE IF EXISTS `Game_CountActive`;

DELIMITER $$

CREATE PROCEDURE `Game_CountActive` ()

BEGIN

SELECT Count(*)
FROM roshambogame
where GameStatus != 6 && StartDate > DATE_SUB(UTC_TIMESTAMP(), INTERVAL 30 MINUTE)
;

END $$

DELIMITER ;





