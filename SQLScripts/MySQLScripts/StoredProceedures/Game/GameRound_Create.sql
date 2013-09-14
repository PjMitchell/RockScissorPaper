DROP PROCEDURE IF EXISTS `GameRound_Create`;

DELIMITER $$



CREATE PROCEDURE `GameRound_Create` (in GameIdInput int, RoundNumberInput int)

BEGIN

INSERT INTO GameRound (RoshamboGameId, RoundNumber)

VALUES (GameIdInput, RoundNumberInput);

SELECT LAST_INSERT_ID();



END$$



DELIMITER ;