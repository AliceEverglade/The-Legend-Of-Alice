using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Objects
    public GameObject Player;
    public CharacterController controller;
    public Transform cam;
    private InputMaster InputMaster;
    private InputAction walking;

    #endregion
    #region Movement Vars
    public float movementSpeed = 3f;
    public float sprintSpeed;
    public float sprintModifier = 2.3f;
    public float jumpheight = 20f;
    float jumpForce;
    float speedModifier;
    float turnSmoothTime = 0.1f;
    public Vector3 playerMovementVector;
    float turnSmoothVelocity;
    #endregion
    #region Gravity Vars

    public float setGravity;
    public float setTerminalVelocity;
    float gravity;
    float currentGravity;
    float constantGravity;
    float maxGravity;
    private Vector3 gravityDirection;
    private Vector3 gravityMovementVector;
    #endregion


    #region Awake and Start
    private void Awake()
    {
        InputMaster = new InputMaster();
        gravityDirection = Vector3.down;
    }
    // Start is called before the first frame update
    void Start()
    {
        setGravity = 50;
        setTerminalVelocity = 150;
        speedModifier = movementSpeed;
        sprintSpeed = movementSpeed * sprintModifier;
        
    }
    #endregion

    #region Enable and Disable
    private void OnEnable()
    {
        walking = InputMaster.Player.walking;
        walking.Enable();

        InputMaster.Player.jumping.performed += Jumping;
        InputMaster.Player.jumping.Enable();
    }
    private void OnDisable()
    {
        walking.Disable();
        InputMaster.Player.jumping.Disable();
    }

    #endregion



    #region Update
    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        CalculateGravity();
        
    }
    #endregion

    
    #region movement
    public void MovementUpdate()
    {

        Vector2 movementAction = walking.ReadValue<Vector2>();

        float horizontal = movementAction.x;
        float vertical = movementAction.y;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        speedModifier = movementSpeed;
        sprintSpeed = movementSpeed * sprintModifier;

        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            /*if (Input.GetKey(KeyCode.LeftShift))
            {
                speedModifier = sprintSpeed;
            }
            else
            {
                speedModifier = movementSpeed;
            }*/

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerMovementVector = moveDir * speedModifier * Time.deltaTime;
        }
        if (direction.magnitude < 0.1f)
        {
            playerMovementVector = new Vector3(0f, 0f, 0f);
        }

        controller.Move(playerMovementVector + gravityMovementVector);
    }
    #endregion

    #region Jumping
    private void Jumping(InputAction.CallbackContext obj)
    {
        jumpForce = jumpheight / setGravity;
        if (Input.GetKey(KeyCode.Space))
        {
            //add the jump force
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("jumpin has stoppeth");
        }
    }


    #endregion

    #region gravity
    private bool IsGrounded()
    {
        return controller.isGrounded;
    }
    private void CalculateGravity()
    {
        gravity = setGravity / 1000;
        constantGravity = gravity * -1;
        maxGravity = setTerminalVelocity / 1000 * -1;

        if (IsGrounded())
        {
            currentGravity = constantGravity;
        }
        else
        {
            if(currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }
        gravityMovementVector = gravityDirection * -currentGravity;
    }
    #endregion
}
