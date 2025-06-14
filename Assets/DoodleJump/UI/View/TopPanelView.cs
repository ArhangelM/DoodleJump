using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using TMPro;
using Tools.SignalBus;
using UnityEngine;

public class TopPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _starCounter;

    private int _starCount = 0; 

    private void OnEnable()
    {
        SignalBus.Instance.Subscribe<ContactWithItemSingal>(UpdateStarCounter);
    }

    private void OnDisable()
    {
        SignalBus.Instance.Unsubscribe<ContactWithItemSingal>(UpdateStarCounter);
    }

    private void UpdateStarCounter(ContactWithItemSingal signal)
    {
        _starCount++;
        _starCounter.text = _starCount.ToString();
    }
}
