using System.Collections;
using Core.Poolin;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunProjectileDataSO", menuName = "Scriptable Objects/ShotgunProjectileDataSO")]
public class MultiProjectileDataSO2 : AProjectileDataSO
{
    [field:SerializeField,Range(1,255)]
    internal int ShotgunCount = 1;
    [field:SerializeField]
    internal float ShotgunPositionifference = 0.5f;
    public override IEnumerator CreateAction(ObjectPool<ProjectileBehaviour> pool, Vector2 origin, Vector2 speed, string ignoredTag)
    {
        Vector2 SideDirection = new(speed.y,speed.x);
        SideDirection.Normalize();
        int startingInt = 0;
        int bounds = ShotgunCount;
        if (ShotgunCount >= 2)
        {
            bounds = Mathf.FloorToInt(ShotgunCount/2);
            startingInt = -Mathf.CeilToInt(ShotgunCount/2);
        }
        Debug.Log(startingInt);
        Debug.Log(bounds);
        for (int i = startingInt; i < bounds; i++)
        {
            if (pool.TryGetFromPool(out ProjectileBehaviour behaviour))
            {
                if (behaviour == null || behaviour.gameObject == null) continue;

                Vector2 newpos = origin;
                if (i != 0)
                {
                    newpos = origin + i * ShotgunPositionifference * SideDirection; 
                }

                behaviour.transform.SetPositionAndRotation(newpos, Quaternion.LookRotation(new Vector3(speed.x,speed.y),Vector3.back) * Quaternion.Euler(90,0,0));
                behaviour.Init(this,ignoredTag);
                if (behaviour.gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
                {
                    rigidbody2D.linearVelocity = speed;
                }
            }
        }
        yield break;
    }
}
