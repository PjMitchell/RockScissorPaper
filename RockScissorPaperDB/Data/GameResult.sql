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