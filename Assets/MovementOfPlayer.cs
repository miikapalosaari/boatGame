using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfPlayer : MonoBehaviour
{
    Vector3 movementInput;
    Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }

    void FixedUpdate()
    {
        movementInput = Input.GetAxisRaw("Horizontal") * Vector3.right +
                        Input.GetAxisRaw("Vertical") * Vector3.forward;
        movementInput.Normalize();

        float y = playerRigidbody.velocity.y;

        if (movementInput != Vector3.zero)
        {
            if (transform.forward != movementInput)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movementInput), Time.deltaTime * 180);

                playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, Vector3.zero, Time.deltaTime * 30);
            }
            else
            {
                playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, movementInput * 10, Time.deltaTime * 30);
            }
        }
        else
        {
            playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, Vector3.zero, Time.deltaTime * 30);
        }
        Vector3 velocity = playerRigidbody.velocity;
        velocity.y = y;
        playerRigidbody.velocity = velocity;
    }
}
