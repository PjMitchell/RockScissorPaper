using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.DAL
{
    /// <summary>
    /// Required Stored Proceedures
    /// Proc_Create_GameRound
    /// Proc_Create_GameRoundResult
    /// Proc_Create_NewGame
    /// Proc_Select_GameRoundByGameIdAndPlayerIds
    /// Proc_Select_BotVsHumanVictoryCount
    /// Proc_Select_GameById
    /// Proc_Update_GamePlayerResult
    /// Proc_Update_GameStatus
    
    /// </summary>
    
    public class GameSQLRepository : IGameRepository 
    {
        private IDatabaseConnector _dataAccess;
        private IPlayerRepository _playerRepositotry;

        public GameSQLRepository(IDatabaseConnector dataConnector, IPlayerRepository playerRepository)
        {
            _dataAccess = dataConnector;
            _playerRepositotry = playerRepository;
        }

        /// <summary>
        /// Adds new empty game to data storage
        /// </summary>
        /// <param name="game"></param>
        public void CreateNewGame(Game game)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("PlayerOneIdInput", game.PlayerOne.PlayerId));
            parameters.Add(new StoreProcedureParameter("PlayerTwoIdInput", game.PlayerTwo.PlayerId));
            parameters.Add(new StoreProcedureParameter("RuleSetIdInput", game.Rules.Id));
            game.GameId = Convert.ToInt32(_dataAccess.ExecuteScalar("Proc_Create_NewGame", parameters));
        }

        
        /// <summary>
        /// Retrives Game by Game Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game GetGame(int id)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("GameIdInput", id));
            RoshamboGameMapper mapper = new RoshamboGameMapper();
            _dataAccess.Get("Proc_Select_GameById", mapper, parameters);
            Game result = mapper.Result as Game;
            if (result.GameId != id) 
            {
                return null;
            }
            GameRulesFactory _rulefactory = new GameRulesFactory(this);
            result.Rules = _rulefactory.GetGameRules(result.Rules.Id);
            result.PlayerOne = _playerRepositotry.GetPlayer(result.PlayerOne.PlayerId);
            result.PlayerTwo = _playerRepositotry.GetPlayer(result.PlayerTwo.PlayerId);
            GameRoundMapper mapper2 = new GameRoundMapper();
            List<StoreProcedureParameter> newParameters = new List<StoreProcedureParameter>();
            newParameters.Add(new StoreProcedureParameter("GameIdInput", result.GameId));
            newParameters.Add(new StoreProcedureParameter("PlayerOneIdInput", result.PlayerOne.PlayerId));
            newParameters.Add(new StoreProcedureParameter("PlayerTwoIdInput", result.PlayerTwo.PlayerId));
            _dataAccess.Get("Proc_Select_GameRoundByGameIdAndPlayerIds", mapper2, newParameters);
            result.Rounds = mapper2.Result as List<GameRound>;
            foreach (GameRound round in result.Rounds)
            {
                result.Rules.RoundResolver.ResolveRound(round);
            }
            return result;

        }

        /// <summary>
        /// Updates the Current game Status of selected gameId with set value
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="value"></param>
        public void UpdateGameStatus(int gameId, GameStatus value)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("GameIdInput", gameId));
            parameters.Add(new StoreProcedureParameter("NewStatusInput", (int)value));
            _dataAccess.ExecuteNonQuery("Proc_Update_GameStatus", parameters);
        }

        /// <summary>
        /// Adds new round To GameRound Table and returns Id
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public int CreateRound(int roundNumber, int gameId)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("GameIdInput", gameId));
            parameters.Add(new StoreProcedureParameter("RoundNumberInput", roundNumber));
            return Convert.ToInt32(_dataAccess.ExecuteScalar("Proc_Create_GameRound", parameters));
        }

        /// <summary>
        /// Creates RoundResult for Player
        /// </summary>
        /// <param name="playerID">Id of player makeing the choice</param>
        /// <param name="gameId">The Id of the Game being played</param>
        /// <param name="gameRoundId">The Id of the current Round </param>
        /// <param name="selection">The Players selection</param>
        public void CreateGameRoundResult(int playerId, int gameId, int gameRoundId, GameSelection selection)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("PlayerIdInput", playerId));
            parameters.Add(new StoreProcedureParameter("RoshamboGameIdInput", gameId));
            parameters.Add(new StoreProcedureParameter("GameRoundIdInput", gameRoundId));
            parameters.Add(new StoreProcedureParameter("SelectionIdInput", (int)selection));
            _dataAccess.ExecuteNonQuery("Proc_Create_GameRoundResult", parameters);
            
        }

        /// <summary>
        /// Updates the GamePlayer Table with Players' game result and score
        /// </summary>
        /// <param name="game"></param>
        public void UpdateGameResult(Game game)
        {
            game.Rules.GameScoreResolver.ResolveGame(game.Rounds);
            UpdateGameResult(game.GameId, game.PlayerOne.PlayerId, game.Rules.GameScoreResolver.PlayerOneOutcome, game.Rules.GameScoreResolver.PlayerOneScore);
            UpdateGameResult(game.GameId, game.PlayerTwo.PlayerId, game.Rules.GameScoreResolver.PlayerTwoOutcome, game.Rules.GameScoreResolver.PlayerTwoScore);
            
        }
        /// <summary>
        /// Private method used by UpdateGameResult for each player
        /// </summary>
        /// <param name="game"></param>
        private void UpdateGameResult(int gameId, int playerId, GameOutcome gameOutcome, int gameScore)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("PlayerIdInput", playerId));
            parameters.Add(new StoreProcedureParameter("RoshamboGameIdInput", gameId));
            parameters.Add(new StoreProcedureParameter("GameOutcomeInput", (int)gameOutcome));
            parameters.Add(new StoreProcedureParameter("GameScoreInput", gameScore));
            _dataAccess.ExecuteNonQuery("Proc_Update_GamePlayerResult", parameters);
        }

        /// <summary>
        /// Returns the Total wins of Human and Bot Players
        /// </summary>
        /// <returns></returns>
        public CurrentGlobalResults RetrieveBotVsHumanScore()
        {
            DataTable dt = _dataAccess.Get("Proc_Select_BotVsHumanVictoryCount");
            DataRow dr = dt.Rows[0];
            MappingHelper map = new MappingHelper(dr);
            CurrentGlobalResults result = new CurrentGlobalResults();
            result.BotWins = map.MapInt32("BotVictory");
            result.HumanWins = map.MapInt32("HumanVictory");
            return result;

        }

        /// <summary>
        /// Returns the rule set for the desired Rule Id
        /// </summary>
        /// <param name="ruleId">Index to be retrieved</param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public GameRules GetGameRules(int ruleId, GameRulesFactory factory)
        {
            List<StoreProcedureParameter> paras = new List<StoreProcedureParameter>();
            paras.Add(new StoreProcedureParameter("GameRuleSetIdInput", ruleId));
            DataTable dt = _dataAccess.Get("Proc_Select_GameRuleById", paras);
            MappingHelper mh = new MappingHelper(dt.Rows[0]);
            int rounds = mh.MapInt32("NumberOfRounds");
            string buttonOrder = mh.MapString("ButtonOrder");
            GameRuleFactoryParameters allowDrawPara = GameRuleFactoryParameters.Null;
            bool allowDraw = mh.MapBool("AllowDraw");
            if (!allowDraw)
            {
                allowDrawPara = GameRuleFactoryParameters.NoDrawAllowed;
            }
            GameRuleFactoryParameters[] args = new GameRuleFactoryParameters[]{ GameRuleFactoryParameters.NoIndexRequired, allowDrawPara};
            GameType gameType =(GameType) Enum.Parse(typeof(GameType), mh.MapString("GameType"),true);
            return factory.GetGameRules(gameType, rounds, buttonOrder, args);

        }

        /// <summary>
        /// Returns Id for GameRuleSet
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        public int GetGameRuleId(GameRules rules)
        {
            List<StoreProcedureParameter> paras = new List<StoreProcedureParameter>();
            paras.Add(new StoreProcedureParameter("GameTypeInput", Convert.ToString(rules.GameType)));
            paras.Add(new StoreProcedureParameter("ButtonOrderInput", rules.ButtonBox.Id));
            paras.Add(new StoreProcedureParameter("AllowDrawInput", rules.AllowDraw));
            paras.Add(new StoreProcedureParameter("NumberOfRoundsInput", rules.TotalRounds));
            int result = (int)_dataAccess.ExecuteScalar("Proc_Select_GameRuleId", paras);
            return result;
        }
    }
}