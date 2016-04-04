using UnityEngine;
using System.Collections;

public class shipRotate : MonoBehaviour {
    [SerializeField]
    private float rotationSpeed;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotationSpeed);
    }
}
