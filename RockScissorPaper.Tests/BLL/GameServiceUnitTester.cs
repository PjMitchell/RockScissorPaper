using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.BLL;
using RockScissorPaper.DAL;
using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;

namespace RockScissorPaper.Tests
{
    [TestClass]
    public class GameServiceUnitTester
    {
        [TestMethod]
        public void GameService_TestGameRuleSet1()
        {
            PlayerSQLRepository playerRepository = new PlayerSQLRepository(new MySQLDatabaseConnector());
            GameService service = new GameService(new GameSQLRepository(new MySQLDatabaseConnector(), playerRepository), new GameEventManager());
            int playerId = playerRepository.CreatePlayer("Tester Bob",  "this.fake.add.ress","fakeAvatar");
            CreateGameCommand createGameCommand = new CreateGameCommand(){
                PlayerOneId = playerId, PlayerTwoId = 2, RuleId= 1};
            int gameId =  service.CreateGame(createGameCommand);

            //Round 1 
            ExecuteMoveCommand moveCommand = new ExecuteMoveCommand(){
                GameId = gameId, PlayerId = playerId, Selection = GameSelection.Paper};
            GameStatus status = service.ExecuteMove(moveCommand);
            
            Assert.AreEqual(GameStatus.NewRound, status);

            //Round 2
            status = service.ExecuteMove(moveCommand);
            Assert.AreEqual(GameStatus.NewRound, status);

            //Round 3
            status = service.ExecuteMove(moveCommand);
            Assert.AreEqual(GameStatus.NewRound, status);

            //Round 4
            status = service.ExecuteMove(moveCommand);
            Assert.AreEqual(GameStatus.NewRound, status);

            GameStateQuery currentState = service.GetLastestRoundResult(gameId, playerId);
            Assert.AreNotEqual(GameStatus.FinalRoundResult, currentState.Status);
            Assert.AreEqual("You Win!", currentState.PlayerOne.PlayerMessage);

            //Round 5
            status = service.ExecuteMove(moveCommand);
            Assert.AreEqual(GameStatus.FinalRoundResult, status);

            //Round 5 State
            currentState = service.GetLastestRoundResult(gameId, playerId);
            Assert.AreEqual(GameStatus.FinalRoundResult, currentState.Status);


            //GameOver
            status = service.ExecuteMove(moveCommand);
            Assert.AreEqual(GameStatus.EndOfGame, status);
            currentState = service.GetEndOfGame(gameId, playerId);
            Assert.AreNotEqual(GameStatus.FinalRoundResult, currentState.Status);
            Assert.AreEqual("Tester Bob wins the game!", currentState.BannerMessage);

        }
    }
}
