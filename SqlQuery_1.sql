/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

CREATE TABLE #GameResult
(
	[GameResultId] int NOT NULL,
	[GameResultText] varchar(25) NOT NULL
)

INSERT INTO #GameResult(GameResultId, GameResultText)
VALUES
(1, 'Lose'),
(2, 'Draw'),
(3, 'Win')

MERGE [GameResult] dst
USING #GameResult src
ON (src.[GameResultId] = dst.[GameResultId])
WHEN MATCHED THEN
UPDATE SET dst.[GameResultText] = src.[GameResultText]
WHEN NOT MATCHED THEN
INSERT VALUES (src.[GameResultId], src.[GameResultText])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

DROP TABLE #GameResult
CREATE TABLE #GameRuleSet
(
	[GameRuleSetId] INT NOT NULL,
	[NumberOfRounds] INT NOT NULL,
	[AllowDraw] bit NOT NULL,
	[GameType] varchar(25) NOT NULL
)

INSERT INTO #GameRuleSet (GameRuleSetId, GameType, AllowDraw, NumberOfRounds)
VALUES
(1, 'StandardGame',  1, 5)

SET IDENTITY_INSERT GameRuleSet ON
MERGE GameRuleSet dst
USING #GameRuleSet src
ON (src.GameRuleSetId = dst.GameRuleSetId)
WHEN MATCHED THEN
UPDATE SET dst.GameType = src.GameType, dst.AllowDraw = src.AllowDraw, dst.NumberOfRounds = src.NumberOfRounds
WHEN NOT MATCHED THEN
INSERT VALUES (src.GameRuleSetId, src.GameType, src.AllowDraw, src.NumberOfRounds)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
SET IDENTITY_INSERT GameRuleSet OFF
DROP TABLE #GameRuleSet
CREATE TABLE #GameStatus
(
	[GameStatusId] INT NOT NULL, 
    [GameStatusText] NVARCHAR(25) NOT NULL
)

INSERT INTO #GameStatus(GameStatusId, GameStatusText)
VALUES
(1, 'New Round'),
(2, 'Waiting Player One'),
(3, 'Waiting Player Two'),
(4,'Round Result'),
(5,'Final Round Result'),
(6, 'End Of Game');

MERGE GameStatus dst
USING #GameStatus src
ON (src.GameStatusId = dst.GameStatusId)
WHEN MATCHED THEN
UPDATE SET dst.GameStatusText = src.GameStatusText
WHEN NOT MATCHED THEN
INSERT VALUES (src.GameStatusId, src.GameStatusText)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

DROP TABLE #GameStatus
If ((Select Count(*) From Player) = 0 )
BEGIN
INSERT INTO Player(PlayerName, PlayerIPAddress, IsBot, BotType, AvatarImg)
VALUES
('Jack','IsBot',1, 'SimpleBot', 'BlueBot.jpg'),
('ROCK BOT','IsBot',1, 'RockBot', 'BlueBot.jpg'),
('Jimbo', 'IsBot', 1, 'SimpleBot', 'GreenBot.jpg'),
('Timmy', 'IsBot', 1, 'SimpleBot', 'PurpleBot.jpg')
END
CREATE TABLE #RoshamboSelection
(
	[SelectionId] int PRIMARY KEY NOT NULL,
	[RoshamboSelectionText] varchar(25) NOT NULL
)

INSERT INTO #RoshamboSelection(SelectionId, RoshamboSelectionText)
VALUES
(1, 'Rock'),
(2, 'Scissor'),
(3, 'Paper')

MERGE [RoshamboSelection] dst
USING #RoshamboSelection src
ON (src.[SelectionId] = dst.[SelectionId])
WHEN MATCHED THEN
UPDATE SET dst.[RoshamboSelectionText] = src.[RoshamboSelectionText]
WHEN NOT MATCHED THEN
INSERT VALUES (src.[SelectionId], src.[RoshamboSelectionText])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

DROP TABLE #RoshamboSelection

GO

GO
PRINT N'Update complete.';


GO
