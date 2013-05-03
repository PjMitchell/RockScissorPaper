
DELIMITER $$



CREATE PROCEDURE `Proc_Update_GamePlayerResult` (

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