using UnityEngine;

[CreateAssetMenu(fileName = "ChaseEnemyStrategy", menuName = "Scriptable Objects/Strategies/ChaseEnemyStrategy")]
public class ChaseEnemyStrategy : AEnemyStrategy
{
    [field:SerializeField]
    public override float EnemyDamage { get; set; } = 1;
    [field:SerializeField]
    public override float EnemySpeed { get; set; }
    [field:SerializeField]
    public override float TargetStayDuration { get; set; }
    [field:SerializeField]
    public string PlayerTag {get; private set; }= "Player";
    [field:SerializeField]
    public float EnemyAcceloration { get; set; }
    public override Transform OnEnemyTouched(GameObject gameObject,Collision2D collider)
    {
        if (collider != null && collider.gameObject.CompareTag(PlayerTag) && collider.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TryDamage(EnemyDamage);
        }
        return null;
    }

    public override Transform OnObjectTriggerExit(GameObject gameObject,Collider2D collider,Transform target)
    {
        if (collider != null && collider.gameObject.CompareTag(PlayerTag))
        {
            return null;
        }
        return target;
    }

    public override Transform OnTriggerEntry(GameObject gameObject,Collider2D collider)
    {
        if (collider != null && collider.gameObject.CompareTag(PlayerTag))
        {
            return collider.transform;
        }
        return null;
    }

    public override void OnUpdate(GameObject gameObject,float dt, Transform target)
    {
        if (gameObject.TryGetComponent(out EnemyHealthHandler handler) && handler.IsDead) return;
        if (target != null && gameObject != null && gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
            Vector2 nonZDirection = new(direction.x,direction.y);
            rigidbody2D.linearVelocity = Vector2.Lerp(rigidbody2D.linearVelocity,nonZDirection * EnemySpeed,dt*EnemyAcceloration);
        }
    }
}
