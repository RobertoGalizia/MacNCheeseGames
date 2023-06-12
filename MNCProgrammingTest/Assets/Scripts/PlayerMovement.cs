using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 200f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(transform.up * -vertical * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(transform.forward, -horizontal * rotationSpeed * Time.deltaTime, Space.World);
    }
}
