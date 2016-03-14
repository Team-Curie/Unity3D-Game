using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    Camera playerCamera;
    public float walkingGravity = 9.8f;
    public float jumpGravity = 40f;
    public float speedKoef = 0.5f;
    public float maxMovingSpeed = 15f;
    public float jumpForce = 25f;
    public float dampTime = 2;
    public float mouseSpeedX = 1f;
    public float mouseSpeedY = 1f;
    public bool isOnTheGround;
    Rigidbody playerRigidbody;

    private Vector2 movementVector;
    private float dampVolumeX;
    private float dampVolumeZ;
    private float mouseLookVertical;
    private float mouseLookHorizontal;

    // Use this for initialization
    void Start()
    {
        playerCamera = Camera.main;
        SetGravity(walkingGravity);
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.z);

        if (movementVector.magnitude > maxMovingSpeed)
        {
            movementVector.Normalize();
            movementVector *= maxMovingSpeed;
        }

        playerRigidbody.velocity = new Vector3(movementVector.x, playerRigidbody.velocity.y, movementVector.y);

        playerRigidbody.AddRelativeForce(Input.GetAxis("Horizontal") * speedKoef, 0f, Input.GetAxis("Vertical") * speedKoef, ForceMode.Force);

        if (Input.GetAxis("Horizontal") == 0f && isOnTheGround)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, Mathf.SmoothDamp(playerRigidbody.velocity.z, 0f, ref dampVolumeZ, dampTime));
        }

        if (Input.GetAxis("Vertical") == 0f && isOnTheGround)
        {
            playerRigidbody.velocity = new Vector3(Mathf.SmoothDamp(playerRigidbody.velocity.x, 0f, ref dampVolumeX, dampTime), playerRigidbody.velocity.y, playerRigidbody.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        mouseLookVertical = playerCamera.transform.localRotation.eulerAngles.x - (Input.GetAxis("Mouse Y") * mouseSpeedX);
        mouseLookHorizontal = transform.rotation.eulerAngles.y + (Input.GetAxis("Mouse X") * mouseSpeedY);

        if (mouseLookVertical <= 300 && mouseLookVertical >= 40)
        {
            return;
        }

        transform.localRotation = Quaternion.Euler(0f, mouseLookHorizontal, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(mouseLookVertical, 0f, 0f);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        if (isOnTheGround)
        {
            return;
        }

        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnTheGround = false;
        SetGravity(jumpGravity);
    }

    private void SetGravity(float newGravity)
    {
        Physics.gravity = -Vector3.up * newGravity;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Floor" && !isOnTheGround)
        {
            isOnTheGround = true;

            if (Physics.gravity != Vector3.up * walkingGravity)
            {
                SetGravity(walkingGravity);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Floor" && !isOnTheGround)
        {
            isOnTheGround = true;
            SetGravity(walkingGravity);
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
