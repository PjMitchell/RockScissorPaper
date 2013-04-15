USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Select_GameTotal`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Select_GameTotal` ()

BEGIN

SELECT Count(*)

FROM roshambogame

WHERE GameStatus = 5

;

END$$



DELIMITER ;