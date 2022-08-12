using UnityEngine;

public class FlipSpriteWithVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private bool isFacingRight = false;
    private void FixedUpdate()
    {
        sprite.flipX = isFacingRight ? rb.velocity.x > 0 : rb.velocity.x < 0;
    }
}