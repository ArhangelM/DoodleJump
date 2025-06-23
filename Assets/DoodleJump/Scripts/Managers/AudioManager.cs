using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            SignalBus.Instance.Subscribe<ContactWithItemSingal>(PlaySound);
        }

        private void OnDisable()
        {
            SignalBus.Instance.Unsubscribe<ContactWithItemSingal>(PlaySound);
        }

        private void PlaySound(ContactWithItemSingal signal)
        {
            if (signal.Sound == null)
                return;

            _audioSource.resource = signal.Sound;

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
                _audioSource.Play();
            }
        }
    }
}