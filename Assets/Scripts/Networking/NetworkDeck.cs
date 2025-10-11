using Fusion;
using UnityEngine;
using TMPro;
using Game_Logic;

public class NetworkDeck : NetworkBehaviour
{
    [SerializeField] private GameObject cardPrefab;

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
        Debug.Log($"Player {player.PlayerId} drew card {value}");

        // Временно игнорируем Hand
        // просто спауним карту в центре стола у всех игроков

        Vector3 spawnPos = new Vector3(-0.357f, 0.615f, -0.265f); // фиксированная позиция для теста
        GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
        cardObj.name = $"Card_{value}";

        var text = cardObj.GetComponentInChildren<TextMeshPro>();
        if (text != null)
            text.text = value.ToString();
    }
}