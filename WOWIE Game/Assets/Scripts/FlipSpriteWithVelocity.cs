using UnityEngine;

public class FlipSpriteWithVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private bool isFacingRight = false;
    public float minvel=0.2f;
    private void Update()
    {
        sprite.flipX = isFacingRight ? rb.velocity.x > minvel : rb.velocity.x < minvel;
    }
}