DROP PROCEDURE IF EXISTS `GameRuleSet_GetGameRuleSetId`;
DELIMITER $$


CREATE PROCEDURE `GameRuleSet_GetGameRuleSetId` (in GameTypeInput varchar(25),
	 AllowDrawInput bit,
	 NumberOfRoundsInput int
 )
                                               
BEGIN   
                                       
SELECT GameRuleSetId
From GameRuleSet
Where GameType= GameTypeInput AND NumberOfRounds = NumberOfRounds AND AllowDraw = AllowDrawInput
;

END$$



DELIMITER ;