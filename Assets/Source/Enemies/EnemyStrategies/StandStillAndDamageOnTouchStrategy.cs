using UnityEngine;

[CreateAssetMenu(fileName = "StandStillAndDamageOnTouchStrategy", menuName = "Scriptable Objects/Strategies/StandStillAndDamageOnTouchStrategy")]
public class StandStillAndDamageOnTouchStrategy : AEnemyStrategy
{
    [field:SerializeField]
    public override float EnemyDamage { get; set; } = 1;
    [field:SerializeField]
    public override float EnemySpeed { get; set; }
    [field:SerializeField]
    public override float TargetStayDuration { get; set; }
    [field:SerializeField]
    public string PlayerTag {get; private set; }= "Player";
    public override Transform OnEnemyTouched(GameObject gameObject,Collision2D collider)
    {
        if (collider != null && collider.gameObject.CompareTag(PlayerTag) && collider.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TryDamage(EnemyDamage);
        }
        return null;
    }

    public override Transform OnObjectTriggerExit(GameObject gameObject,Collider2D collision,Transform collider)
    {
        return null;
    }

    public override Transform OnTriggerEntry(GameObject gameObject,Collider2D collider)
    {
        return null;
    }

    public override void OnUpdate(GameObject gameObject,float dt, Transform target)
    {
        // do nothing
    }
}
