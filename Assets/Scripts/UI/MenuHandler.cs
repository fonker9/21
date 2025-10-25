using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using System.Threading.Tasks;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private NetworkBootstrap _networkBootstrap;

    public async void StartGameAsHost()
    {
        await SceneManager.LoadSceneAsync("Main");

        await _networkBootstrap.StartGame(GameMode.Host, "dev");
    }

    public async void StartGameAsClient()
    {
        await _networkBootstrap.StartGame(GameMode.Client, "dev");
    }
}
