
DROP PROCEDURE IF EXISTS `Game_CountActive`;

DELIMITER $$

CREATE PROCEDURE `Game_CountActive` ()

BEGIN

SELECT Count(*)
FROM roshambogame
where GameStatus != 6 && StartDate > DATE_SUB(UTC_TIMESTAMP(), INTERVAL 30 MINUTE)
;

END $$

DELIMITER ;

DROP PROCEDURE IF EXISTS `Game_GetById`;

DELIMITER $$


CREATE PROCEDURE `Game_GetById` (in GameIdInput int)

BEGIN

SELECT rg.*, gpone.PlayerId as PlayerOneId, gpone.PlayerName as PlayerOneName, gptwo.PlayerId as PlayerTwoId, gptwo.PlayerName as PlayerTwoName FROM RoshamboGame as rg

Inner Join Player as gpone on gpone.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = GameIdInput and Position = 1)

Inner Join Player as gptwo on gptwo.PlayerId = (SELECT PlayerId FROM gameplayer where RoshamboGameId = GameIdInput and Position = 2)

Where rg.RoshamboGameId = GameIdInput

;

END$$



DELIMITER ;

DROP PROCEDURE IF EXISTS `GamePlayer_GetBotVsHumanVictoryCount`;

DELIMITER $$


CREATE PROCEDURE `GamePlayer_GetBotVsHumanVictoryCount` ()

BEGIN

SELECT 
(
	SELECT Count(*) 
	FROM gameplayer as gp
	Inner Join Player as p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 1 and PlayerGameResult = 3
)
as BotVictory,
(
	SELECT Count(*) 
	FROM gameplayer as gp
	Inner Join Player as p On gp.PlayerId = p.PlayerId
	WHERE p.IsBot = 0 and PlayerGameResult = 3
)
as HumanVictory
;



END$$



DELIMITER ;

DROP PROCEDURE IF EXISTS `GamePlayer_Update`;
DELIMITER $$



CREATE PROCEDURE `GamePlayer_Update` (

in 
RoshamboGameIdInput int,

PlayerOneIdInput int,

PlayerOneGameOutcomeInput int,

PlayerOneGameScoreInput int,

PlayerTwoIdInput int,

PlayerTwoGameOutcomeInput int,

PlayerTwoGameScoreInput int)

BEGIN
Start Transaction;

UPDATE Gameplayer

SET PlayerGameResult = PlayerOneGameOutcomeInput, PlayerGameScore = PlayerOneGameScoreInput

WHERE PlayerId = PlayerOneIdInput AND RoshamboGameId = RoshamboGameIdInput
;
UPDATE Gameplayer

SET PlayerGameResult = PlayerTwoGameOutcomeInput, PlayerGameScore = PlayerTwoGameScoreInput

WHERE PlayerId = PlayerTwoIdInput AND RoshamboGameId = RoshamboGameIdInput
;
Commit;

END$$



DELIMITER ;

DROP PROCEDURE IF EXISTS `GameRound_Create`;

DELIMITER $$



CREATE PROCEDURE `GameRound_Create` (in GameIdInput int, RoundNumberInput int)

BEGIN

INSERT INTO GameRound (RoshamboGameId, RoundNumber)

VALUES (GameIdInput, RoundNumberInput);

SELECT LAST_INSERT_ID();



END$$



DELIMITER ;


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

DROP PROCEDURE IF EXISTS `GameRoundResult_Create`;
DELIMITER $$


CREATE PROCEDURE `GameRoundResult_Create` (in PlayerIdInput int, RoshamboGameIdInput int, GameRoundIdInput int, SelectionIdInput int)

BEGIN

INSERT INTO GameRoundResult (PlayerId, RoshamboGameId, GameRoundId, SelectionId)

VALUES (PlayerIdInput, RoshamboGameIdInput, GameRoundIdInput, SelectionIdInput)

;

END$$



DELIMITER ;

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


DROP PROCEDURE IF EXISTS `GameStatus_Update`;

DELIMITER $$

CREATE PROCEDURE `GameStatus_Update` (in GameIdInput int, NewStatusInput int)

BEGIN

UPDATE RoshamboGame

SET GameStatus= NewStatusInput

WHERE RoshamboGameId = GameIdInput;

END$$



DELIMITER ;

