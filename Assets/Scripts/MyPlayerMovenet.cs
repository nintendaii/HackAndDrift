using System;
using DefaultNamespace;
using Helpers;
using UnityEngine;

public class MyPlayerMovenet : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private float speed = 5f;

    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private Animator animator;

    [SerializeField] private float attackOffset = 4f;
    [SerializeField] private float attackRange = 4f;

    private Vector2 inputAxis;
    private State state;

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
            var vectorToCalc = attackPosition - transform.position;
            var angle = Mathf.Atan2(vectorToCalc.y, vectorToCalc.x) * Mathf.Rad2Deg;
            Debug.LogError($"Attack direction is {AttackAngleToDirection(angle)}");

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

    private AttackDirection AttackAngleToDirection(float angle)
    {
        switch (angle)
        {
            case var n when n >= -45 && n <= 45:
                return AttackDirection.Right;
            case var n when n <= 135 && n >= 45:
                return AttackDirection.Top;
            case var n when n <= 180 && n >= 135 || n >= -180 && n <= -135:
                return AttackDirection.Left;
            case var n when n >= -135 && n <= -45:
                return AttackDirection.Bottom;
            default:
                return default;
        }
    }

    private enum AttackDirection
    {
        Top,
        Left,
        Right,
        Bottom
    }


    private enum State
    {
        Normal,
        Attack
    }
}