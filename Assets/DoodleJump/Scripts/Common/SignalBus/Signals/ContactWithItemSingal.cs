using UnityEngine;

namespace Assets.DoodleJump.Scripts.Common.SignalBus.Signals
{
    internal class ContactWithItemSingal
    {
        public AudioClip Sound { get; set; }

        public ContactWithItemSingal(AudioClip sound = null)
        {
            Sound = sound;
        }
    }
}
