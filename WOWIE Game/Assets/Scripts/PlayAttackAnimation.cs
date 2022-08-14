using UnityEngine;

public class PlayAttackAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string attackParamName = "Attack";

    public void StartAttack()
    {
        anim.SetBool(attackParamName, true);
    }

    public void StopAttack()
    {
        anim.SetBool(attackParamName, false);
    }
}