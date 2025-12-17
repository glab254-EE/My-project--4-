using System;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour,IDamagable
{
    [field:SerializeField]
    public float MaxHealth {get;private set;} = 10;
    public float Health{get;private set;}
    public bool IsDead {get;private set;}
    public event Action<float> OnHealthChanged;
    private float OldHealth;
    public bool TryDamage(float damage)
    {
        if (Health - damage > 0)
        {
            Health = Mathf.Clamp(Health-damage,0,MaxHealth);
            OnHealthChanged.Invoke(OldHealth - Health);
            return true;
        } else
        {
            Health = 0;
            IsDead = true;
            OnHealthChanged.Invoke(OldHealth - Health);
            return false;
        }
    }

    void Start()
    {
        Health = MaxHealth;
        OldHealth = Health;
    }
}