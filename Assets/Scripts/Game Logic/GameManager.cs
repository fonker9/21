using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class GameManager : NetworkBehaviour 
    {
        [SerializeField]private NetworkDeck _networkDeck;
        private Player hostPlayer;
        public Player HostPlayer => hostPlayer;
        
        private Player clientPlayer;
        public Player ClientPlayer => clientPlayer;
        public enum GameState
        {
            Idle,
            Dealing,
            PlayerTurns,
            RoundEnd
        }
        [Networked] public GameState currentState { get; set; }
        
        public void RunRound()
        {
            
            Debug.Log("запущен метод RunRound()");
            PrepareRound();
            DealInitialCards();
            HandleTurns();
        }
        
        //может запустить только Host, иначе deck = Null
        private void PrepareRound()
        {
            Debug.Log("запущен метод PrepareRound()");
            //Проверка
            _networkDeck.deck.PrintDeck();
            _networkDeck.Hand.PrintHands();
            
            currentState = GameState.Dealing;
            _networkDeck.deck.ResetDeck();
            _networkDeck.Hand.HostCards.Clear();
            _networkDeck.Hand.ClientCards.Clear();
            
            //Проверка
            _networkDeck.deck.PrintDeck();
            _networkDeck.Hand.PrintHands();
        }
        
        private void DealInitialCards()
        {
            Debug.Log("Раздача начальных карт...");

            // Проверим, что GameManager запущен только у StateAuthority (хоста)
            if (!Object.HasStateAuthority)
                return;

            // Раздаём по 2 карты каждому игроку
            for (int i = 0; i < 2; i++)
            {
                foreach (var player in Runner.ActivePlayers)
                {
                    _networkDeck.DealCardTo(player);
                }
            }

            Debug.Log("Начальные карты розданы!");
        }

        
        private void HandleTurns()
        {
            Debug.Log("Запустился HandleTurns()");
            currentState = GameState.PlayerTurns;
            Player currentPlayer = RandomFirstPlayer();
            while (!BothPlayersFinished())
            {
                HandlePlayerTurn(currentPlayer);
                currentPlayer = GetOpponent(currentPlayer);
            }
            Debug.Log("Закончился HandleTurns()");
        }
        
        private void HandlePlayerTurn(Player player)
        {
            Debug.Log("Запустился HandlePlayerTurn");
            Debug.Log($"ход игрока: {player.PlayerName}");
            if (player.HasStopped || player.IsBusted)
            {
                Debug.Log($"{player.PlayerName} уже завершил ход");
                return;
            }

            if (!player.HasInputAuthority)
            {
                Debug.Log($"Ожидание хода другого игрока ({player.PlayerName})...");
            }

            var playerAction = ""; // заменить, также заменить case ы
            switch (playerAction)
            {
                case "Hit":
                    _networkDeck.DealCardTo(player.PlayerRef);
                    Debug.Log($"{player.PlayerName} взял карту.");
                    break;
                case "Stand":
                    player.HasStopped = true;
                    Debug.Log($"{player.PlayerName} остановился.");
                    break;
                default:
                    Debug.LogWarning($"Неизвестное действие от {player.PlayerName}");
                    break;
            }
            
        }
        
        private Player RandomFirstPlayer()
        {
            return (UnityEngine.Random.Range(0, 2) == 0) ? hostPlayer : clientPlayer;
        }
        private bool BothPlayersFinished()
        {
            bool hostDone = hostPlayer.IsBusted || hostPlayer.HasStopped;
            bool clientDone = clientPlayer.IsBusted || clientPlayer.HasStopped;
            Debug.Log("both players have stopped");
            return hostDone && clientDone;
        }

        private Player GetOpponent(Player player)
        {
            return (player == hostPlayer) ? clientPlayer : hostPlayer;
        }
        public void RegisterPlayer(Player player, bool isHost)
        {
            if (isHost)
            {
                hostPlayer = player;
                hostPlayer.PlayerName = "Host Player";
            }
            else
            {
                clientPlayer = player;
                clientPlayer.PlayerName = "Client Player";
            }
                
        }
        
    }
}