using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour 
{
    public int speed;
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();

        transform.Translate(new Vector3(moveVector.x, 0f, moveVector.y));
    }
}
