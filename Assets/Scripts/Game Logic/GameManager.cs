using Fusion;
using Game_Logic;
using System.Collections.Generic;
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

    private Dictionary<PlayerSlot, Player> _playerSlots = new Dictionary<PlayerSlot, Player>();
    private List<Player> _players;
    private GameState _state;
    private Player _activePlayer;

    private System.Random _random = new System.Random();


    public void Start()
    {
        var components = GetComponentsInChildren<PlayerSlot>();

        foreach (var component in components)
        {
            _playerSlots.Add(component, null);
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
        _activePlayer = RandomPlayer();

        ChangeState(GameState.PlayerTurn);
    }

    public void PlayerTurn()
    {

    }

    public void End()
    {

    }

    public void AddPlayer(Player player)
    {
        var slot = FirstFreeSlot();

        _playerSlots.Add(slot, player);
        _players.Add(player);

        player.AssignSlot(slot);
    }

    public void RemovePlayer(Player player)
    {
        _playerSlots.Remove(player.Slot);
        _players.Remove(player);
    }

    private PlayerSlot FirstFreeSlot()
    {
        foreach (var (slot, player) in _playerSlots)
        {
            if (player == null)
            {
                return slot;
            }
        }

        return null;
    }

    private Player RandomPlayer()
    {
        var index = _random.Next(0, _players.Count);

        return _players[index];
    }
}
