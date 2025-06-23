using Cysharp.Threading.Tasks;
using System;
using System.Timers;
using TMPro;
using Tools.Extensions;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Common
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private Timer _timer;
        private float _interval = 1000;
        private DateTime _start;

        private void OnEnable()
        {
            TimerSetting();
            _timer.Start();
        }

        private void OnDisable()
        {
            if (_timer.HasValue())
            {
                _timer.Elapsed -= TimerElapsed;
                _timer.Stop();
            }
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            await OnTimerElapsed(sender, e);
        }

        private async UniTask OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await UniTask.SwitchToMainThread();
            _timerText.text = (e.SignalTime - _start).ToString(@"mm\:ss");
        }

        private void TimerSetting()
        {
            _start = DateTime.Now;
            _timer = new Timer(_interval);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
    }
}