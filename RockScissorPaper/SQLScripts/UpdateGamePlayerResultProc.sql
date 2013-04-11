USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Update_GamePlayerResult`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Update_GamePlayerResult` (

in 

PlayerIdInput int,

RoshamboGameIdInput int,

GameOutcomeInput int,

GameScoreInput int)

BEGIN

UPDATE Gameplayer

SET PlayerGameResult = GameOutcomeInput, PlayerGameScore = GameScoreInput

WHERE PlayerId = PlayerIdInput AND RoshamboGameId = RoshamboGameIdInput

;

END$$



DELIMITER ;