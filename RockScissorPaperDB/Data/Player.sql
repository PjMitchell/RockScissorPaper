If ((Select Count(*) From Player) = 0 )
BEGIN
INSERT INTO Player(PlayerName, PlayerIPAddress, IsBot, BotType, AvatarImg)
VALUES
('Jack','IsBot',1, 'SimpleBot', 'BlueBot.jpg'),
('ROCK BOT','IsBot',1, 'RockBot', 'BlueBot.jpg'),
('Jimbo', 'IsBot', 1, 'SimpleBot', 'GreenBot.jpg'),
('Timmy', 'IsBot', 1, 'SimpleBot', 'PurpleBot.jpg')
END