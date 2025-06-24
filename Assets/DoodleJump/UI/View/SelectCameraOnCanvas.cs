using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Tools.Extensions;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.UI.View
{
    [RequireComponent(typeof(Canvas))]
    internal class SelectCameraOnCanvas : MonoBehaviour
    {
        private Camera _camera;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SelectCamera(StartGameSignal signal)
        {
            if (!_camera.HasValue())
            {
                _camera = Camera.main;
                _canvas.worldCamera = _camera;
            }
        }

        private void SubscribeEvents()
        {
            SignalBus.Instance.Subscribe<StartGameSignal>(SelectCamera);
        }

        private void UnsubscribeEvents()
        {
            SignalBus.Instance.Unsubscribe<StartGameSignal>(SelectCamera);
        }
    }
}
