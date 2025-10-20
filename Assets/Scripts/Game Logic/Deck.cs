using System.Collections.Generic;
using UnityEngine;

namespace Game_Logic
{
    public class Deck
    {
        private List<Card> _cards;
    
        // Конструктор
        public Deck()
        {
            _cards = new List<Card>();
            InitializeDeck();
            Shuffle();
        }

        public void ResetDeck()
        {
            _cards = new List<Card>();
            InitializeDeck();
            Shuffle();
        }
        public void Shuffle() /* перемешать карты */ 
        {
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card DrawCard()
        {
            /* взять карту сверху */
            if (_cards.Count != 0)
            {
                var topCard = _cards[0];
                _cards.RemoveAt(0);
                return topCard;
            }
            else
            {
                return null;
            }
        }

        private void InitializeDeck()
        {
            for (var i = 1; i < 12; i++)
            {
                _cards.Add(new Card(i));
            }

        }
        public void PrintDeck()
        {
            if (_cards.Count == 0)
            {
                Debug.Log("Deck is empty.");
                return;
            }
            
            string deck = string.Join(", ", _cards.ConvertAll(c => c.value.ToString()));
            

            Debug.Log($"Deck: {deck}");
        }
    }
}
