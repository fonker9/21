using Fusion;
using Fusion.Sockets;
using Game_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NetworkPlayerRegistry : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private GameManager _gameManager;

    private Dictionary<PlayerRef, NetworkObject> _players;

    public override void Spawned()
    {
        Debug.Log(Runner);
    }
    public PlayerRef GetPlayerRef(Player player)
    {
        return _players.FirstOrDefault(pair => pair.Value == player).Key;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} has joined.");

        if (runner.IsServer)
        {
            if (!_players.ContainsKey(player))
            {
                NetworkObject playerObject = runner.Spawn(_playerPrefab, null, null, player);
                Debug.Log($"NetworkObject spawned for Player {player.PlayerId}");

                Debug.Log(playerObject.GetComponent<Player>());

                _players.Add(player, playerObject);
            } else
            {
                Debug.LogWarning($"Player {player.PlayerId} already has been registered???");
            }
        }
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) 
    {
        Debug.Log($"Player {player.PlayerId} has left.");

        if (runner.IsServer)
        {
            if (_players.TryGetValue(player, out var playerObject))
            {
                runner.Despawn(playerObject);
                Debug.Log($"NetworkObject revmoed for Player {player.PlayerId}");

                _players.Remove(player);
            }
            else
            {
                Debug.LogWarning($"Player {player.PlayerId} has no NetworkPlayer???");
            }
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
}
