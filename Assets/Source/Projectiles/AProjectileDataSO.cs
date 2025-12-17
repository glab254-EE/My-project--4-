using System.Collections;
using Core.Poolin;
using UnityEngine;

public abstract class AProjectileDataSO : ScriptableObject
{
    [field:SerializeField]
    internal float Damage;
    [field:SerializeField]
    internal float LifeTime;
    [field:SerializeField]
    internal Color color;
    public virtual IEnumerator CreateAction(ObjectPool<ProjectileBehaviour> pool,Vector2 origin, Vector2 speed, string ignoredTag)
    {
        if (pool.TryGetFromPool(out ProjectileBehaviour behaviour))
        {
            if (behaviour == null || behaviour.gameObject == null) yield break;

            behaviour.transform.SetPositionAndRotation(origin, Quaternion.LookRotation(new Vector3(speed.x,speed.y),Vector3.back) * Quaternion.Euler(90,0,0));
            behaviour.Init(this,ignoredTag);
            if (behaviour.gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.linearVelocity = speed;
            }
        }
    }
}
