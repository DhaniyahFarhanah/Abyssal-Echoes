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

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    /*[Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

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
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        AdjustSpeed();

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void AdjustSpeed()
    {
        //moving forward only
        if(verticalInput == 1 && horizontalInput == 0)
        {
            rb.drag = forwardDrag;
        }
        else
        {
            rb.drag = normDrag;
        }
    }

    private void Debuging()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        speedText.text = "Speed: " + flatVel.magnitude.ToString();
    }
}
