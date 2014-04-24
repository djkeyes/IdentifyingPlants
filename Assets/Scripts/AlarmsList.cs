using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class AlarmsList : MonoBehaviour {

	private DetectInView dv;

	public Dictionary<PlantClassification.PlantAttribute, bool> alarmSet;

	// Use this for initialization
	void Start () {
		dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");

		alarmSet = new Dictionary<PlantClassification.PlantAttribute, bool>();
		alarmSet.Add (PlantClassification.PlantAttribute.building, false);
		alarmSet.Add (PlantClassification.PlantAttribute.fire, false);
		alarmSet.Add (PlantClassification.PlantAttribute.food, false);
		alarmSet.Add (PlantClassification.PlantAttribute.medicine, false);
		// initially the poison alarm is set
		alarmSet.Add (PlantClassification.PlantAttribute.poison, true);


	}
	
	// Update is called once per frame
	void Update () {
		// on every update, check if any alarms are set for plants currently in view
		foreach(PlantClassification.PlantAttribute attr in Enum.GetValues(typeof(PlantClassification.PlantAttribute))){
			if(alarmSet[attr] && dv.NumberOf(attr) > 0){
				alarmSet[attr] = false;
				Debug.Log("found a plant of type " + attr + ", " + attr + " alarm turned off.");
				// TODO perform plant search from here
				// any special behavior needed for poison?
			}
		}
	}
}
