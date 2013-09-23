CREATE TABLE GameStatus
(
GameStatusId int NOT NULL,
GameStatusText varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (GameStatusId)
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
GameRuleSetId int NOT NULL AUTO_INCREMENT,
NumberOfRounds int NOT NULL,
AllowDraw bit NOT NULL,
GameType varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (GameRuleSetId)
)
;

INSERT INTO GameRuleSet (GameRuleSetId, GameType, AllowDraw, NumberOfRounds)
VALUES
(1, 'StandardGame',  1, 5)
;


CREATE TABLE RoshamboGame
(
RoshamboGameId int NOT NULL AUTO_INCREMENT,
StartDate Datetime NOT NULL,
GameStatus int NOT NULL,
RuleSet int NOT NULL,
ButtonOrder varchar(10) character set UTF8 NOT NULL,
PRIMARY KEY (RoshamboGameId),
FOREIGN KEY (GameStatus) REFERENCES GameStatus(GameStatusId),
FOREIGN KEY (RuleSet) REFERENCES GameRuleSet(GameRuleSetId)
)
;

CREATE TABLE Player
(
PlayerId int NOT NULL AUTO_INCREMENT,
PlayerName  varchar(25) character set UTF8 NOT NULL,
PlayerIPAddress varchar(45) character set UTF8 NOT NULL,
AvatarImg varchar(25) character set UTF8 NOT NULL DEFAULT 'Rock1.jpg',
IsBot bit NOT NULL DEFAULT 0, 
BotType varchar(25) character set UTF8,
PRIMARY KEY (PlayerId)
)
;
INSERT INTO Player(PlayerId, PlayerName, PlayerIPAddress, IsBot, BotType, AvatarImg)
VALUES
(1, 'Jack','IsBot',1, 'SimpleBot', 'BlueBot.jpg'),
(2, 'ROCK BOT','IsBot',1, 'RockBot', 'BlueBot.jpg'),
(3, 'Jimbo', 'IsBot', 1, 'SimpleBot', 'GreenBot.jpg'),
(4, 'Timmy', 'IsBot', 1, 'SimpleBot', 'PurpleBot.jpg')
;

CREATE TABLE GameResult
(
GameResultId int NOT NULL,
GameResultText varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (GameResultId)
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
PlayerId int NOT NULL,
RoshamboGameId int NOT NULL,
Position int NOT NULL,
PlayerGameResult int,
PlayerGameScore int,
PRIMARY KEY (PlayerId,RoshamboGameId),
FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),
FOREIGN KEY (RoshamboGameId) REFERENCES RoshamboGame(RoshamboGameId),
FOREIGN KEY (PlayerGameResult) REFERENCES GameResult(GameResultId)
)
;



CREATE TABLE RoshamboSelection
(
SelectionId int NOT NULL,
RoshamboSelectionText varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (SelectionId)
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
GameRoundId int NOT NULL AUTO_INCREMENT,
RoshamboGameId int NOT NULL,
RoundNumber int NOT NULL,
PRIMARY KEY (GameRoundId ),
FOREIGN KEY (RoshamboGameId) REFERENCES RoshamboGame(RoshamboGameId)
)
;

CREATE TABLE GameRoundResult
(
GameRoundResultId int NOT NULL AUTO_INCREMENT,
PlayerId int NOT NULL,
RoshamboGameId int NOT NULL,
GameRoundId int NOT NULL,
SelectionId int NOT NULL,
PRIMARY KEY (GameRoundResultId ),
FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),
FOREIGN KEY (RoshamboGameId) REFERENCES RoshamboGame(RoshamboGameId),
FOREIGN KEY (GameRoundId) REFERENCES GameRound(GameRoundId),
FOREIGN KEY (SelectionId) REFERENCES RoshamboSelection(SelectionId)
)
;



