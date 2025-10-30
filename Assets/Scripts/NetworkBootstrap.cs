using Fusion;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkBootstrap : MonoBehaviour
{
    public const int MAX_PLAYERS = 2;

    private NetworkRunner _runner;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task StartGame(GameMode gameMode, string sessionName)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);

        await _runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = sessionName,
            Scene = scene,
            SceneManager = sceneManager,
            PlayerCount = MAX_PLAYERS,
        });
    }
}
