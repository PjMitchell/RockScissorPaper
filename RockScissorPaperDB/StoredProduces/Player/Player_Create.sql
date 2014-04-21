CREATE PROCEDURE [dbo].[Player_Create]
	@PlayerName varchar(25), @IpAddress varchar(45), @Avatar varchar(25)
AS
	Insert INTO Player(PlayerName, PlayerIPAddress, AvatarImg)
	Values (@PlayerName, @IpAddress, @Avatar);
RETURN Select @@IDENTITY;
