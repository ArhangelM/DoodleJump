using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Interaction.Platforms
{
    internal class SnowPlatform : BasePlatform
    {
        [SerializeField] private Sprite[] _stages;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private int _hp = 3;

        private void Awake()
        {
            _animator.enabled = false;
        }

        public override void Interaction()
        {
            _hp--;
            _spriteRenderer.sprite = _stages[_hp];

            if (_hp <= 0)
            {
                _animator.enabled = true;
            }
        }

        public void DestroyPlatform()
        {
            Destroy(gameObject);
        }
    }
}
