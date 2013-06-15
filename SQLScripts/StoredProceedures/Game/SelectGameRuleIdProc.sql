
DELIMITER $$


CREATE PROCEDURE `Proc_Select_GameRuleId` (in GameTypeInput varchar(25),
	 ButtonOrderInput varchar(25),
	 AllowDrawInput bit,
	 NumberOfRoundsInput int
 )
                                               
BEGIN   
                                       
SELECT GameRuleSetId
From GameRuleSet
Where GameType= GameTypeInput AND ButtonOrder = ButtonOrderInput AND NumberOfRounds = NumberOfRounds AND AllowDraw = AllowDrawInput
;

END$$



DELIMITER ;