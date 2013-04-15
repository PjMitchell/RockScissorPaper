USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Select_RoundStatistics`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Select_RoundStatistics` (in RoundNumberInput int)

BEGIN

SELECT SelectionId,Count(*) as Count FROM gameroundresult as grr

Inner Join gameround as gr on grr.GameRoundId = gr.GameRoundId

Where PlayerId !=2 and RoundNumber = RoundNumberInput

Group by SelectionId

;

END$$



DELIMITER ;



