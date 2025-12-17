using System;
using UnityEngine;

public interface IDamagable
{
    abstract event Action<float> OnHealthChanged;
    abstract bool TryDamage(float damage);
}
