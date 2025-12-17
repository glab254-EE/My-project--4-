using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [field:SerializeField]
    private PlayerSpriteAnimator animator;
    internal ShootActionSO currentAction;
    private float Cooldown = 0;
    void Update()
    {
        if (currentAction != null && PlayerInputListener.IsLeftMouseDown && Cooldown <= 0)
        {
            Cooldown = currentAction.Delay;
            ProjectileManager.Instance.Shoot(transform.position,transform.right*currentAction.Speed,currentAction.projectileData);
            if (animator != null && currentAction.animationClip != null)
            {
                animator.PlayAnimation(currentAction.animationClip);
            }
        }
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }
    }
}
