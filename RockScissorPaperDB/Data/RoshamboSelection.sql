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