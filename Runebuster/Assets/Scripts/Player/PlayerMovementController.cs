using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpModifier;
    [SerializeField] private Transform playerGroundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private int extraJumpValue;

    private Animator playerAnimator;
    private float playerMoveInput;
    private Rigidbody2D playerRigidBody2D;
    private bool isPlayerFacingRight = true;
    private bool isPlayerGrounded;
    private int playerExtraJumps;
    
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerExtraJumps = extraJumpValue;
        playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isPlayerGrounded = Physics2D.OverlapCircle(playerGroundCheck.position, groundCheckRadius, whatIsGround);

        if (!isPlayerGrounded) playerAnimator.SetBool("isJumping", true); else playerAnimator.SetBool("isJumping", false);

        playerMoveInput = Input.GetAxisRaw("Horizontal");
        playerRigidBody2D.velocity = new Vector2(playerMoveInput * playerSpeed, playerRigidBody2D.velocity.y);

        if (isPlayerFacingRight == false && playerMoveInput > 0)
        {
            playerAnimator.SetBool("isRunning", true);
            Flip();
        }
        else if(isPlayerFacingRight == true && playerMoveInput < 0)
        {
            playerAnimator.SetBool("isRunning", true);
            Flip();
        }
        else if (playerMoveInput == 0)
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
    }

    private void Update()
    {
        if (isPlayerGrounded == true) playerExtraJumps = extraJumpValue;

        if (Input.GetKeyDown(KeyCode.Space) && playerExtraJumps > 0)
        {
            playerRigidBody2D.velocity = Vector2.up * playerJumpModifier;
            playerExtraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && playerExtraJumps == 0 && isPlayerGrounded == true)
        {
            playerRigidBody2D.velocity = Vector2.up * playerJumpModifier;
        }
    }

    private void Flip()
    {
        isPlayerFacingRight = !isPlayerFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}

