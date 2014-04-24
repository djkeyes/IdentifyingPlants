using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AlarmsList))]

public class AuditoryInput : MonoBehaviour {

	private AlarmsList alarmsList;
	
	public Dictionary<PlantClassification.PlantAttribute, KeyCode> searchHotkeys;
	public Dictionary<PlantClassification.PlantAttribute, KeyCode> alarmHotkeys;

	// Use this for initialization
	void Start () {

		alarmsList = GetComponent<AlarmsList> ();
		
		// hotkeys:
		// search
		// 1 - building material
		// 2 - firewood
		// 3 - food
		// 4 - medicine
		// 5 - poison
		searchHotkeys = new Dictionary<PlantClassification.PlantAttribute, KeyCode>();
		searchHotkeys.Add (PlantClassification.PlantAttribute.building, KeyCode.Alpha1);
		searchHotkeys.Add (PlantClassification.PlantAttribute.fire, KeyCode.Alpha2);
		searchHotkeys.Add (PlantClassification.PlantAttribute.food, KeyCode.Alpha3);
		searchHotkeys.Add (PlantClassification.PlantAttribute.medicine, KeyCode.Alpha4);
		searchHotkeys.Add (PlantClassification.PlantAttribute.poison, KeyCode.Alpha5);
		// toggle alarm
		// z - building material
		// x - firewood
		// c - food
		// v - medicine
		// b - poison
		alarmHotkeys = new Dictionary<PlantClassification.PlantAttribute, KeyCode>();
		alarmHotkeys.Add (PlantClassification.PlantAttribute.building, KeyCode.Z);
		alarmHotkeys.Add (PlantClassification.PlantAttribute.fire, KeyCode.X);
		alarmHotkeys.Add (PlantClassification.PlantAttribute.food, KeyCode.C);
		alarmHotkeys.Add (PlantClassification.PlantAttribute.medicine, KeyCode.V);
		alarmHotkeys.Add (PlantClassification.PlantAttribute.poison, KeyCode.B);
	}
	
	// Update is called once per frame
	void Update () {
	
		// listen for "auditory" input
		// ultimately this means voice input
		// for now, this just means keypresses

		
		// check for new alarms set
		// maybe we should put a little graphic icon in the display to show what alarms are set
		// is there a way to import an Enum? this is lengthy to type out ._.
		foreach(PlantClassification.PlantAttribute attr in Enum.GetValues(typeof(PlantClassification.PlantAttribute))){
			if (Input.GetKeyUp(alarmHotkeys[attr])) {
				alarmsList.alarmSet[attr] = !alarmsList.alarmSet[attr];
				Debug.Log(attr + " alarm toggled " + (alarmsList.alarmSet[attr]?"on":"off"));
			}
		}

		// also check to see if the user asked for a search
		foreach(PlantClassification.PlantAttribute attr in Enum.GetValues(typeof(PlantClassification.PlantAttribute))){
			if (Input.GetKeyUp(searchHotkeys[attr])) {
				Debug.Log("perform search for " + attr);
			}
		}

		// TODO other commands
	}
}
