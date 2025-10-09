using UnityEngine;
using TMPro;

namespace Game_Logic
{
    public class ClickableDeck : MonoBehaviour
    {
        private Deck deck;
        public GameObject cardPrefab;
        public Hand hand;
        private void Start()
        {
            deck = new Deck();
            hand  = new Hand();
        }
        private void OnMouseDown()
        {
            Debug.Log("Deck clicked!");
            Card card = deck.DrawCard();
            if (card != null)
            {
                Debug.Log("Вытянута карта со значением " + card.value);
                SpawnCard(card);

            }
            else
            {
                Debug.Log("В колоде больше нет карт");
            }
           
        }

        private void SpawnCard(Card card)
        {
            Vector3 spawnPos;
            
            if (hand.cards.Count == 0)
            {
                spawnPos = new Vector3(-0.1678f,0.5876f, -0.294f);
            }
            else
            {
                GameObject lastcard =  hand.cards[hand.cards.Count - 1];
                spawnPos = lastcard.transform.position + new Vector3(hand.cardSpacing, 0, 0);
            }
            
            GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
            hand.cards.Add(cardObj);
            cardObj.name = "Card_" + card.value;
            // ищем текст внутри карты
            TextMeshPro text = cardObj.GetComponentInChildren<TextMeshPro>();
            if (text != null)
            {
                text.text = card.value.ToString();
            }

            
        }
    }
}
