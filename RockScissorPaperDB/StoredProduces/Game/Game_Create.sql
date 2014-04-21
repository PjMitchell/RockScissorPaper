CREATE PROCEDURE [dbo].[Game_Create]
	@PlayerOneId int,
	@PlayerTwoId int,
	@RuleSetId int,
	@ButtonOrder varchar(10)

AS
	Insert INTO RoshamboGame(StartDate, GameStatus, RuleSet, ButtonOrder)
	Values (GETUTCDATE(), 1, @RuleSetId, @ButtonOrder) ;

	Select @@IDENTITY;

	Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)
	Values (@PlayerOneId, @@IDENTITY, 1);

	Insert INTO GamePlayer(PlayerId, RoshamboGameId, Position)
	Values (@PlayerTwoId, @@IDENTITY, 2);

RETURN 0
