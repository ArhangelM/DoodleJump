using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.Gameplay;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Assets.DoodleJump.Scripts.Models;
using TMPro;
using Tools.SignalBus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DoodleJump.UI.View
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _starCounter;
        [SerializeField] private TextMeshProUGUI _enemyCounter;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _menu;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UpdateStarCounter(ContactWithItemSingal signal)
        {
            Statistic.Instance.AddStar();
            _starCounter.text = Statistic.Instance.StarCount.ToString();
        }

        private void UpdateEnemyCounter(KillEnemySignal signal)
        {
            Statistic.Instance.KillEnemy();
            _enemyCounter.text = Statistic.Instance.EnemyCount.ToString();
        }

        private void UpdateScoreCounter(UpdateScoreSignal signal)
        {
            Statistic.Instance.UpdateScore(signal.Score);
            _score.text = Statistic.Instance.Score.ToString();
        }

        private void Reset(EndGameSignal signal)
        {
            Statistic.Instance.Reset();
            ClearView();
        }

        private void OnMenuButtonClick()
        {
            Statistic.Instance.Reset();
            ClearView();
            SignalBus.Instance.Invoke(new OpenMenuSignal());
        }

        private void ClearView()
        {
            _starCounter.text = "0";
            _enemyCounter.text = "0";
            _score.text = "0";
        }

        private void SubscribeEvents()
        {
            SignalBus.Instance.Subscribe<ContactWithItemSingal>(UpdateStarCounter, 1);
            SignalBus.Instance.Subscribe<KillEnemySignal>(UpdateEnemyCounter, 1);
            SignalBus.Instance.Subscribe<UpdateScoreSignal>(UpdateScoreCounter, 1);
            SignalBus.Instance.Subscribe<EndGameSignal>(Reset);
            _menu.onClick.AddListener(OnMenuButtonClick);
        }

        private void UnsubscribeEvents()
        {
            SignalBus.Instance.Unsubscribe<ContactWithItemSingal>(UpdateStarCounter);
            SignalBus.Instance.Unsubscribe<KillEnemySignal>(UpdateEnemyCounter);
            SignalBus.Instance.Unsubscribe<UpdateScoreSignal>(UpdateScoreCounter);
            SignalBus.Instance.Unsubscribe<EndGameSignal>(Reset);
            _menu.onClick.RemoveListener(OnMenuButtonClick);
        }
    }
}