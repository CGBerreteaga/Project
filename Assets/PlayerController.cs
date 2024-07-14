using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 4.0f;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] bool isCrouching = false;
    [SerializeField] bool cursorEnabled = false;
    [SerializeField] bool isAttacking = false;

    [SerializeField] InputActionAsset playerInput;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody rb;
    private Animator animator;
    private bool isSprinting = false;
    private bool isGrounded = true;
    private bool isPerformingAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
    }

    void LateUpdate()
    {
        Move();
        Attack();
    }

    void Move()
    {
        float speed = isSprinting && !isCrouching ? runSpeed : walkSpeed;
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 moveVelocity = transform.forward * moveDirection.z * speed + transform.right * moveDirection.x * speed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        if (moveDirection != Vector3.zero)
        {
            if (isSprinting && !isCrouching)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isCrouchWalking", false);
            }
            else if (!isSprinting && !isCrouching)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isCrouchWalking", false);
            }
            else
            {
                animator.SetBool("isCrouchWalking", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrouchWalking", false);

            if (isCrouching)
            {
                animator.SetBool("isCrouching", true);
            }
            else
            {
                animator.SetBool("isCrouching", false);
            }
        }
    }

    void Look()
    {
        Vector3 rotation = new Vector3(0, lookInput.x * rotationSpeed, 0);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    void Attack()
    {
        if (isAttacking && !isPerformingAttack)
        {
            isPerformingAttack = true;
            animator.SetTrigger("Attack");
        }
    }

   
        
  

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    void OnMelee()
    {
        isAttacking = true;
    }

    void OnCrouch(InputValue value)
    {
        isCrouching = value.isPressed;

        if (isCrouching)
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
    }

    void OnCursor()
    {
        cursorEnabled = !cursorEnabled;
        Cursor.lockState = cursorEnabled ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = cursorEnabled;
    }

    void OnChangeTarget()
    {
        // Implement target change logic here
    }

    public void OnAttackAnimationEnd()
    {
        isAttacking = false;

        isPerformingAttack = false;
        animator.ResetTrigger("Attack");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
