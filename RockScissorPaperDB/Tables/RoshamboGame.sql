CREATE TABLE [dbo].[RoshamboGame]
(
	[RoshamboGameId] INT NOT NULL Primary Key Identity(1,1),
	[StartDate] Datetime NOT NULL,
	[GameStatus] int NOT NULL,
	[RuleSet] int NOT NULL,
	[ButtonOrder] varchar(10) NOT NULL,
Constraint FK_Roshambo_GameStatus Foreign Key (GameStatus) REFERENCES GameStatus(GameStatusId),
Constraint FK_Roshambo_RuleSet Foreign Key (RuleSet) REFERENCES GameRuleSet(GameRuleSetId)
)
