CREATE PROCEDURE [dbo].[GamePlayer_Update]
	@RoshamboGameId int,
	@PlayerOneId int,
	@PlayerOneGameOutcome int,
	@PlayerOneGameScore int,
	@PlayerTwoId int,
	@PlayerTwoGameOutcome int,
	@PlayerTwoGameScore int
AS
Begin Transaction;

UPDATE Gameplayer

SET PlayerGameResult = @PlayerOneGameOutcome, PlayerGameScore = @PlayerOneGameScore

WHERE PlayerId = @PlayerOneId AND RoshamboGameId = @RoshamboGameId
;
UPDATE Gameplayer

SET PlayerGameResult = @PlayerTwoGameOutcome, PlayerGameScore = @PlayerTwoGameScore

WHERE PlayerId = @PlayerTwoId AND RoshamboGameId = @RoshamboGameId
;
Commit;

