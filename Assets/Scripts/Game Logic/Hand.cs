using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class Hand : NetworkBehaviour
    {
        private readonly List<Card> _hostCards = new List<Card>();
        private readonly List<Card> _clientCards = new List<Card>();

        // Добавление карты: мы знаем, хост это или клиент
        public void AddCard(bool isHost, Card card)
        {
            if (isHost)
            {
                _hostCards.Add(card);
                Debug.Log($"Добавлена карта {card.value} в руку хоста");
            }
            else
            {
                _clientCards.Add(card);
                Debug.Log($"Добавлена карта {card.value} в руку клиента");
            }
        }

        // Получить карты одной из сторон
        public List<Card> GetCards(bool isHost)
        {
            return isHost ? _hostCards : _clientCards;
        }

        public void PrintHands()
        {
            string hostHand = string.Join(", ", _hostCards.ConvertAll(c => c.value.ToString()));
            string clientHand = string.Join(", ", _clientCards.ConvertAll(c => c.value.ToString()));

            Debug.Log($"Host hand: {hostHand}");
            Debug.Log($"Client hand: {clientHand}");
        }
    }
}