using UnityEngine;
using System.Collections;

public class PlantsExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DetectInView dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		Debug.Log (dv.NumberOf(PlantClassification.PlantAttribute.medicine));
	}
}
