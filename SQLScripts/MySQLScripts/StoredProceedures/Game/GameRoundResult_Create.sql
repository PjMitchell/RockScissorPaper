DROP PROCEDURE IF EXISTS `GameRoundResult_Create`;
DELIMITER $$


CREATE PROCEDURE `GameRoundResult_Create` (in PlayerIdInput int, RoshamboGameIdInput int, GameRoundIdInput int, SelectionIdInput int)

BEGIN

INSERT INTO GameRoundResult (PlayerId, RoshamboGameId, GameRoundId, SelectionId)

VALUES (PlayerIdInput, RoshamboGameIdInput, GameRoundIdInput, SelectionIdInput)

;

END$$



DELIMITER ;

