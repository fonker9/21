
using Fusion;
using Fusion.Sockets;
using Game_Logic;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private GameManager _gameManager;
    private NetworkRunner _runner;
    private Dictionary<PlayerRef, NetworkObject> _players = new Dictionary<PlayerRef, NetworkObject>();

    [SerializeField] private NetworkPrefabRef playerPrefab;


    async void StartGame(GameMode mode)
    {
        _gameManager = GetComponent<GameManager>();

        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} has joined.");

        if (runner.IsServer)
        {
            if (!_players.ContainsKey(player))
            {
                Vector3 spawnPos = Vector3.zero; // можно настроить разные позиции для хоста/клиента
                Quaternion spawnRot = Quaternion.identity;

                NetworkObject playerObject = runner.Spawn(
                    playerPrefab,
                    spawnPos,
                    spawnRot,
                    player // inputAuthority
                );

                _players.Add(player, playerObject);
                Debug.Log($"PlayerPrefab spawned for Player {player.PlayerId}");
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

                _players.Remove(player);
            }
            else
            {
                Debug.LogWarning($"Player {player.PlayerId} has no NetworkPlayer???");
            }
        }
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {

    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
}