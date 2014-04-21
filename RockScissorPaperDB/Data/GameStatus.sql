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