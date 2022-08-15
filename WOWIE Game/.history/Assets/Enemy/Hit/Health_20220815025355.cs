using System;
using BulletFury;
using BulletFury.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Keep track of health
/// </summary>
public class Health : MonoBehaviour, IHitReceiver
{
    // Serialized Fields - set in the Unity Inspector

    #region SerializedFields

    [SerializeField] private float maxHealth;

    #endregion

    public float MaxHealth => maxHealth;
    public GameObject deathcloud ;
    // Events - broadcast a message to any objects that are listening

    #region Events

    // the event to broadcast
    public event Action<float, bool> OnHit;

    #endregion


    // Private fields - only used in this script

    #region PrivateFields

    // some private variables to track various health values
    protected float _currentHealth;
    protected float _previousHealth;
    protected float _healthPercentage;

    #endregion

    public float CurrentHealth => _currentHealth;
    public float PreviousHealth => _previousHealth;
    public float HealthPercentage => _healthPercentage;
    public bool die;
    public bool dying =false;
    /// <summary>
    /// Called when the object is enabled
    /// Reset the current health
    /// </summary>
    protected virtual void OnEnable()
    {
        _healthPercentage = 1;
        _previousHealth = _currentHealth = maxHealth;
    }

    /// <summary>
    /// Receive a hit
    /// </summary>
    /// <param name="data"></param>
    public virtual void ReceiveHit(HitData data)
    {
     //   print(_currentHealth);
        if (!enabled)
            return;
        // keep track of what the health used to be
        _previousHealth = _currentHealth;

        _currentHealth -= data.Damage;
        // keep track of what the health currently is 
        // keep track of the current health as a percentage
        // - doing it here means we only have to do the divide once, and division is a computationally expensive operation
        _healthPercentage = _currentHealth / maxHealth;
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetBool("dying", dying);
        }
        if (name.Contains("The AI"))
        {
            GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>().HIT();
                }
        if (name.Contains("Enemy") )
        {
            GetComponent<Animator>().SetTrigger("Take damage");
        }
        if(name.Contains("Sheep")){
            _currentHealth = 0;
        }
  

        if (CurrentHealth<= 0&& die)
        {
            if(deathcloud != null)
            {
                Instantiate(deathcloud, transform.position, transform.rotation);
            }
            dying = true;
           // GetComponent<Animator>().SetTrigger("Die");
           
            Destroy(gameObject,0.3f);
           
        }
        InvokeHitEvent(data.Damage > 0);
      
    }
    
    public virtual void OnBulletHit(BulletContainer bullet, BulletCollider collider)
    {
        print(_currentHealth);
        if (!enabled)
            return;
        ReceiveHit(new HitData
        {
            Damage = bullet.Damage,
            DamageType = DamageType.Projectile,
            IncomingDirection = bullet.Up,
            IncomingBullet = bullet
        });
    }

    protected void InvokeHitEvent(bool isHit)
    {
        // tell everything that cares "hey, I've been hit"
        OnHit?.Invoke(_healthPercentage, isHit);
    }
}