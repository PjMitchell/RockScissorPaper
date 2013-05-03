
DELIMITER $$


CREATE PROCEDURE `Proc_Create_NewGame` (

in PlayerOneIdInput int,

PlayerTwoIdInput int,

RuleSetIdInput int)

BEGIN

Insert INTO RoshamboGame(StartDate, GameStatus, RuleSet)

Values (UTC_TIMESTAMP(), 1,RuleSetIdInput);

Select LAST_INSERT_ID();

Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)

Values (PlayerOneIdInput, LAST_INSERT_ID(), 1);

Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)

Values (PlayerTwoIdInput, LAST_INSERT_ID(), 2);


END$$



DELIMITER ;

