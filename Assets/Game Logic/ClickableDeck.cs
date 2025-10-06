using UnityEngine;
using TMPro;

namespace Game_Logic
{
    public class ClickableDeck : MonoBehaviour
    {
        private Deck deck;
        public GameObject cardPrefab;
        private void Start()
        {
            deck = new Deck();
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
            Vector3 spawnPos = transform.position + new Vector3(2, 0, 0); 
            GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
            // ищем текст внутри карты
            TextMeshPro text = cardObj.GetComponentInChildren<TextMeshPro>();
            if (text != null)
            {
                text.text = card.value.ToString();
            }

            cardObj.name = "Card_" + card.value;
        }
    }
}
