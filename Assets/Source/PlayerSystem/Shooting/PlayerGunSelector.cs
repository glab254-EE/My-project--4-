using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunSelector : MonoBehaviour
{
    [field:SerializeField]
    private PlayerShootingHandler handler;
    [field:SerializeField]
    internal List<ShootActionSO> Selection{get; private set;}
    internal int SelectedIndex {get; private set;} = 0;
    private InputSystem_Actions inputActions;
    void Start()
    {
        inputActions = new();
        inputActions.Player.Next.performed += NextWeapon;
        inputActions.Player.Previous.performed += PreviousWeapon;
        inputActions.Player.Enable();
        SelectCurrentWeapon();
    }
    void OnDestroy()
    {
        inputActions.Player.Disable();        
    }
    void NextWeapon(InputAction.CallbackContext _)
    {
        if (SelectedIndex + 1 >= Selection.Count)
        {
            SelectedIndex = 0;
        } else
        {
            SelectedIndex += 1;
        }
        SelectCurrentWeapon();
    }
    void PreviousWeapon(InputAction.CallbackContext _)
    {
        if (SelectedIndex - 1 < 0)
        {
            SelectedIndex = Selection.Count-1;
        } else
        {
            SelectedIndex -= 1;
        }        
        SelectCurrentWeapon();
    }
    private void SelectCurrentWeapon()
    {
        ShootActionSO next = Selection[SelectedIndex];
        if (next != null)
        {
            handler.currentAction = next;
        }
    }
}
