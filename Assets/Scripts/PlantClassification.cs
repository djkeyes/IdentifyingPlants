using UnityEngine;
using System.Collections;

public class PlantClassification : MonoBehaviour {
	public enum PlantAttribute {
		medicine, fire, food, building, poison
	}
	public bool isMedicine = false;
	public bool isFire = false;
	public bool isFood = false;
	public bool isBuilding = false;
	public bool isPoison = false;
	public string plantName = "plant";
	public string detailsTranscript = "This is a plant.";
	public AudioClip detailsSound = null;
	public AudioClip nameSound = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsAttribute(PlantAttribute attribute){
		switch(attribute){
		case PlantAttribute.medicine:
			return isMedicine;
		case PlantAttribute.building:
			return isBuilding;
		case PlantAttribute.food:
			return isFood;
		case PlantAttribute.fire:
			return isFire;
		case PlantAttribute.poison:
			return isPoison;
		}
		return false;
	}
}
