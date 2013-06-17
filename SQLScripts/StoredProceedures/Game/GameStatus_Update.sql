
DROP PROCEDURE IF EXISTS `GameStatus_Update`;

DELIMITER $$

CREATE PROCEDURE `GameStatus_Update` (in GameIdInput int, NewStatusInput int)

BEGIN

UPDATE RoshamboGame

SET GameStatus= NewStatusInput

WHERE RoshamboGameId = GameIdInput;

END$$



DELIMITER ;

