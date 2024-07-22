using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerCam cam;

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

    [Header("Audio")]
    public AudioSource WaterMovement;
    public AudioSource breathingSounds;
    public AudioClip normBreath;
    public AudioClip panicBreath;

    [Header("Other")]
    public Transform orientation;
    public bool isPaused;

    float horizontalInput;
    float verticalInput;

    bool input;

    public bool panic;

    Vector3 moveDirection;
    Vector2 moveInput;

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
        if(!isPaused)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
            PlayerInput();
            AdjustSpeed();

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            cameraAnim.SetFloat("Speed", flatVel.magnitude);

            if(flatVel.magnitude > 0.1f)
            {
                WaterMovement.volume = Mathf.Lerp(WaterMovement.volume, flatVel.magnitude, Time.deltaTime * 1.5f);
            }
            else if(cam.lookDir.magnitude > 0.1f)
            {
                WaterMovement.volume = Mathf.Lerp(WaterMovement.volume, 1f, Time.deltaTime * 1.5f);
            }
            else
            {
                WaterMovement.volume = Mathf.Lerp(WaterMovement.volume, 0f, Time.deltaTime * 1.5f);
            }

            if (panic)
            {
                breathingSounds.clip = panicBreath;
                if (!breathingSounds.isPlaying)
                {
                    breathingSounds.Play();
                }
                StartCoroutine(Panic(panicBreath.length));
                panic = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            MovePlayer();
            SpeedControl();
        }
            
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

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        //onGround
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Force);

            //add the dust effects
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

    IEnumerator Panic(float length)
    {
        yield return new WaitForSeconds(length);
        breathingSounds.clip = normBreath;
        if (!breathingSounds.isPlaying)
        {
            breathingSounds.Play();
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            panic = true;
        }
    }
}
