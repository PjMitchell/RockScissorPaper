
DELIMITER $$

CREATE PROCEDURE `Proc_Update_GameStatus` (in GameIdInput int, NewStatusInput int)

BEGIN

UPDATE RoshamboGame

SET GameStatus= NewStatusInput

WHERE RoshamboGameId = GameIdInput;

END$$



DELIMITER ;

