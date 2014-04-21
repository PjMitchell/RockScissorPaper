CREATE TABLE [dbo].[Player]
(
	[PlayerId] int IDENTITY(1,1) PRIMARY KEY,
	[PlayerName]  varchar(25) NOT NULL,
	[PlayerIPAddress] varchar(45) NOT NULL,
	[AvatarImg] varchar(25) NOT NULL DEFAULT 'Rock1.jpg',
	[IsBot] bit NOT NULL DEFAULT 0, 
	[BotType] varchar(25)
)
