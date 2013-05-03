
DELIMITER $$


CREATE PROCEDURE `Proc_Create_GameRoundResult` (in PlayerIdInput int, RoshamboGameIdInput int, GameRoundIdInput int, SelectionIdInput int)

BEGIN

INSERT INTO GameRoundResult (PlayerId, RoshamboGameId, GameRoundId, SelectionId)

VALUES (PlayerIdInput, RoshamboGameIdInput, GameRoundIdInput, SelectionIdInput)

;

END$$



DELIMITER ;

