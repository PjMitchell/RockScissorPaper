USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Create_NewPlayer`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Create_NewPlayer` (in PlayerNameInput varchar(25), IpAddressInput varchar(25))

BEGIN

Insert INTO Player(PlayerName, PlayerIPAddress)

Values (PlayerNameInput, IpAddressInput);

Select LAST_INSERT_ID();



END$$



DELIMITER ;

