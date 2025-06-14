using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Common;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Items
{
    public class Star : MonoBehaviour, IInteraction
    {
        private AudioClip _starSound;

        private void Awake()
        {
            _starSound = Resources.Load<AudioClip>("Sounds/Items/star");
        }

        public void Interaction()
        {
            SignalBus.Instance.Invoke(new ContactWithItemSingal(_starSound));
            Destroy(gameObject);
        }
    }
}