using Fusion;
using UnityEngine;

namespace Game_Logic
{
    public class Player : NetworkBehaviour
    {
        private PlayerSlot _slot;
        private Camera playerCamera;

        public PlayerSlot Slot { get; }

        public void AssignSlot(PlayerSlot slot)
        {
            _slot = slot;
        }

        public override void Spawned()
        {
            Debug.Log(this);
        }
    }
}