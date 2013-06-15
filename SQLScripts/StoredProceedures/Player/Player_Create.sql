
DELIMITER $$



CREATE PROCEDURE `Proc_Create_NewPlayer` (in PlayerNameInput varchar(25), IpAddressInput varchar(25))

BEGIN

Insert INTO Player(PlayerName, PlayerIPAddress)

Values (PlayerNameInput, IpAddressInput);

Select LAST_INSERT_ID();



END$$



DELIMITER ;

