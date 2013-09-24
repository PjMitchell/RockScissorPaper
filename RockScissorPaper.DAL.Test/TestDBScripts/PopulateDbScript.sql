INSERT INTO Player(PlayerId, PlayerName, PlayerIPAddress, IsBot, BotType, AvatarImg)
VALUES
(5, 'Peter','1st.fake.add.ress', 0, null, 'Paper1.jpg'),
(6, 'James','2nd.fake.add.ress', 0, null, 'Paper2.jpg'),
(7, 'John', '3rd.fake.add.ress', 0, null, 'Rock2.jpg'),
(8, 'Bill', '4th.fake.add.ress', 0, null, 'Scissor1.jpg')
;

INSERT INTO Roshambogame(RoshamboGameId, StartDate, GameStatus, RuleSet, ButtonOrder)
VALUES
(1,'2013-07-25 16:46:59', 6, 1, 'RSP')
;

INSERT INTO GamePlayer(PlayerId, RoshamboGameId, Position, PlayerGameResult, PlayerGameScore)
VALUES
(5,1,1,1,3),
(6,1,2,3,0)
;

INSERT INTO GameRound (GameRoundId, RoshamboGameId, RoundNumber)
VALUES
(1,1,1),
(2,1,2),
(3,1,3),
(4,1,4),
(5,1,5)
;
INSERT INTO GameRoundResult (GameRoundResultId, PlayerId, RoshamboGameId, GameRoundId, SelectionId)
VALUES
(1,5,1,1,1),(2,6,1,1,3),
(3,5,1,2,2),(4,6,1,2,2),
(5,5,1,3,2),(6,6,1,3,3),
(7,5,1,4,3),(8,6,1,4,1),
(9,5,1,5,1),(10,6,1,5,2)

