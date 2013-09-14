CREATE TABLE GameStatus
(
GameStatusId int PRIMARY KEY,
GameStatusText varchar(25) NOT NULL
)
;

INSERT INTO GameStatus(GameStatusId, GameStatusText)
VALUES
(1, 'New Round'),
(2, 'Waiting Player One'),
(3, 'Waiting Player Two'),
(4,'Round Result'),
(5,'Final Round Result'),
(6, 'End Of Game');

CREATE TABLE GameRuleSet
(
GameRuleSetId int PRIMARY KEY,
NumberOfRounds int NOT NULL,
AllowDraw bit NOT NULL,
GameType varchar(25) NOT NULL
)
;

INSERT INTO GameRuleSet (GameRuleSetId, GameType, AllowDraw, NumberOfRounds)
VALUES
(1, 'StandardGame',  1, 5)
;


CREATE TABLE RoshamboGame
(
RoshamboGameId int IDENTITY(1,1) PRIMARY KEY,
StartDate Datetime NOT NULL,
GameStatus int FOREIGN KEY REFERENCES GameStatus(GameStatusId) NOT NULL,
RuleSet int REFERENCES GameRuleSet(GameRuleSetId) NOT NULL,
ButtonOrder varchar(10)
)
;

CREATE TABLE Player
(
PlayerId int IDENTITY(1,1) PRIMARY KEY,
PlayerName  varchar(25) NOT NULL,
PlayerIPAddress varchar(45) NOT NULL,
AvatarImg varchar(25) NOT NULL DEFAULT 'Rock1.jpg',
IsBot bit NOT NULL DEFAULT 0, 
BotType varchar(25)
)
;
INSERT INTO Player(PlayerName, PlayerIPAddress, IsBot, BotType, AvatarImg)
VALUES
('Jack','IsBot',1, 'SimpleBot', 'BlueBot.jpg'),
('ROCK BOT','IsBot',1, 'RockBot', 'BlueBot.jpg'),
('Jimbo', 'IsBot', 1, 'SimpleBot', 'GreenBot.jpg'),
('Timmy', 'IsBot', 1, 'SimpleBot', 'PurpleBot.jpg')
;

CREATE TABLE GameResult
(
GameResultId int PRIMARY KEY NOT NULL,
GameResultText varchar(25)
)
;
INSERT INTO GameResult(GameResultId, GameResultText)
VALUES
(1, 'Lose'),
(2, 'Draw'),
(3, 'Win')
;

CREATE TABLE GamePlayer
(
PlayerId int FOREIGN KEY REFERENCES Player(PlayerId) NOT NULL,
RoshamboGameId int FOREIGN KEY REFERENCES RoshamboGame(RoshamboGameId) NOT NULL,
Position int NOT NULL,
PlayerGameResult int FOREIGN KEY REFERENCES GameResult(GameResultId),
PlayerGameScore int,
CONSTRAINT pk_GamePlayerId PRIMARY KEY (PlayerId,RoshamboGameId)
)
;


CREATE TABLE RoshamboSelection
(
SelectionId int PRIMARY KEY NOT NULL,
RoshamboSelectionText varchar(25) NOT NULL
)
;

INSERT INTO RoshamboSelection(SelectionId, RoshamboSelectionText)
VALUES
(1, 'Rock'),
(2, 'Scissor'),
(3, 'Paper')
;

CREATE TABLE GameRound
(
GameRoundId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
RoshamboGameId int FOREIGN KEY REFERENCES RoshamboGame(RoshamboGameId) NOT NULL,
RoundNumber int NOT NULL
)
;

CREATE TABLE GameRoundResult
(
GameRoundResultId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
PlayerId int FOREIGN KEY REFERENCES Player(PlayerId) NOT NULL,
RoshamboGameId int FOREIGN KEY REFERENCES RoshamboGame(RoshamboGameId) NOT NULL,
GameRoundId int FOREIGN KEY REFERENCES GameRound(GameRoundId) NOT NULL,
SelectionId int FOREIGN KEY REFERENCES RoshamboSelection(SelectionId) NOT NULL
)
;
