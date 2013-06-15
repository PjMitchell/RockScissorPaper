
DELIMITER $$



CREATE PROCEDURE `Proc_Select_PlayerById` (in PlayerIdInput int)

BEGIN

SELECT * 

FROM player

Where PlayerId = PlayerIdInput;

END$$



DELIMITER ;