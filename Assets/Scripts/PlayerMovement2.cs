using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    //Camera playerCamera;
    public float playerHealth = 1024f;
    public float walkGravity = 9.8f;
    public float jumpGravity = 40f;
    public float moveSpeed = 25f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 10f;
    public Slider healthSlider;

    private Rigidbody playerRigidbody;
    private bool isOnTheGround = false;

    // Use this for initialization
    void Start()
    {
        //playerCamera = Camera.main;
        SetGravity(walkGravity);
        playerRigidbody = GetComponent<Rigidbody>();
        healthSlider.value = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //moving player
        playerRigidbody.velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * moveSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed));

        //rotate
        transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0f));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            Debug.Log(healthSlider.value);
        }
    }

    private void Jump()
    {
        Debug.Log("Jumping...");
        if (isOnTheGround)
        {
            return;
        }

        isOnTheGround = false;
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
