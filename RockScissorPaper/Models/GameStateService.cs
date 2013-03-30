using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameStateService
    {
        public GameState GameState { get; private set; }
        public RoshamboGame CurrentGame { get; private set; }

        public GameStateService(RoshamboGame game)
        {
            CurrentGame = game;
            GameState = new GameState();
        }

        public void Update(GameStatus currentStatus, GameRound currentRound = null)
        {
            switch (currentStatus)
            {
                case GameStatus.NewRound :
                    SetAsInitialState();
                    break;
                case GameStatus.RoundResult :
                    if (currentRound != null)
                    {
                        SetAsRoundResultState(currentRound);
                    }
                    break;
            }
        }

        #region Update private Methods
        
        private void SetAsRoundResultState(GameRound currentRound)
        {
            GameState.GameId = CurrentGame.GameId;
            CurrentGame.Rules.RoundResolver.ResolveRound(currentRound);
            GameState.BannerMessage = CurrentGame.Rules.RoundResolver.Message;
            int currentRoundNumber = CurrentGame.Rounds.Count;
            GameState.RoundMessage = string.Format("{0} / {1}", currentRound, CurrentGame.Rules.TotalRounds);
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            
            //Sets Player One State
            GameState.PlayerOne.PlayerId = CurrentGame.PlayerOne.PlayerId;
            GameState.PlayerOne.CurrentSelection = currentRound.PlayerOneSelection;
            GameState.PlayerOne.CurrentScore = CurrentGame.Rules.GameScoreResolver.PlayerOneScore;
            GameState.PlayerOne.PlayerMessage = SetWinLoseDrawMessage(CurrentGame.Rules.RoundResolver.PlayerOneResult);
            
            //Sets Player Two State
            GameState.PlayerTwo.PlayerId = CurrentGame.PlayerTwo.PlayerId;
            GameState.PlayerTwo.CurrentSelection = currentRound.PlayerTwoSelection;
            GameState.PlayerTwo.CurrentScore = CurrentGame.Rules.GameScoreResolver.PlayerTwoScore;
            GameState.PlayerTwo.PlayerMessage = SetWinLoseDrawMessage(CurrentGame.Rules.RoundResolver.PlayerTwoResult);
        }

        private void SetAsInitialState()
        {
            GameState.GameId = CurrentGame.GameId;
            GameState.BannerMessage = " Round Start";
            int currentRound = CurrentGame.Rounds.Count + 1;
            GameState.RoundMessage = string.Format("{0} / {1}", currentRound, CurrentGame.Rules.TotalRounds);
            GameState.PlayerOne = SetInitialPlayerState(CurrentGame.PlayerOne.PlayerId);
            GameState.PlayerTwo = SetInitialPlayerState(CurrentGame.PlayerOne.PlayerId); 
        }

        private PlayerState SetInitialPlayerState(int playerId)
        {
            PlayerState state = new PlayerState();
            state.PlayerId = playerId;
            state.CurrentScore = 0;
            state.PlayerMessage = "Go!";
            return state;
        }

        private string SetWinLoseDrawMessage(GameOutcome gameOutcome)
        {
            switch (gameOutcome)
            {
                case GameOutcome.Win:
                    return "You Win!";
                case GameOutcome.Lose:
                    return "You Lose!";
                case GameOutcome.Draw:
                    return "Its a tie!";
                default:
                    return null;
            }
        }
       
        #endregion

        public void SetObservingPlayer(int playerId)
        {
            GameState.PlayerOne.IsViewer = playerId == GameState.PlayerOne.PlayerId;
            GameState.PlayerTwo.IsViewer = playerId == GameState.PlayerTwo.PlayerId;
        }

       
    }
}