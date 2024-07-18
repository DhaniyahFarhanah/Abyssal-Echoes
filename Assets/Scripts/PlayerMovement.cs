using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Debug Readings")]
    public TMP_Text speedText;

    [Header("Movement")]
    public float moveSpeed;
    public float forwardDrag;
    public float normDrag;
    public float moveMultiplier;

    [Header("OLD_Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    //bool canJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Animations")]
    public Animator cameraAnim;

    [Header("Other")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        PlayerInput();
        AdjustSpeed();

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        cameraAnim.SetFloat("Speed", flatVel.magnitude);

        //to be deleted
        Debuging();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //JUMPING OLD
        /*if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }*/
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //onGround
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Force);
        }

        //in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier * airMultiplier, ForceMode.Force);
        }
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void AdjustSpeed()
    {
        if (grounded)
        {
            //moving forward only
            if (verticalInput == 1 && horizontalInput == 0)
            {
                rb.drag = forwardDrag;
            }
            else
            {
                rb.drag = normDrag;
            }
        }
        else
        {
            rb.drag = 0;
        }
        
    }

    private void Debuging()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(speedText!= null)
        {
            speedText.text = "Speed: " + flatVel.magnitude.ToString();
        }
        
    }

    //Jumping OLD
    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        //canJump = true;
    }
}
