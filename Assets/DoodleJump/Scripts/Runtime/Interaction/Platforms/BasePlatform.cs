using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 3f;
    public float JumpForce => _jumpForce;

    public virtual void Interaction()
    {
        
    }
}
