using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class Player : NetworkBehaviour
    {
        private AudioListener _audioListener;

        public Camera PlayerCamera;
        public PlayerSlot Slot { get; private set; }

        public override void Spawned()
        {
            _audioListener = PlayerCamera.GetComponent<AudioListener>();
            
            PlayerCamera.enabled = false;
            _audioListener.enabled = false;
        }

        public void AssignSlot(PlayerSlot slot)
        {
            Slot = slot;


            if (Object.HasInputAuthority)
            {
                PlayerCamera.enabled = true;
                _audioListener.enabled = true;

                PlayerCamera.transform.SetPositionAndRotation(slot.CameraTransform.position, slot.CameraTransform.rotation);
            }
            
            Debug.Log($"Assigned slot {slot} for {this}");
        }
    }
}