
DROP PROCEDURE IF EXISTS `Game_GetById`;

DELIMITER $$


CREATE PROCEDURE `Game_GetById` (in GameIdInput int)

BEGIN

SELECT rg.*, gpone.PlayerId as PlayerOneId, gpone.PlayerName as PlayerOneName, gptwo.PlayerId as PlayerTwoId, gptwo.PlayerName as PlayerTwoName FROM RoshamboGame as rg

Inner Join Player as gpone on gpone.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = GameIdInput and Position = 1)

Inner Join Player as gptwo on gptwo.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = GameIdInput and Position = 2)

Where rg.RoshamboGameId = GameIdInput

;

END$$



DELIMITER ;