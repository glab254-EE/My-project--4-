using UnityEngine;

public class MovementInvoker
{
    private Rigidbody2D rb;
    public void ChangeVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }
    public void LookAtPoint(Vector3 point)
    {
        Vector3 direction = ((Vector2)point-rb.position).normalized;
        rb.SetRotation(Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg);
    }
    public MovementInvoker(Rigidbody2D rigidbody2D)
    {
        rb = rigidbody2D;
    }
}
