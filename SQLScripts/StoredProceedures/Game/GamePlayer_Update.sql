DROP PROCEDURE IF EXISTS `GamePlayer_Update`;
DELIMITER $$



CREATE PROCEDURE `GamePlayer_Update` (

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