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

	public int NumberOf(PlantAttribute attribute){
		int num = 0;
		foreach(PlantClassification plant in plants){
			if(plant.IsAttribute(attribute)){
				num++;
			}
		}
		return num;
	}

	public float GetInverseSumOfDistances(PlantAttribute attribute){
		float distance = 0f;
		for(int i = 0; i < plants.Length;i++){
			if(plants[i].IsAttribute(attribute)){
				if(distances[i] < 1e-3){
					distance += 1.0f;
				} else {
					distance += 1.0f / distances[i];
				}
			}
		}
		return SonifyDistanceToVolume (distance);
	}

	private float SonifyDistanceToVolume(float distance){
		if (distance >= 1f) {
			return 1f;
		} else if(distance < 0.005f){
			return 0.005f;
		} else {
			return distance;
		}
	}
	
	public float PlantVolume(string name){
		float closestDistance = 0f;
		for(int i = 0; i < plants.Length;i++){
			if(plants[i].name.Equals(name)){
				float distance;
				if(distances[i] < 1e-3){
					distance = 1.0f;
				} else {
					distance = 1.0f / distances[i];
				}
				if(closestDistance < distance){
					closestDistance = distance;
				}
			}
		}
		return SonifyDistanceToVolume (closestDistance);
	}

	public float ClosestVolume(){
		float closestDistance = float.MinValue;
		for(int i = 0; i < distances.Length;i++){
			if(distances[i] < closestDistance){
				closestDistance = distances[i];
			}
		}
		return SonifyDistanceToVolume(closestDistance);
	}

	public float ClosestVolume(PlantAttribute attribute){
		float closestDistance = float.MinValue;
		for(int i = 0; i < distances.Length;i++){
			if(plants[i].IsAttribute(attribute)){
				if(distances[i] < closestDistance){
					closestDistance = distances[i];
				}
			}
		}
		return SonifyDistanceToVolume(closestDistance);
	}

	public PlantClassification ClosestPlant(PlantAttribute attribute){
		PlantClassification closest = null;
		float closestDistance = float.MaxValue;
		for(int i = 0; i < distances.Length;i++){
			if(plants[i].IsAttribute(attribute)){
				if(distances[i] < closestDistance){
					closestDistance = distances[i];
					closest = plants[i];
				}
			}
		}
		return closest;
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
