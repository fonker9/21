using System;
using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class Player : NetworkBehaviour
    {
        private Camera playerCamera;
        [Networked] public PlayerRef PlayerRef { get; set; }
        [Networked] public String PlayerName { get; set; }
        [Networked] public bool IsBusted { get; set; }           // перебор
        [Networked] public bool HasStopped { get; set; }   
        public override void Spawned()
        {
            playerCamera = GetComponentInChildren<Camera>(true);
            Debug.Log("Found camera: " + playerCamera);
            if (Object.HasInputAuthority)
            {
                // Включаем только локальную камеру
                playerCamera.enabled = true;
                var listener = playerCamera.GetComponent<AudioListener>();
                if (listener) listener.enabled = true;

                // Проверяем — это Host или Client
                if (Runner.IsServer)
                {
                    // Хост (первый игрок)
                    playerCamera.transform.position = new Vector3(-0.04522977f, 1.048429f, -0.9731609f);
                    playerCamera.transform.rotation = Quaternion.Euler(43.763f, -360.074f, -0.047f);
                    Debug.Log("Host camera positioned.");
                }
                else
                {
                    // Клиент (второй игрок)
                    playerCamera.transform.position = new Vector3(-0.04791813f, 1.01291f, 0.5280198f);
                    playerCamera.transform.rotation = Quaternion.Euler(34.652f, -180.21f, -0.053f);
                    Debug.Log("Client camera positioned.");
                }
            }
            else
            {
                // Выключаем чужие камеры
                playerCamera.enabled = false;
                var listener = playerCamera.GetComponent<AudioListener>();
                if (listener) listener.enabled = false;
            }
        }
    }
}