using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealthHandler))]
public class EnemyHandler : MonoBehaviour
{
    [field:SerializeField]
    internal EnemyDataSO enemyData;
    [field:SerializeField]
    private float DeletionTimer = 4f;
    private EnemyHealthHandler healthHandler;
    private Transform Target;
    private Transform NextTarget;
    private float KnowDuration = 0;
    void Start()
    {
        healthHandler = GetComponent<EnemyHealthHandler>();
        healthHandler.Init(enemyData);
        healthHandler.OnHealthChanged += _ =>
        {
          if (healthHandler.IsDead) 
            StartCoroutine(DeletionEnumerator());  
        };
    }
    void Update()
    {
        if (healthHandler.IsDead) return;

        if (Target == null && NextTarget != null)
        {
            KnowDuration = 0;
            Target = NextTarget;
        }  
        else if (NextTarget == null && Target != null)
        {
            KnowDuration += Time.deltaTime;
            if (KnowDuration > enemyData.EnemyStrategy.TargetStayDuration)
            {
                KnowDuration = 0;
                Target = null;
            }
        }

        enemyData.EnemyStrategy.OnUpdate(gameObject,Time.deltaTime,Target);
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        NextTarget = enemyData.EnemyStrategy.OnTriggerEntry(gameObject,collision);        
    }
    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        KnowDuration = 0;
        Transform potentialTarget = enemyData.EnemyStrategy.OnObjectTriggerExit(gameObject, collision, Target);
        if (potentialTarget == null)
        {
            Target = null;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        NextTarget = enemyData.EnemyStrategy.OnEnemyTouched(gameObject,collision);
    }
    private IEnumerator DeletionEnumerator()
    {
        yield return new WaitForSeconds(DeletionTimer);
        Destroy(gameObject);
    }
}
