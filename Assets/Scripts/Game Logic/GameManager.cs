using Fusion;
using Game_Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private Dictionary<PlayerSlot, Player> _playerSlots;


    public void Start()
    {
        var slots = GetComponentsInChildren<PlayerSlot>();

        foreach (var slot in slots)
        {
            _playerSlots.Add(slot, null);
        }
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
}
