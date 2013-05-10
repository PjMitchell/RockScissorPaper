
DELIMITER $$


CREATE PROCEDURE `Proc_Select_GameRuleById` (in GameRuleSetIdInput int)
                                               
BEGIN   
                                       
SELECT *
From GameRuleSet
Where GameRuleSetId = GameRuleSetIdInput
;

END$$



DELIMITER ;