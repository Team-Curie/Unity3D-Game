using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public string playerName;
	public Transform playerCamera;
	public int moveSpeed = 10;
	public int mouseSens = 10;
	public float rotationZ = 0.0f;
	public bool onGround;
	public bool onWater;

	// Update is called once per frame
	void Update () {
		playerMove();
		playerRotate();
	}

	void playerMove() {
		// Move Player
		this.transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
		this.transform.position += Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime;
	}

	void playerRotate() {
		//Player Rotate
		this.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime, 0);
		rotationZ += Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		rotationZ = Mathf.Clamp(rotationZ, -50, 50);
		playerCamera.localEulerAngles = new Vector3(-rotationZ, 0, 0);
	}

}
