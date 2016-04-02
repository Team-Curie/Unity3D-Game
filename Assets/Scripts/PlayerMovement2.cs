using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    public Transform playerCamera;
    public float playerHealth = 1024f;
    public float walkGravity = 9.8f;
    public float jumpGravity = 40f;
    public float moveSpeed = 25f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 1f;
    public float rotationZ = 0.0f;
    public Slider healthSlider;

    private Rigidbody playerRigidbody;
    private bool isOnTheGround = false;
    private GameObject gun;
    private Animation reloadAnimation;

    void Awake()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
    }

    // Use this for initialization
    void Start()
    {
        //playerCamera = Camera.main;
        SetGravity(walkGravity);
        playerRigidbody = this.GetComponent<Rigidbody>();
        healthSlider.value = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //moving player
        //playerRigidbody.velocity = transform.TransformDirection(new Vector3(-Input.GetAxis("Vertical") * moveSpeed, playerRigidbody.velocity.y, Input.GetAxis("Horizontal") * moveSpeed));
        //rotate
        //this.transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0f));
        playerMove();
        playerRotate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            Debug.Log(healthSlider.value);
        }

        if (Input.GetMouseButtonDown(1))
        {
            reloadAnimation = gun.GetComponent<Animation>();
            //reloadAnimation = gun.GetComponent("ReloadGun") as Animation;
            reloadAnimation.Play();
        }
    }

    void playerMove()
    {
        // Move Player
        this.transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
        this.transform.position += Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime;

        if (Input.GetAxis("Vertical") != 0)
        {
            //Debug.Log(transform.GetComponent<Animation>().name);
            //this.transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
            //transform.GetComponent<Animation>().Play("PlayerMove");
        }
    }

    void playerRotate()
    {
        //Player Rotate
        this.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0);
        rotationZ += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationZ = Mathf.Clamp(rotationZ, -50, 50);
        playerCamera.localEulerAngles = new Vector3(-rotationZ, 0, 0);
    }

    private void Jump()
    {
        Debug.Log("Jumping...");
        if (isOnTheGround)
        {
            return;
        }

        playerRigidbody.AddForce(Vector3.up * jumpForce);
        isOnTheGround = false;
        SetGravity(jumpGravity);
    }

    private void SetGravity(float gravityToBeSet)
    {
        Physics.gravity = -Vector3.up * gravityToBeSet;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Floor" && !isOnTheGround)
        {
            isOnTheGround = true;
            SetGravity(walkGravity);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Floor" && !isOnTheGround)
        {
            isOnTheGround = true;

            if (Physics.gravity != Vector3.up * walkGravity)
            {
                SetGravity(walkGravity);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Floor" && isOnTheGround)
        {
            isOnTheGround = false;
            SetGravity(jumpGravity);
        }
    }
}
