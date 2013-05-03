USE `joelmitc_petertestdatabase`;

DROP procedure IF EXISTS `Proc_Select_GameRoundByGameIdAndPlayerIds`;



DELIMITER $$

USE `joelmitc_petertestdatabase`$$

CREATE PROCEDURE `joelmitc_petertestdatabase`.`Proc_Select_GameRoundByGameIdAndPlayerIds` (

in GameIdInput int,

PlayerOneIdInput int,

PlayerTwoIdInput int

)

BEGIN

Select gr.GameRoundId,

gr.RoundNumber,

(SELECT SelectionId From gameroundresult Where GameRoundId = gr.GameRoundId and PlayerId = PlayerOneIdInput) as PlayerOneChoice,

(SELECT SelectionId From gameroundresult Where GameRoundId = gr.GameRoundId and PlayerId = PlayerTwoIdInput) as PlayerTwoChoice

From gameround as gr

WHERE gr.RoshamboGameId = GameIdInput

;

END$$



DELIMITER ;