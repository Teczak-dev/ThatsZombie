using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public MapController MapContr;
    public GameObject UIPlayer;
    public PauseSys ps;
    public GameObject Body;
    public GameObject Camera;
    public HungrySys hs;
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

    public bool isPause;
    private Animator playerAnim;

    #region Move Values
    [Header("Other")]
    public Rigidbody PlayerRB;
    public Camera PlayerCamera;
    public float MouseSensitivity = 90f;
    public float PlayerSpeed = 1f;
    public float jumpForce = 5f;

    private bool isJumping = false;
    private bool isWalking = false;
    public bool isHide = false;
    private bool isInCarRange = false;
    //car spec
    public CarSystem carS;
    private int CarId;
    
    private bool canCrouch = true;
    
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
        playerAnim = GetComponent<Animator>();


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !MapContr.isMap)
        {
            if (gameObject.GetComponent<PlayerUIManager>().isInv) ps.Pause(3);    
            else ps.Pause(0);
            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            ps.Resume();
            isPause = false;
        }
        

        if (!isPause)
        {
            #region KeyBind
            if (Input.GetKeyDown(KeyCode.I))
            {
                gameObject.GetComponent<PlayerUIManager>().ChangeInventoryView();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                MapContr.setMap();
            }

            if (canCrouch)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                    Camera.transform.position =
                        new Vector3(Camera.transform.position.x, 1.3f, Camera.transform.position.z);
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Body.transform.localScale = new Vector3(1f, 0.5f, 1f);
                    PlayerSpeed = PlayerCrounchSpeed;
                    canSprint = false;

                }
                else
                {
                    Body.transform.localScale = new Vector3(1f, 1f, 1f);
                    PlayerSpeed = PlayerWalkingSpeed;
                    canSprint = true;
                }

                if (Input.GetKeyUp(KeyCode.LeftControl))
                    Camera.transform.position =
                        new Vector3(Camera.transform.position.x, 1.2f, Camera.transform.position.z);
            }

            if (Input.GetKey(KeyCode.LeftShift) && isWalking && canSprint)
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
                    hs.RemoveHungry(1);
                }
                else
                {
                    SprintSlider.gameObject.SetActive(false);
                }

            }

            if (Input.GetKey(KeyCode.W))
            {
                playerMoveForward();
                playerAnim.SetBool("MoveF", true);
                isWalking = true;
            }
            else playerAnim.SetBool("MoveF", false);

            if (Input.GetKey(KeyCode.S))
            {
                playerMoveBackward();
                playerAnim.SetBool("MoveB", true);
                isWalking = true;
            }
            else playerAnim.SetBool("MoveB", false);

            if (Input.GetKey(KeyCode.D))
            {
                playerMoveRight();
                isWalking = true;
                playerAnim.SetBool("MoveR", true);
            }
            else playerAnim.SetBool("MoveR", false);

            if (Input.GetKey(KeyCode.A))
            {
                playerMoveLeft();
                isWalking = true;
                playerAnim.SetBool("MoveL", true);
            }
            else playerAnim.SetBool("MoveL", false);

            if (Body.transform.localScale.y > 0.5f)
            {
                if (Input.GetKey(KeyCode.Space) && !isJumping)
                {

                    playerJumped();
                    isJumping = true;
                    canCrouch = false;
                }
            }

            if (Input.mousePosition != prevoiusMousePosition)
            {
                rotateCamera();
            }

            if (PlayerRB.transform.position == prevoiusPlayerPosition)
            {
                isWalking = false;
                playerAnim.SetBool("isMove", false);
                //PlayerAnimator.SetBool("isWalk", false);
            }
            else
            {
                isWalking = true;

                playerAnim.SetBool("isMove", true);
                //PlayerAnimator.SetBool("isWalk", true);
            }

            prevoiusPlayerPosition = PlayerRB.transform.position;

            #endregion KeyBind

            if (isInCarRange)
            {
                SetInterActionText("Press E to Enter the car");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    carS.InteractionWithCar(CarId);

                }
            }
        }
    }

    #region Player Movement Function
    private void playerMoveForward()
    {
        Vector3 movement = transform.forward * Time.deltaTime * PlayerSpeed;
        PlayerRB.MovePosition(PlayerRB.position + movement);
    }
    private void playerMoveBackward()
    {
        Vector3 movement = -transform.forward * Time.deltaTime * PlayerSpeed;
        PlayerRB.MovePosition(PlayerRB.position + movement);
    }
    private void playerMoveRight()
    {
        Vector3 movement = transform.right * Time.deltaTime * PlayerSpeed;
        PlayerRB.MovePosition(PlayerRB.position + movement);
    }
    private void playerMoveLeft()
    {
        Vector3 movement = -transform.right * Time.deltaTime * PlayerSpeed;
        PlayerRB.MovePosition(PlayerRB.position + movement);
    }
    private void playerJumped()
    {
        Vector3 jumpVelocity = new Vector3(PlayerRB.velocity.x, jumpForce, PlayerRB.velocity.z);
        PlayerRB.velocity = jumpVelocity;
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
            canCrouch = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            isInCarRange = true;
            CarId = other.GetComponentInParent<CarController>().CarId;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            isInCarRange = false;
            SetInterActionText("");
        }
    }

    #endregion Triggers And Collisions

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetInterActionText(string txt)
    {
        InterActionTxt.text = txt;
        Debug.Log(InterActionTxt.text);
    }
    

}
