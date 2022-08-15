using UnityEngine;

public class PlayAttackAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string attackParamName = "Attack";
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void StartAttack()
    {
        if (health.CurrentHealth <= 0) return;

        anim.SetBool(attackParamName, true);
    }

    public void StopAttack()
    {
        anim.SetBool(attackParamName, false);
    }
}