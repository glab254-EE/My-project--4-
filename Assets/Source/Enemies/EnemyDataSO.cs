using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [field:SerializeField]
    public float MaxHealth {get;private set;} = 2;
    [field:SerializeField]
    public AEnemyStrategy EnemyStrategy{get;private set;}
}
