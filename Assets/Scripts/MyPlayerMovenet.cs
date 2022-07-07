using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerMovenet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField] 
    private Animator animator;

    private Vector2 inputAxis;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Update()
    {
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat(Horizontal, inputAxis.x);
        animator.SetFloat(Vertical, inputAxis.y);
        animator.SetFloat(Speed, inputAxis.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + inputAxis * speed * Time.fixedDeltaTime);
    }
}
