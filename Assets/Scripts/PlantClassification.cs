using UnityEngine;
using System.Collections;

public class PlantClassification : MonoBehaviour {
	public bool isMedicine = false;
	public bool isFire = false;
	public bool isFood = false;
	public bool isBuilding = false;
	public bool isPoison = false;
	public string plantName = "plant";
	public string detailsTranscript = "This is a plant.";
	public AudioClip detailsSound = null;
	public AudioClip nameSound = null;
	public Texture plantImage = null;
	public PoisonLevel poisonLevel = PoisonLevel.none;

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
		case PlantAttribute.building_material:
			return isBuilding;
		case PlantAttribute.food:
			return isFood;
		case PlantAttribute.firewood:
			return isFire;
		case PlantAttribute.poison:
			return isPoison;
		}
		return false;
	}
}
