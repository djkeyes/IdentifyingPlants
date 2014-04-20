using UnityEngine;
using System.Collections;

public class DetectInView : MonoBehaviour {
	private float[] distances;
	private PlantClassification[] plants;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Plant");
		int numPlants = 0;
		for(int i = 0; i < objects.Length;i++){
			if(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), objects[i].renderer.bounds)){
				numPlants++;
			}
		}
		plants = new PlantClassification[numPlants];
		distances = new float[numPlants];
		for(int i = 0; i < objects.Length;i++){
			if(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), objects[i].renderer.bounds)){
				plants[--numPlants] = (PlantClassification) objects[i].GetComponent("PlantClassification");
				distances[numPlants] = Vector3.Distance(camera.transform.position, objects[i].transform.position);
			}
		}
	}

	public PlantClassification[] GetPlants(){
		return plants;
	}

	public float[] GetDistances(){
		return distances;
	}

	public int NumberOf(PlantClassification.PlantAttribute attribute){
		int num = 0;
		foreach(PlantClassification plant in plants){
			if(plant.IsAttribute(attribute)){
				num++;
			}
		}
		return num;
	}

	public PlantClassification ClosestPlant(){
		PlantClassification closest = null;
		float closestDistance = float.MaxValue;
		for(int i = 0; i < distances.Length;i++){
			if(distances[i] < closestDistance){
				closestDistance = distances[i];
				closest = plants[i];
			}
		}
		return closest;
	}
}
