using System.Collections;
using Core.Poolin;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstProjectileDataSO", menuName = "Scriptable Objects/BurstProjectileDataSO")]
public class BurstProjectileDataSO : AProjectileDataSO
{
    [field:SerializeField,Range(1,255)]
    internal int BurstCount = 1;
    [field:SerializeField]
    internal float BurstDelay = 0.5f;
    public override IEnumerator CreateAction(ObjectPool<ProjectileBehaviour> pool, Vector2 origin, Vector2 speed, string ignoredTag)
    {
        for (int i = 0; i < BurstCount; i++)
        {
            if (pool.TryGetFromPool(out ProjectileBehaviour behaviour))
            {
                if (behaviour == null || behaviour.gameObject == null) continue;

                behaviour.transform.SetPositionAndRotation(origin, Quaternion.LookRotation(new Vector3(speed.x,speed.y),Vector3.back) * Quaternion.Euler(90,0,0));
                behaviour.Init(this,ignoredTag);
                if (behaviour.gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
                {
                    rigidbody2D.linearVelocity = speed;
                }
            }
            yield return new WaitForSeconds(BurstDelay);
        }
    }
}
