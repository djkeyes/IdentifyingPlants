using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class AlarmsList : MonoBehaviour {

	private DetectInView dv;
	
	public Dictionary<PlantClassification.PlantAttribute, bool> alarmSet;

	public Dictionary<PlantClassification.PlantAttribute, KeyCode> hotkeys;

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

		
		// hotkeys:
		// z - building material
		// x - firewood
		// c - food
		// v - medicine
		// b - poison
		hotkeys = new Dictionary<PlantClassification.PlantAttribute, KeyCode>();
		hotkeys.Add (PlantClassification.PlantAttribute.building, KeyCode.Z);
		hotkeys.Add (PlantClassification.PlantAttribute.fire, KeyCode.X);
		hotkeys.Add (PlantClassification.PlantAttribute.food, KeyCode.C);
		hotkeys.Add (PlantClassification.PlantAttribute.medicine, KeyCode.V);
		hotkeys.Add (PlantClassification.PlantAttribute.poison, KeyCode.B);
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

		// also check for new alarms set
		// maybe we should put a little graphic icon in the display to show what alarms are set
		// is there a way to import an Enum? this is lengthy to type out ._.
		foreach(PlantClassification.PlantAttribute attr in Enum.GetValues(typeof(PlantClassification.PlantAttribute))){
			if (Input.GetKeyUp(hotkeys[attr])) {
				alarmSet[attr] = !alarmSet[attr];
				Debug.Log(attr + " alarm toggled " + (alarmSet[attr]?"on":"off"));
			}
		}
	}
}
