using System;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour, IDamagable
{
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
            OnHealthChanged?.Invoke(OldHealth - Health);
            return true;
        } else
        {
            Health = 0;
            IsDead = true;
            OnHealthChanged?.Invoke(OldHealth - Health);
            return false;
        }
    }
    internal void Init(EnemyDataSO enemyDataSO)
    {
        MaxHealth = enemyDataSO.MaxHealth;
        Health = enemyDataSO.MaxHealth;
        OldHealth = Health;
    }
}
