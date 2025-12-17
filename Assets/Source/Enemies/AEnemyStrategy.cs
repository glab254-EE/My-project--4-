using UnityEngine;

public abstract class AEnemyStrategy : ScriptableObject
{
    public abstract float EnemyDamage {get; set;}
    public abstract float EnemySpeed {get; set;}
    public abstract float TargetStayDuration {get;set;}
    public abstract void OnUpdate(GameObject gameObject,float dt, Transform target);
    public abstract Transform OnTriggerEntry(GameObject gameObject,Collider2D collider);
    public abstract Transform OnObjectTriggerExit(GameObject gameObject,Collider2D collider,Transform oldtarget);
    public abstract Transform OnEnemyTouched(GameObject gameObject,Collision2D collider);
}
