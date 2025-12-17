using UnityEngine;

[CreateAssetMenu(fileName = "ShootActionSO", menuName = "Scriptable Objects/ShootActionSO")]
public class ShootActionSO : ScriptableObject
{
    [field:SerializeField]
    internal string DisplayName = "N/A";
    [field:SerializeField]
    internal float Delay = 1f;
    [field:SerializeField]
    internal float Speed = 2f;
    [field:SerializeField]
    internal AProjectileDataSO projectileData;
    [field:SerializeField]
    internal AnimationClip animationClip;
}
