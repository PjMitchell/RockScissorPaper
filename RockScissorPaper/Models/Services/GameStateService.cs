using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameStateService
    {
        public GameStateQuery GameState { get; private set; }
        public Game CurrentGame { get; private set; }

        public GameStateService(Game game)
        {
            CurrentGame = game;
            GameState = new GameStateQuery();
        }

        public void Update(GameStatus currentStatus, GameRound currentRound = null)
        {
            GameState.Status = currentStatus;
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
                case GameStatus.EndOfGame :
                    SetAsEndGameResult();
                    break;
            }
        }

        

        #region Update private Methods

        private void SetAsEndGameResult()
        {
            GameState.GameId = CurrentGame.GameId;
            int currentRoundNumber = CurrentGame.Rounds.Count;
            GameState.RoundMessage = string.Format("{0} / {1}", currentRoundNumber, CurrentGame.Rules.TotalRounds);
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            
            //Sets Player One State
            GameState.PlayerOne.PlayerId = CurrentGame.PlayerOne.PlayerId;
            GameState.PlayerOne.CurrentScore = CurrentGame.Rounds.Count(r => r.PlayerOneOutcome == GameOutcome.Win);
            GameState.PlayerOne.PlayerMessage = SetWinLoseDrawMessage(CurrentGame.Rules.GameScoreResolver.PlayerOneOutcome);
            GameState.PlayerOne.PlayerMessage = "<a href=\"/home/GameLobby/" + CurrentGame.PlayerOne.PlayerId + "\">Play again?</a>";
            //Sets Player Two State
            GameState.PlayerTwo.PlayerId = CurrentGame.PlayerTwo.PlayerId;
            GameState.PlayerTwo.CurrentScore = CurrentGame.Rounds.Count(r => r.PlayerTwoOutcome == GameOutcome.Win);
            GameState.PlayerTwo.PlayerMessage = "<a href=\"/home/GameLobby/" + CurrentGame.PlayerTwo.PlayerId + "\">Play again?</a>";

            if (CurrentGame.Rules.GameScoreResolver.PlayerOneOutcome == GameOutcome.Win)
            {
                GameState.BannerMessage = CurrentGame.PlayerOne.Name + " wins the game!";
            }
            else if (CurrentGame.Rules.GameScoreResolver.PlayerTwoOutcome == GameOutcome.Win)
            {
                GameState.BannerMessage = CurrentGame.PlayerTwo.Name + " wins the game!";
            }
            else 
            {
                  GameState.BannerMessage = "Its a draw.";
            }
        }

        private void SetAsRoundResultState(GameRound currentRound)
        {
            GameState.GameId = CurrentGame.GameId;
            CurrentGame.Rules.RoundResolver.ResolveRound(currentRound);
            GameState.BannerMessage = CurrentGame.Rules.RoundResolver.Message;
            int currentRoundNumber = CurrentGame.Rounds.Count;
            GameState.RoundMessage = string.Format("{0} / {1}", currentRoundNumber, CurrentGame.Rules.TotalRounds);
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            
            //Sets Player One State
            GameState.PlayerOne.PlayerId = CurrentGame.PlayerOne.PlayerId;
            GameState.PlayerOne.CurrentSelection = currentRound.PlayerOneSelection;
            GameState.PlayerOne.CurrentScore = CurrentGame.Rounds.Count(r=>r.PlayerOneOutcome== GameOutcome.Win);
            GameState.PlayerOne.PlayerMessage = SetWinLoseDrawMessage(CurrentGame.Rules.RoundResolver.PlayerOneResult);
            
            //Sets Player Two State
            GameState.PlayerTwo.PlayerId = CurrentGame.PlayerTwo.PlayerId;
            GameState.PlayerTwo.CurrentSelection = currentRound.PlayerTwoSelection;
            GameState.PlayerTwo.CurrentScore = CurrentGame.Rounds.Count(r => r.PlayerTwoOutcome == GameOutcome.Win);
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

        private PlayerGameInformation SetInitialPlayerState(int playerId)
        {
            PlayerGameInformation state = new PlayerGameInformation();
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

        public void SetAsFinalRoundResult(bool valueToSet = true)
        {
            GameState.FinalRoundResult = valueToSet;
        }

       
    }
}