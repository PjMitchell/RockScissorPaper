DELIMITER $$



CREATE PROCEDURE `Proc_Create_GameRound` (in GameIdInput int, RoundNumberInput int)

BEGIN

INSERT INTO GameRound (RoshamboGameId, RoundNumber)

VALUES (GameIdInput, RoundNumberInput);

SELECT LAST_INSERT_ID();



END$$



DELIMITER ;