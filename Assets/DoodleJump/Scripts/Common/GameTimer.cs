using Cysharp.Threading.Tasks;
using System;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private Timer _timer;
    private float _interval = 1000;
    private DateTime _start;

    private void OnEnable()
    {
        _start = DateTime.Now;
        _timer = new Timer(_interval);
        _timer.Elapsed += async (sender, e) => await OnTimerElapsed(sender, e);
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Start();
    }

    private void OnDisable()
    {
        if (_timer != null)
        {
            _timer.Elapsed -= async (sender, e) => await OnTimerElapsed(sender, e);
            _timer.Stop();
        }
    }

    public async UniTask OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        await UniTask.SwitchToMainThread();
        _timerText.text = (e.SignalTime - _start).ToString(@"mm\:ss");
    }
}
