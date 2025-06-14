using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.UI.View
{
    internal class UserInterface : MonoBehaviour
    {
        public void Shoot() => SignalBus.Instance.Invoke(new ShootSignal());
    }
}
