DROP PROCEDURE IF EXISTS `GamePlayer_Update`;
DELIMITER $$



CREATE PROCEDURE `GamePlayer_Update` (

in 
RoshamboGameIdInput int,

PlayerOneIdInput int,

PlayerOneGameOutcomeInput int,

PlayerOneGameScoreInput int,

PlayerTwoIdInput int,

PlayerTwoGameOutcomeInput int,

PlayerTwoGameScoreInput int)

BEGIN
Start Transaction;

UPDATE Gameplayer

SET PlayerGameResult = PlayerOneGameOutcomeInput, PlayerGameScore = PlayerOneGameScoreInput

WHERE PlayerId = PlayerOneIdInput AND RoshamboGameId = RoshamboGameIdInput
;
UPDATE Gameplayer

SET PlayerGameResult = PlayerTwoGameOutcomeInput, PlayerGameScore = PlayerTwoGameScoreInput

WHERE PlayerId = PlayerTwoIdInput AND RoshamboGameId = RoshamboGameIdInput
;
Commit;

END$$



DELIMITER ;