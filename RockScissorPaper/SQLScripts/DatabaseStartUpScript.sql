START TRANSACTION;

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
(5, 'End Of Game');

CREATE TABLE GameRuleSet
(
GameRuleSetId int NOT NULL AUTO_INCREMENT,
Description varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (GameRuleSetId)
)
;

INSERT INTO GameRuleSet (GameRuleSetId, Description)
VALUES
(1, 'Standard Game')
;


CREATE TABLE RoshamboGame
(
RoshamboGameId int NOT NULL AUTO_INCREMENT,
StartDate Datetime NOT NULL,
GameStatus int NOT NULL,
RuleSet int NOT NULL,
PRIMARY KEY (RoshamboGameId),
FOREIGN KEY (GameStatus) REFERENCES GameStatus(GameStatusId),
FOREIGN KEY (RuleSet) REFERENCES GameRuleSet(GameRuleSetId)
)
;

CREATE TABLE Player
(
PlayerId int NOT NULL AUTO_INCREMENT,
PlayerName  varchar(25) character set UTF8 NOT NULL,
PlayerIPAddress varchar(25) character set UTF8 NOT NULL,
PRIMARY KEY (PlayerId)
)
;

CREATE TABLE GamePlayer
(
PlayerId int NOT NULL,
RoshamboGameId int NOT NULL,
Position int NOT NULL,
PRIMARY KEY (PlayerId,RoshamboGameId),
FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),
FOREIGN KEY (RoshamboGameId) REFERENCES RoshamboGame(RoshamboGameId)
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

COMMIT;