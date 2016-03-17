using UnityEngine;
using System.Collections;

public class planet : MonoBehaviour {

	public int range = 400;
	public GameObject player;
	public float distance;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		distanceCheck ();
	}

	void distanceCheck(){
		// Distance between Player and Planet
		distance = Vector3.Distance (this.transform.position, player.transform.position);

		if (distance <= range) {
			Debug.Log ("You are Near " + this.gameObject.name);
			player.GetComponent<ship> ().nearPlanet = true;
		} else {
			player.GetComponent<ship> ().nearPlanet = false;
		}
	}
}
