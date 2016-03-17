using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    Camera shipCamera;
    public float fuel = 1024f;
    public float speed = 1f;
    public float moveRate = 10f;
    public float turnRate = 2f;

    //private Rigidbody shipRigidBody;
    private bool isMoving = false;
    private Vector3 targetPos;
    private float mousePositionX;
    private float mousePositionY;
    private Vector3 mousePosition;

    void Start()
    {
        shipCamera = Camera.main;
        //shipRigidBody = GetComponent<Rigidbody>();
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Moving");
            isMoving = true;
            DecreaseFuel(); ;
            Debug.Log(targetPos);
            //MoveShip();
            //transform.position += transform.forward * speed * Time.deltaTime;
            //transform.Translate(0f, 0f, Time.deltaTime * speed);
        }

        if (isMoving)
        {
            MoveShip();
        }
    }

    void MoveShip()
    {
        //shipRigidBody.AddRelativeForce(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, Time.deltaTime * speed, ForceMode.Impulse);
        //float distance = transform.position.z + moveRate;
        mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - shipCamera.transform.position.z;
        //targetPos = new Vector3(mousePositionX, mousePositionY, distance);
        targetPos = shipCamera.ScreenToWorldPoint(mousePosition);

        //this.transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), (this.transform.position.z + speed) * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * turnRate, 0f);
    }

    private void DecreaseFuel()
    {
        fuel -= (speed / 10);
        Debug.Log(fuel);
    }
}
