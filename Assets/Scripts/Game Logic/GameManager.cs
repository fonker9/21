using Fusion;
using Game_Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Prepare,
        PlayerTurn,
        End
    }

    [SerializeField] private NetworkDeck _deckObject;

    private Dictionary<PlayerSlot, Player> _playerSlots;
    private List<Player> _players;
    private GameState _state;


    public void Start()
    {
        var slots = GetComponentsInChildren<PlayerSlot>();

        foreach (var slot in slots)
        {
            _playerSlots.Add(slot, null);
        }
    }

    public void ChangeState(GameState state)
    {
        _state = state;

        switch(state)
        {
            case GameState.Prepare:
                Prepare();
                break;
            case GameState.PlayerTurn:
                PlayerTurn();
                break;
            case GameState.End:
                End();
                break;
        }
    }

    public void Prepare()
    {

    }

    public void PlayerTurn()
    {

    }

    public void End()
    {

    }

    public bool TryFindFreeSlot(out PlayerSlot freeSlot)
    {
        foreach (var (slot, player) in _playerSlots)
        {
            if (player == null)
            {
                freeSlot = slot;
                return true;
            }
        }

        freeSlot = null;
        return false;
    }

    public void AddPlayer(Player player)
    {
        if (TryFindFreeSlot(out var slot))
        {
            _playerSlots.Add(slot, player);
        }
    }

    public void RemovePlayer()
    {

    }
}
