using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Movement speed
    public float jumpForce = 10f; // Force applied when jumping

    [Header("Ground Check")]
    public Transform groundCheck; // Transform to check if the player is grounded
    public LayerMask groundLayer; // Layer mask for detecting ground

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rotation due to physics
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GamePlayManager.instance.gameState == GamePlayManager.State.Gameplay)
        {
            HandleMovement();
            HandleJumping();
        }
        else
        {
            ResetAnimator();
        }
    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        Vector3 move = GetMovementInput();

        if (move != Vector3.zero && isGrounded)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        Vector3 movement = Vector3.ProjectOnPlane(move, Physics.gravity.normalized).normalized * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.velocity = movement;
    }

    private Vector3 GetMovementInput()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right;
        }
        return move;
    }

    private void HandleJumping()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Vector3 jumpDirection = -Physics.gravity.normalized * jumpForce;
            rb.AddForce(jumpDirection, ForceMode.Impulse);
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void ResetAnimator()
    {
        animator.SetBool("Move", false);
        animator.SetBool("Grounded", true);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.01f, groundLayer);
    }
}
