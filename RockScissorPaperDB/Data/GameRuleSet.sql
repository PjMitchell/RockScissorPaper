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
INSERT (GameRuleSetId, GameType, AllowDraw, NumberOfRounds) VALUES (src.GameRuleSetId, src.GameType, src.AllowDraw, src.NumberOfRounds)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
SET IDENTITY_INSERT GameRuleSet OFF
DROP TABLE #GameRuleSet