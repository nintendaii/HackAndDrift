using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBody2D;

    private Vector2 movement;
    
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rigidBody2D.MovePosition(rigidBody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
