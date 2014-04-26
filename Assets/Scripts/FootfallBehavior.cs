using UnityEngine;
using System.Collections;

public class FootfallBehavior : MonoBehaviour {

	public AudioSource soundsource;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		soundsource.mute = directionVector.magnitude == 0;
	}
}
