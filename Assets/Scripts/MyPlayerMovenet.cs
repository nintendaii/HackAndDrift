using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Helpers;
using UnityEngine;

public class MyPlayerMovenet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField] 
    private Animator animator;

    [SerializeField] private float attackOffset = 4f;
    [SerializeField] private float attackRange = 4f;

    private Vector2 inputAxis;
    private State state;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        state = State.Normal;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandleAttack();
                break;
            case State.Attack:
                HandleAttack();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + inputAxis * speed * Time.fixedDeltaTime);
    }

    private void HandleMovement()
    {
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat(Horizontal, inputAxis.x);
        animator.SetFloat(Vertical, inputAxis.y);
        animator.SetFloat(Speed, inputAxis.sqrMagnitude);
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Utils.GetMouseWorldPosition();
            var mouseDir = (mousePosition - transform.position).normalized;
            
            var attackPosition = transform.position + mouseDir * attackOffset;
            var attackDir = mouseDir;
            
            Debug.DrawLine(transform.position, attackPosition, Color.red, 2f);

            var targetEnemy = DefaultEnemyFactory.Instance.GetClosestEnemy(attackPosition, attackRange);
            if (targetEnemy != null)
            {
                targetEnemy.Damage();
                attackDir = (targetEnemy.GetPosition() - transform.position).normalized;
            }
            var dashDistance = 2.1f;
            transform.position += attackDir * dashDistance;
            //state = PlayerState.Attack;
        }
    }
    
    
    private enum State
    {
        Normal,
        Attack
    }
}
