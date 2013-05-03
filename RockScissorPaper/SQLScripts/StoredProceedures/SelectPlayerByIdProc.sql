USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Select_PlayerById`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Select_PlayerById` (in PlayerIdInput int)

BEGIN

SELECT * 

FROM player

Where PlayerId = PlayerIdInput;

END$$



DELIMITER ;