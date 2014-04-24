using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AlarmsList))]
[RequireComponent(typeof(SearchManager))]

public class AuditoryInput : MonoBehaviour {

	private AlarmsList alarmsList;
	private SearchManager searchManager;

	
	public Dictionary<PlantAttribute, KeyCode> searchHotkeys;
	public Dictionary<PlantAttribute, KeyCode> alarmHotkeys;

	// Use this for initialization
	void Start () {

		alarmsList = GetComponent<AlarmsList> ();
		searchManager = GetComponent<SearchManager> ();
		// hotkeys:
		// search
		// 3 - building material
		// 4 - firewood
		// 5 - food
		// 6 - medicine
		// 7 - poison
		searchHotkeys = new Dictionary<PlantAttribute, KeyCode>();
		searchHotkeys.Add (PlantAttribute.building, KeyCode.Alpha3);
		searchHotkeys.Add (PlantAttribute.fire, KeyCode.Alpha4);
		searchHotkeys.Add (PlantAttribute.food, KeyCode.Alpha5);
		searchHotkeys.Add (PlantAttribute.medicine, KeyCode.Alpha6);
		searchHotkeys.Add (PlantAttribute.poison, KeyCode.Alpha7);
		// toggle alarm
		// z - building material
		// x - firewood
		// c - food
		// v - medicine
		// b - poison
		alarmHotkeys = new Dictionary<PlantAttribute, KeyCode>();
		alarmHotkeys.Add (PlantAttribute.building, KeyCode.Z);
		alarmHotkeys.Add (PlantAttribute.fire, KeyCode.X);
		alarmHotkeys.Add (PlantAttribute.food, KeyCode.C);
		alarmHotkeys.Add (PlantAttribute.medicine, KeyCode.V);
		alarmHotkeys.Add (PlantAttribute.poison, KeyCode.B);
	}
	
	// Update is called once per frame
	void Update () {
	
		// listen for "auditory" input
		// ultimately this means voice input
		// for now, this just means keypresses

		
		// check for new alarms set
		// maybe we should put a little graphic icon in the display to show what alarms are set
		// is there a way to import an Enum? this is lengthy to type out ._.
		foreach(PlantAttribute attr in Enum.GetValues(typeof(PlantAttribute))){
			if (Input.GetKeyUp(alarmHotkeys[attr])) {
				alarmsList.alarmSet[attr] = !alarmsList.alarmSet[attr];
				Debug.Log(attr + " alarm toggled " + (alarmsList.alarmSet[attr]?"on":"off"));
			}
		}

		// also check to see if the user asked for a search
		foreach(PlantAttribute attr in Enum.GetValues(typeof(PlantAttribute))){
			if (Input.GetKeyUp(searchHotkeys[attr])) {
				if(searchManager.DoingSearch(attr)){
					searchManager.RemoveSearch(attr);
				} else if(searchManager.HasAvailableSearches()){
					searchManager.AddSearch(attr);
				}
//				Debug.Log("perform search for " + attr);
			}
		}

		// TODO other commands
	}
}
