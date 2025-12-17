using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInvoker : MonoBehaviour
{
    [field:SerializeField]
    private float PLayerSpeed = 1;
    [field:SerializeField]
    private PlayerHealthHandler healthHandler;
    private MovementInvoker invoker;
    private bool Active = true;
    void Start()
    {
        healthHandler.OnHealthChanged += OnHealthChanged;
        invoker = new(GetComponent<Rigidbody2D>());
    }
    void Update()
    {
        if (Active)
        {
            invoker.ChangeVelocity(PlayerInputListener.MovementVector * PLayerSpeed);
            invoker.LookAtPoint(PlayerInputListener.WorldMousePosition);
        }
    }
    private void OnHealthChanged(float currentHealth)
    {
        Active = currentHealth > 0;
    }
}
