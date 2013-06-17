DROP PROCEDURE IF EXISTS `Player_Create`;
DELIMITER $$



CREATE PROCEDURE `Player_Create` (in PlayerNameInput varchar(25), IpAddressInput varchar(25))

BEGIN

Insert INTO Player(PlayerName, PlayerIPAddress)

Values (PlayerNameInput, IpAddressInput);

Select LAST_INSERT_ID();



END$$



DELIMITER ;

