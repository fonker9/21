using UnityEngine;
using Fusion;

namespace Game_Logic
{
    public class ClickableDeck : MonoBehaviour
    {
        private NetworkDeck networkDeck;

        private void Start()
        {
            //networkDeck = FindObjectOfType<NetworkDeck>();
        }

        private void OnMouseDown()
        {
            Debug.Log("Нажатие обработалось");
            
            if (networkDeck != null && networkDeck.Runner != null)
            {
                Debug.Log("Deck clicked (multiplayer)!");
                networkDeck.RPC_RequestCard(networkDeck.Runner.LocalPlayer);
            }
            
        }
    }
}