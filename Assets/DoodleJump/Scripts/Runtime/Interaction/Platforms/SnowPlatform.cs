using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Interaction.Platforms
{
    internal class SnowPlatform : BasePlatform
    {
        [SerializeField] private Sprite[] _stages;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _hp = 3;

        public override void Interaction()
        {
            _hp--;
            _spriteRenderer.sprite = _stages[_hp];

            if (_hp <= 0)
                Destroy(gameObject);
        }
    }
}
