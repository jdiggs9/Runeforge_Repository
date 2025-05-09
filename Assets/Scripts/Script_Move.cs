using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Move : MonoBehaviour
{

    //public Transform playerTransform;

    //private float playerSpeed;
    //private float normalHeight;

    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public float runSpeedMult;
    bool running;

    [Header("Ground Check")]
    public float height;
    //public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerSpeed = 0.08f;
        //normalHeight = 1.5f;
        //playerTransform.position = new Vector3(0f, normalHeight, 0f);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, height * .5f + .2f);
        running = Input.GetKey(KeyCode.LeftShift);
        MyInput();
        SpeedControl();
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        //float cX = playerTransform.position.x;
        //float cZ = playerTransform.position.z;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    playerTransform.position = new Vector3(cX, normalHeight, cZ + playerSpeed);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    playerTransform.position = new Vector3(cX, normalHeight, cZ - playerSpeed);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    playerTransform.position = new Vector3(cX + playerSpeed, normalHeight, cZ);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    playerTransform.position = new Vector3(cX - playerSpeed, normalHeight, cZ);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    playerTransform.position = new Vector3(cX, normalHeight + 1f, cZ);
        //}
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void SpeedControl()
    {
        Vector3 flatVal = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (!running) {
            if (flatVal.magnitude > moveSpeed)
            {
                Vector3 limitedVal = flatVal.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVal.x, rb.linearVelocity.y, limitedVal.z);
            }
        } else
        {
            if (flatVal.magnitude > moveSpeed * runSpeedMult)
            {
                Vector3 limitedVal = flatVal.normalized * moveSpeed * runSpeedMult;
                rb.linearVelocity = new Vector3(limitedVal.x, rb.linearVelocity.y, limitedVal.z);
            }
        }
        
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    
    
}
