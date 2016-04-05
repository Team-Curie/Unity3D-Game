using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    public Transform playerCamera;
    public float playerHealth = 100f;
    public float walkGravity = 9.8f;
    public float jumpGravity = 250f;
    public float moveSpeed = 10f;
    public float mouseSensitivity = 25f;
    public float jumpForce = 1000f;
    public float rotationZ = 0.0f;
    public Slider healthSlider;
    public Text moneyText;
    public Image damageImage;
    public float flashSpeed = 1f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool shouldFlashDamage = false;
    public bool isPlayerHit = false;
    public PlayerShooting clip;
    private Rigidbody playerRigidbody;
    public bool isOnTheGround;
    private GameObject gun;
    private Animation reloadAnimation;
    private float hitTimeout = 3f;
    public int currency;

    void Awake()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
        reloadAnimation = gun.GetComponent<Animation>();
        currency = PlayerPrefs.GetInt("Money");
    }

    // Use this for initialization
    void Start()
    {
        SetGravity(walkGravity);
        playerRigidbody = GetComponent<Rigidbody>();
        healthSlider.value = playerHealth;
        clip = GetComponent<PlayerShooting>();

        //Debug.Log(PlayerPrefs.GetFloat("shipFuel"));
        //Debug.Log(PlayerPrefs.GetFloat("shipShield"));
        //Debug.Log(PlayerPrefs.GetFloat("shipHealth"));
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        playerRotate();
        moneyText.text = currency.ToString();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Jump();
        //    //Debug.Log(healthSlider.value);
        //}

        if (Input.GetMouseButtonDown(1))
        {
            if (clip.clipAmmount >= 0 && clip.currentBulletAmount == 30)
            {
                reloadAnimation.Play();
            }
        }
    }

    void LateUpdate()
    {
        if (shouldFlashDamage)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed);
        }

        shouldFlashDamage = false;
        IsPlayerHitCheck();
    }

    private void IsPlayerHitCheck()
    {
        hitTimeout -= Time.deltaTime;
        if (hitTimeout <= 0)
        {
            this.isPlayerHit = false;
            hitTimeout = 3f;
        }
    }

    void playerMove()
    {
        // Move Player
        this.transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
        this.transform.position += Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime;

        //if (Input.GetAxis("Vertical") != 0)
        //{
        //Debug.Log(transform.GetComponent<Animation>().name);
        //this.transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
        //transform.GetComponent<Animation>().Play("PlayerMove");
        //}
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
        //Debug.Log("Are we on the ground? " + isOnTheGround);
        if (!isOnTheGround)
        {
            return;
        }

        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
        //playerRigidbody.velocity += (Vector3.up * jumpForce);
        isOnTheGround = false;
        SetGravity(jumpGravity);
    }

    private void SetGravity(float gravityToBeSet)
    {
        Physics.gravity = -Vector3.up * gravityToBeSet;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "Terrain" && !isOnTheGround)
        {
            isOnTheGround = true;

            if (Physics.gravity != Vector3.up * walkGravity)
            {
                SetGravity(walkGravity);
            }
        }
        Debug.Log("Gravity on the ground is " + Physics.gravity);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Terrain" && !isOnTheGround)
        {
            isOnTheGround = true;
            SetGravity(walkGravity);
        }
        Debug.Log("Gravity on the ground is " + Physics.gravity);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Terrain" && isOnTheGround)
        {
            isOnTheGround = false;
            SetGravity(jumpGravity);
        }
        Debug.Log("Gravity in air is " + Physics.gravity);
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "darkCrystal")
        {
            currency += 20;
            Destroy(col.gameObject);
        }
    }

}
