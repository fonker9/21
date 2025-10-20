using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class GameManager : NetworkBehaviour 
    {
        [SerializeField]private NetworkDeck _networkDeck;
        private Player hostPlayer;
        private Player clientPlayer;
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
    }
}