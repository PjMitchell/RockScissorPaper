
DROP PROCEDURE IF EXISTS `Game_Create`;

DELIMITER $$

CREATE PROCEDURE `Game_Create` (

in PlayerOneIdInput int,

PlayerTwoIdInput int,

RuleSetIdInput int,
ButtonOrderInput varchar(10))



BEGIN

Insert INTO RoshamboGame(StartDate, GameStatus, RuleSet, ButtonOrder)
Values (UTC_TIMESTAMP(), 1, RuleSetIdInput, ButtonOrderInput) ;

Select LAST_INSERT_ID();

Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)
Values (PlayerOneIdInput, LAST_INSERT_ID(), 1);

Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)
Values (PlayerTwoIdInput, LAST_INSERT_ID(), 2);


END $$

DELIMITER ;





