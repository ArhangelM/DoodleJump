using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DoodleJump.UI.Animations
{
    [RequireComponent(typeof(Image))]
    public class Loader : MonoBehaviour
    {
        [SerializeField] private Image _loaderImage;
        [SerializeField] private float _speed = 1.0f;
        private void Start()
        {
            RunLoader();
        }

        private void RunLoader()
        {
            _loaderImage.transform.DOLocalRotate(new Vector3(0, 0, 360), _speed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}