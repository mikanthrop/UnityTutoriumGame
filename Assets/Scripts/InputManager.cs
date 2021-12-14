using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerControl player;

    //if player presses WASD
    public void OnMove(InputAction.CallbackContext _context)
    {
        if (!player) return;

        Vector2 movementDirection = _context.ReadValue<Vector2>();
        player.ChangeMoveDirection(movementDirection);
        print("Movement Direction: " + movementDirection);
    }

    //if player presses space
    public void OnDash(InputAction.CallbackContext _context)
    {
        if (!player) return;

        if (_context.phase == InputActionPhase.Started)
        {
            player.Dash();
        }
    }

    //if player presses Shift
    public void OnFireball(InputAction.CallbackContext _context)
    {
        if (!player) return;

        if (_context.phase == InputActionPhase.Started)
        {
            player.Fireball();
        }
    }

    public void OnInteract(InputAction.CallbackContext _context)
    {
        if (!player) return;

        if (_context.phase == InputActionPhase.Started)
        {
            player.Interact();
        }
    }
}
