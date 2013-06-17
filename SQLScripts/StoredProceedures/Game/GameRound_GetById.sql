
DROP PROCEDURE IF EXISTS `GameRound_GetById`;

DELIMITER $$


CREATE PROCEDURE `GameRound_GetById` (

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