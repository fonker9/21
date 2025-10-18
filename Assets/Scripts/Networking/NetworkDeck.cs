using Fusion;
using UnityEngine;
using TMPro;
using Game_Logic;

public class NetworkDeck : NetworkBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    
    [SerializeField] private Transform hostAnchor;   // точка для хоста
    [SerializeField] private Transform clientAnchor; // точка для клиента
    private Hand hand =  new Hand();
    

    private Deck deck;
    public override void Spawned()
    {
        // Колода создается только на стороне StateAuthority (хоста)
        if (Object.HasStateAuthority)
        {
            deck = new Deck();
            Debug.Log("государственная власть есть!");
        }
            
    }

    // Клиент вызывает, когда нажимает на колоду
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_RequestCard(PlayerRef player)
    {   
        
        Debug.Log($"{player} requested a card");
        // Только хост достает карту и сообщает всем
        Card card = deck.DrawCard();
        if (card != null)
        {
            RPC_SpawnCard(card.value, player);
        }
        else
        {
            Debug.Log("Колода пуста!");
        }
        
    }

    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SpawnCard(int value, PlayerRef player)
    {
        bool isLocalPlayer = (Runner.LocalPlayer == player);
        bool isHost;
        float yAngle = 0f;
        // Если это моя карта — спавним у моего якоря,
        // иначе — у противоположного якоря.
        Transform anchor;
        if (isLocalPlayer)
        {
            // у меня — если я хост, hostAnchor; если клиент — clientAnchor
            if (Object.HasStateAuthority)
            {
                anchor = hostAnchor;
                yAngle = 0f;
            }
            else
            {
                anchor = clientAnchor;
                yAngle = 180f;
            }
            
        }
        else
        {
            if (Object.HasStateAuthority)
            {
                anchor = clientAnchor;
                yAngle = 0f;
            }
            else
            {
                anchor = hostAnchor;
                yAngle = 180f;
            }
            // карта другого — противоположный якорь
        }

        if (anchor == hostAnchor)
        { 
            isHost = true;
        }
        else
        {
            isHost = false;
        }
        //вычисляем смещение по количеству уже имеющихся карт
        int cardIndex = hand.GetCards(isHost).Count;
        float spacing = 0.08f; // расстояние между картами
        Vector3 offset = new Vector3(cardIndex * spacing, 0, 0);
        
        // Спавним карту со смещением
        Vector3 spawnPos = anchor.position + offset;
        GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
        cardObj.name = $"Card_{value}";
        cardObj.transform.Rotate(0f, yAngle, 0f);
        

        var text = cardObj.GetComponentInChildren<TextMeshPro>();
        if (text != null)
            text.text = value.ToString();

        Card card = new Card(value);
        hand.AddCard(isHost, card);
    }
    
}