using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    #region Sprinting
    [Header("Sprint")]
    public Slider SprintSlider;
    //public Animator PlayerAnimator;
    private float sprintCount=5000f;
    private float sprintMinus = 10f;
    private float SRS = 0.2f; // Sprint Regeneration Speed -  jak szybko odnawia się sprint
    private float PlayerSprintSpeed=8f;
    private float PlayerWalkingSpeed = 6f;
    private float PlayerCrounchSpeed = 3f;
    public bool isSprinting = false;
    private bool canSprint = true;
    #endregion Sprinting 

    #region Move Values
    [Header("Other")]
    public Rigidbody PlayerRB;
    public Camera PlayerCamera;
    public float MouseSensitivity = 90f;
    public float PlayerSpeed = 3f;
    public float jumpForce = 5f;

    private bool isJumping = false;
    private bool isWalking = false;
    public bool isHide = false;
    private float rotationOnX;
    private Vector3 prevoiusMousePosition;
    private Vector3 prevoiusPlayerPosition;
    #endregion Move Values

    public Text InterActionTxt;

    //[Header("LibraryOfWeapons")]
    //public Sprite xdd;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
       
       

    }

    void Update()
    {

        #region KeyBind
        if (Input.GetKey(KeyCode.LeftControl))
        {
            GetComponentInParent<Transform>().localScale = new Vector3(1f, 0.5f ,1f);
            PlayerSpeed = PlayerCrounchSpeed;
            canSprint = false;

        }
        else
        {
            GetComponentInParent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            PlayerSpeed = PlayerWalkingSpeed;
            canSprint = true;
        }

        if(Input.GetKey(KeyCode.LeftShift) && isWalking && canSprint)
        {
            if (sprintCount > 0)
            {
                //PlayerAnimator.SetBool("isSprint", true);
                SprintSlider.gameObject.SetActive(true);
                sprintCount -= sprintMinus * 1.5f;
                SprintSlider.value = sprintCount;
                PlayerSpeed = PlayerSprintSpeed;
            }
            else
            {
                PlayerSpeed = PlayerWalkingSpeed;
                //PlayerAnimator.SetBool("isSprint", false);
                SprintSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            //PlayerAnimator.SetBool("isSprint", false);
            sprintCount += sprintMinus * SRS;
            SprintSlider.value = sprintCount;
            if (sprintCount < 5000)
            {
                SprintSlider.gameObject.SetActive(true);
                
            }
            else
            {
                SprintSlider.gameObject.SetActive(false);
            }

        }
        
        if (Input.GetKey(KeyCode.W))
        {
            playerMoveForward();
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerMoveBackward();
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerMoveRight();
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerMoveLeft();
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {

            playerJumped();
            isJumping = true;
        }

        if (Input.mousePosition != prevoiusMousePosition)
        {
            rotateCamera();
        }

        if (PlayerRB.transform.position == prevoiusPlayerPosition)
        {
            isWalking = false;
            //PlayerAnimator.SetBool("isWalk", false);
        }
        else
        {
            isWalking = true;
            //PlayerAnimator.SetBool("isWalk", true);
        }
        prevoiusPlayerPosition = PlayerRB.transform.position;
       
        #endregion KeyBind

    }

    #region Player Movement Function
    private void playerMoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * PlayerSpeed;
    }
    private void playerMoveBackward()
    {
        transform.position -= transform.forward * Time.deltaTime * PlayerSpeed;
    }
    private void playerMoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * PlayerSpeed);
    }
    private void playerMoveLeft()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * PlayerSpeed);
    }
    private void playerJumped()
    {
        PlayerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void rotateCamera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
        PlayerCamera.transform.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);


    }
    #endregion Player Movement Function

    #region Triggers And Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        
    }
    #endregion Triggers And Collisions

    public void SetInterActionText(string txt)
    {
        InterActionTxt.text = txt;
    }
    

}
