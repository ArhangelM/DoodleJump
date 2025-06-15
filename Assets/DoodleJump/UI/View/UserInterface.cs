using Assets.DoodleJump.Scripts.Common.Enums;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.UI.View
{
    internal class UserInterface : MonoBehaviour
    {
        public void Shoot() => SignalBus.Instance.Invoke(new ShootSignal());
        public void MoveLeft() => SignalBus.Instance.Invoke(new PlayerMovingSignal(Direction.Left));
        public void MoveRight() => SignalBus.Instance.Invoke(new PlayerMovingSignal(Direction.Right));
    }
}
