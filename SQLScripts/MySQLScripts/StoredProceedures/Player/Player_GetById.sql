DROP PROCEDURE IF EXISTS `Player_GetById`;
DELIMITER $$



CREATE PROCEDURE `Player_GetById` (in PlayerIdInput int)

BEGIN

SELECT * 

FROM player

Where PlayerId = PlayerIdInput;

END$$



DELIMITER ;