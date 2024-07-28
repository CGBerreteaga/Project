using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 4.0f;
    [SerializeField] float raycastDistance = 10.0f;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] bool isCrouching = false;
    [SerializeField] bool cursorEnabled = false;
    [SerializeField] bool behindEnemy = false;
    [SerializeField] public bool aimingMode = false;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject aimingModeCamera;
    [SerializeField] GameObject aimingUI;
    [SerializeField] SleepDart sleepDart;
    [SerializeField] InputActionAsset playerInput;
    [SerializeField] int sprintingCost = 1;
    [SerializeField] int staminaRestoreRate = 5;
    [SerializeField] float staminaUseInterval = 1.0f; // Interval in seconds
    [SerializeField] float staminaRestoreInterval = 1.0f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody rb;
    private Animator animator;
    private bool isSprinting = false;
    private bool isGrounded = true;
    private bool isPerformingAttack = false;
    private float pitch = 0.0f;
    private Coroutine staminaCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sleepDart = GetComponent<SleepDart>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EventManager.OnJumpStartSound += HandleJumpStartSound;
        EventManager.OnJumpLandSound += HandleJumpLandSound;
        EventManager.OnSwordSound += HandleSwordSound;
        EventManager.OnStaminaUse += HandleStaminaUse;
        EventManager.OnStaminaRestore += HandleStaminaRestore;
    }

    private void OnDestroy()
    {
        EventManager.OnJumpStartSound -= HandleJumpStartSound;
        EventManager.OnJumpLandSound -= HandleJumpLandSound;
        EventManager.OnSwordSound -= HandleSwordSound;
        EventManager.OnStaminaUse -= HandleStaminaUse;
        EventManager.OnStaminaRestore -= HandleStaminaRestore;
    }

    void Update()
    {
        Look();
        if (aimingMode)
        {
            Aim();
        }
        else
        {
            ResetAim();
        }
    }

    void LateUpdate()
    {
        if (!aimingMode)
        {
            Move();
        }

        Attack();
    }

    void Move()
    {
        if (!isGrounded)
        {
            // If not grounded, don't update movement animations
            return;
        }

        float speed = isSprinting && !isCrouching ? runSpeed : walkSpeed;
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 moveVelocity = transform.forward * moveDirection.z * speed + transform.right * moveDirection.x * speed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z); // Maintain vertical velocity

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
        float aimingRotationSpeed = 70.0f;
        float normalRotationSpeed = 60.0f;

        if (aimingMode)
        {
            float yaw = lookInput.x * aimingRotationSpeed * Time.deltaTime;
            pitch -= lookInput.y * aimingRotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -45.0f, 45.0f);

            transform.Rotate(0, yaw, 0);
            aimingModeCamera.transform.localEulerAngles = new Vector3(pitch, aimingModeCamera.transform.localEulerAngles.y, 0);
        }
        else
        {
            Vector3 rotation = new Vector3(0, lookInput.x * normalRotationSpeed * Time.deltaTime, 0);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
    }

    void Attack()
    {
        if (isAttacking && !isPerformingAttack && !aimingMode && !isCrouching)
        {
            isPerformingAttack = true;
            animator.SetTrigger("Attack");
            EventManager.TriggerSwordSound(gameObject);
        }
        else if (isAttacking && !isPerformingAttack && aimingMode)
        {
            isPerformingAttack = true;
            RaycastHit hit;
            sleepDart.UseSleepDart();

            if (Physics.Raycast(aimingModeCamera.transform.position, aimingModeCamera.transform.forward, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider.GetComponent<Enemy>() != null)
                        hit.collider.GetComponent<Enemy>().Sleep();
                }
            }

            isPerformingAttack = false;
            isAttacking = false;
        }
        else if (isAttacking && !isPerformingAttack && isCrouching && !aimingMode)
        {
            isPerformingAttack = true;
            animator.SetBool("isBackstabbing", true);
            EventManager.TriggerSwordSound(gameObject);
        }
    }

    public void HandleSwordSound(GameObject instigator, AudioSource audioSource)
    {
        Debug.Log("Sword Sound Triggered");
    }

    private void HandleStaminaUse(int staminaUsed)
    {
        // No direct handling in PlayerController; handled in Player script
    }

    private void HandleStaminaRestore(int staminaRestoreRate)
    {
        // No direct handling in PlayerController; handled in Player script
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
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrouchWalking", false);
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            EventManager.TriggerJumpStartSound(gameObject);
        }
    }

    private void HandleJumpStartSound(GameObject instigator, AudioSource audioSource)
    {
        Debug.Log("Jump Start Sound Triggered");
    }

    private void HandleJumpLandSound(GameObject instigator, AudioSource audioSource)
    {
        Debug.Log("Jump Land Sound Triggered");
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;

        if (isSprinting)
        {
            if (staminaCoroutine != null)
                StopCoroutine(staminaCoroutine);
            staminaCoroutine = StartCoroutine(ConsumeStamina());
        }
        else
        {
            if (staminaCoroutine != null)
                StopCoroutine(staminaCoroutine);
            staminaCoroutine = StartCoroutine(RestoreStamina());
        }
    }

    void OnMelee()
    {
        isAttacking = true;
    }

    void OnCrouch(InputValue value)
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
    }

    void Aim()
    {
        mainCamera.SetActive(false);
        aimingModeCamera.SetActive(true);
        aimingUI.SetActive(true);
    }

    void ResetAim()
    {
        mainCamera.SetActive(true);
        aimingModeCamera.SetActive(false);
        aimingUI.SetActive(false);
    }

    void OnAimingMode()
    {
        aimingMode = !aimingMode;
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

    public void BackstabComplete()
    {
        animator.SetBool("isBackstabbing", false);
        isAttacking = false;
        isPerformingAttack = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            EventManager.TriggerJumpLandSound(gameObject);
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

    void ReactComplete()
    {
        animator.SetBool("isReacting", false);
        isAttacking = false;
        isPerformingAttack = false;
    }

    private IEnumerator ConsumeStamina()
    {
        while (isSprinting)
        {
            EventManager.TriggerOnStaminaUse(sprintingCost);
            yield return new WaitForSeconds(staminaUseInterval);
        }
    }

    private IEnumerator RestoreStamina()
    {
        while (!isSprinting)
        {
            EventManager.TriggerOnStaminaRestore(staminaRestoreRate);
            yield return new WaitForSeconds(staminaRestoreInterval);
        }
    }
}
