USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Update_GameStatus`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `Proc_Update_GameStatus` (in GameIdInput int, NewStatusInput int)

BEGIN

UPDATE RoshamboGame

SET GameStatus= NewStatusInput

WHERE RoshamboGameId = GameIdInput;

END$$



DELIMITER ;

