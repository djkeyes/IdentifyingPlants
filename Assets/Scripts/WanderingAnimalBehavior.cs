using UnityEngine;
using System.Collections;
using System;


public class WanderingAnimalBehavior : MonoBehaviour {

	public float r;
	public float f;
	private Vector3 initialTransform;

	// Use this for initialization
	void Start () {
		this.initialTransform = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.initialTransform + new Vector3 (r*(float)Math.Cos(f*Time.time),0, r*(float)Math.Sin(f*Time.time));
	}
}
