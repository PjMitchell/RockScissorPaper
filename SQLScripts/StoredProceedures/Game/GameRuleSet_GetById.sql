DROP PROCEDURE IF EXISTS `GameRuleSet_GetById`;

DELIMITER $$


CREATE PROCEDURE `GameRuleSet_GetById` (in GameRuleSetIdInput int)
                                               
BEGIN   
                                       
SELECT *
From GameRuleSet
Where GameRuleSetId = GameRuleSetIdInput
;

END$$



DELIMITER ;